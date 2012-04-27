using System;

namespace Manatee.StateMachine.Exceptions
{
	/// <summary>
	/// Thrown when no action as been defined for the the given input in the current state.
	/// </summary>
	/// <typeparam name="TState">The object type of the state.</typeparam>
	/// <typeparam name="TInput">The object type of the input.</typeparam>
	[Serializable]
	public class ActionNotDefinedForStateAndInputException<TState, TInput> : Exception
	{
		/// <summary>
		/// The state of the state machine.
		/// </summary>
		public TState State { get; private set; }
		/// <summary>
		/// The input for the state.
		/// </summary>
		public TInput Input { get; private set; }

		/// <summary>
		/// Creates a new instance of this exception.
		/// </summary>
		public ActionNotDefinedForStateAndInputException() { }
		/// <summary>
		/// Creates a new instance of this exception with the default message.
		/// </summary>
		public ActionNotDefinedForStateAndInputException(TState state, TInput input)
			: this(string.Format("No action is defined for state {0} and input {1}.", state.ToString(), input.ToString()))
		{
			State = state;
			Input = input;
		}
		/// <summary>
		/// Creates a new instance of this exception with a given message.
		/// </summary>
		public ActionNotDefinedForStateAndInputException(string message) : base(message) { }
		/// <summary>
		/// Creates a new instance of this exception with the a given message and inner exception.
		/// </summary>
		public ActionNotDefinedForStateAndInputException(string message, Exception inner) : base(message, inner) { }
		/// <summary>
		/// Creates a new instance of this exception with serialization and context information.
		/// </summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The context information.</param>
		protected ActionNotDefinedForStateAndInputException(System.Runtime.Serialization.SerializationInfo info,
		                                                    System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}