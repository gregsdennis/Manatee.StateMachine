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
 
	File Name:		StateNotValidException.cs
	Namespace:		Manatee.StateMachine.Exceptions
	Class Name:		StateNotValidException<TState, TInput>
	Purpose:		Thrown when the specified state is not set up in the state
					machine.

***************************************************************************************/
using System;

namespace Manatee.StateMachine.Exceptions
{
	/// <summary>
	/// Thrown when the specified state is not set up in the state machine.
	/// </summary>
	/// <typeparam name="TState">The object type of the state.</typeparam>
	[Serializable]
	public class StateNotValidException<TState> : InvalidOperationException
	{
		/// <summary>
		/// The state that the state machine does not recognize.
		/// </summary>
		public TState State { get; }

		/// <summary>
		/// Creates a new instance of this exception with the default message.
		/// </summary>
		internal StateNotValidException(TState state)
			: this($"State {state} is not registered.")
		{
			State = state;
		}
		/// <summary>
		/// Creates a new instance of this exception with a given message.
		/// </summary>
		private StateNotValidException(string message)
			: base(message) {}
		/// <summary>
		/// Creates a new instance of this exception with serialization and context information.
		/// </summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The context information.</param>
		protected StateNotValidException(System.Runtime.Serialization.SerializationInfo info,
		                                 System.Runtime.Serialization.StreamingContext context)
			: base(info, context) {}
	}
}