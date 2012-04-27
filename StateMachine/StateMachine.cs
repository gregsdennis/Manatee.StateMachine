﻿using System;
using System.Collections.Generic;
using Manatee.StateMachine.Exceptions;

namespace Manatee.StateMachine
{
	/// <summary>
	/// Represents a generic finite state machine.
	/// </summary>
	/// <typeparam name="TState">The object type to be used for states.</typeparam>
	/// <typeparam name="TInput">The object type to be used for inputs.</typeparam>
	public class StateMachine<TState, TInput>
	{
		private Dictionary<TState, Dictionary<TInput, StateMachineAction>> _machine;
		private Dictionary<object, TState> _currentStates;

		/// <summary>
		/// Gets the list of states.
		/// </summary>
		public List<TState> States { get; private set; }
		/// <summary>
		/// Gets the list of all Inputs for all states.
		/// </summary>
		public List<TInput> Inputs { get; private set; }
		/// <summary>
		/// Gets the list of all actions for all state-input combinations.
		/// </summary>
		public List<StateMachineAction> Actions { get; private set; }
		/// <summary>
		/// Gets and sets a custom update function.
		/// </summary>
		/// <remarks>
		/// The update function is called before each input is retrieved from
		/// the input stream.  It is typically used to update the input stream,
		/// but can also include any code that needs to be executed for each 
		/// state-input evaluation.
		/// </remarks>
		public UpdateAction UpdateFunction { get; set; }
		/// <summary>
		/// Provides an interface for getting and setting actions for state-input
		/// combinations.
		/// </summary>
		/// <param name="state">The state for which to get/set the action.</param>
		/// <param name="input">The input for which to get/set the action.</param>
		/// <returns>
		/// The action for the given state-input combination.  Throws an exception if
		/// not set.
		/// </returns>
		/// <remarks>
		/// The setter automatically addes the state and input to the States and
		/// Inputs lists.
		/// </remarks>
		/// <exception cref="InputNotValidForStateException&lt;TState,TInput&gt;">
		/// Thrown when attempting to get a value for an input that the given state does
		/// not recognize.
		/// </exception>
		/// <exception cref="StateNotValidException&lt;TState&gt;">
		/// Thrown when attempting to get a value for a state that the state machine
		/// does not recognize.
		/// </exception>
		public StateMachineAction this[TState state, TInput input]
		{
			get
			{
				if (_machine.ContainsKey(state))
					if (_machine[state].ContainsKey(input))
						return _machine[state][input];
					else
						throw new InputNotValidForStateException<TState, TInput>(state, input);
				throw new StateNotValidException<TState>(state);
			}
			set
			{
				if (!States.Contains(state))
				{
					States.Add(state);
					_machine[state] = new Dictionary<TInput, StateMachineAction> {{input, value}};
				}
				else
				{
					_machine[state][input] = value;
				}
				if (!Inputs.Contains(input)) Inputs.Add(input);
				if (!Actions.Contains(value)) Actions.Add(value);
			}
		}

		/// <summary>
		/// Provides a method template for a state-input action.
		/// </summary>
		/// <param name="owner">The object which created the StateMachine.</param>
		/// <param name="input">The input that triggered the function call.</param>
		/// <returns>
		/// The next state for this StateMachine object.
		/// </returns>
		public delegate TState StateMachineAction(object owner, TInput input);
		/// <summary>
		/// Provides a method template for an action to be called before each iteration
		/// of the state machine.
		/// </summary>
		/// <param name="owner">The object which created the StateMachine.</param>
		public delegate void UpdateAction(object owner);

		/// <summary>
		/// Creates a new StateMachine object.
		/// </summary>
		public StateMachine()
		{
			States = new List<TState>();
			Inputs = new List<TInput>();
			Actions = new List<StateMachineAction>();
			_machine = new Dictionary<TState, Dictionary<TInput, StateMachineAction>>();
			_currentStates = new Dictionary<object, TState>();
			UpdateFunction = null;
		}

		/// <summary>
		/// Runs the state machine.
		/// </summary>
		/// <param name="owner">The object which created the StateMachine.</param>
		/// <param name="startState">The state from which to start.</param>
		/// <param name="inputStream">The input stream.</param>
		/// <exception cref="ActionNotDefinedForStateAndInputException&lt;TState, TInput&gt;">
		/// Thrown when the input stream contains an input that the current state does
		/// not recognize.
		/// </exception>
		/// <exception cref="InputNotValidForStateException&lt;TState, TInput&gt;">
		/// Thrown when attempting to get a value for an input that the given state does
		/// not recognize.
		/// </exception>
		/// <exception cref="StateNotValidException&lt;TState&gt;">
		/// Thrown when attempting to get a value for a state that the state machine
		/// does not recognize.
		/// </exception>
		public void Run(object owner, TState startState, InputStream<TInput> inputStream)
		{
			StateMachineAction action;
			object input;
			_currentStates[owner] = startState;
			if (UpdateFunction != null) UpdateFunction(owner);
			inputStream.Reset();
			while (!inputStream.IsAtEnd)
			{
				input = inputStream.Next();
				action = this[_currentStates[owner], (TInput)input];
				if (action == null)
					throw new ActionNotDefinedForStateAndInputException<TState, TInput>(_currentStates[owner], (TInput)input);
				_currentStates[owner] = action(owner, (TInput)input);
				if (UpdateFunction != null) UpdateFunction(owner);
			}
		}

		/// <summary>
		/// Disassociates an object from this StateMachine, allowing it to remove
		/// all associated states.
		/// </summary>
		/// <param name="owner"></param>
		public void UnregisterOwner(object owner)
		{
			_currentStates.Remove(owner);
		}
	}
}