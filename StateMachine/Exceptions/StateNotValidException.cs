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
		public TState State { get; private set; }

		/// <summary>
		/// Creates a new instance of this exception.
		/// </summary>
		public StateNotValidException() { }
		/// <summary>
		/// Creates a new instance of this exception with the default message.
		/// </summary>
		public StateNotValidException(TState state)
			: this(string.Format("State {0} is not registered.", state.ToString()))
		{
			State = state;
		}
		/// <summary>
		/// Creates a new instance of this exception with a given message.
		/// </summary>
		public StateNotValidException(string message) : base(message) { }
		/// <summary>
		/// Creates a new instance of this exception with the a given message and inner exception.
		/// </summary>
		public StateNotValidException(string message, Exception inner) : base(message, inner) { }
		/// <summary>
		/// Creates a new instance of this exception with serialization and context information.
		/// </summary>
		/// <param name="info">The serialization information.</param>
		/// <param name="context">The context information.</param>
		protected StateNotValidException(System.Runtime.Serialization.SerializationInfo info,
		                                 System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}