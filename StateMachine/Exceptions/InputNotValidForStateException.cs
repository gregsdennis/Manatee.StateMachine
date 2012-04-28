/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		InputNotValidForStateException.cs
	Namespace:		Manatee.StateMachine.Exceptions
	Class Name:		InputNotValidForStateException<TState, TInput>
	Purpose:		Thrown when the current state is not set up to process the
					given input.

***************************************************************************************/
using System;

namespace Manatee.StateMachine.Exceptions
{
	/// <summary>
	/// Thrown when the current state is not set up to process the given input.
	/// </summary>
	/// <typeparam name="TState">The object type of the state.</typeparam>
	/// <typeparam name="TInput">The object type of the input.</typeparam>
	[Serializable]
	public class InputNotValidForStateException<TState, TInput> : InvalidOperationException
	{
		/// <summary>
		/// The state of the state machine.
		/// </summary>
		public TState State { get; private set; }
		/// <summary>
		/// The input that the state does not recognize.
		/// </summary>
		public TInput Input { get; private set; }

		/// <summary>
		/// Creates a new instance of this exception.
		/// </summary>
		public InputNotValidForStateException() { }
		/// <summary>
		/// Creates a new instance of this exception with the default message.
		/// </summary>
		public InputNotValidForStateException(TState state, TInput input)
			: this(string.Format("State {0} is not registered to accept input {1}.", state.ToString(), input.ToString()))
		{
			State = state;
			Input = input;
		}
		/// <summary>
		/// Creates a new instance of this exception with a given message.
		/// </summary>
		public InputNotValidForStateException(string message) : base(message) { }
		/// <summary>
		/// Creates a new instance of this exception with the a given message and inner exception.
		/// </summary>
		public InputNotValidForStateException(string message, Exception inner) : base(message, inner) { }
		/// <summary>
		/// Creates a new instance of this exception with serialization and context information.
		/// </summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The context information.</param>
		protected InputNotValidForStateException(System.Runtime.Serialization.SerializationInfo info,
												 System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}