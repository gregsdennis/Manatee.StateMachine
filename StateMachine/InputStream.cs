using System;
using System.Collections.Generic;
using System.Reflection;

namespace Manatee.StateMachine
{
	/// <summary>
	/// Represents a stream of input parameters for a StateMachine object.  Functions as a queue,
	/// but can be reset to the beginning of the collection.
	/// </summary>
	/// <typeparam name="T">
	/// The object type used for the inputs in the StateMachine.
	/// </typeparam>
	public class InputStream<T> : List<T>
	{
		int _currentIndex;

		///<summary>
		/// Gets whether the current index is at the end of the stream.
		///</summary>
		public bool IsAtEnd { get { return _currentIndex == Count; } }

		/// <summary>
		/// A default constructor.
		/// </summary>
		public InputStream()
		{
			Reset();
		}

		/// <summary>
		/// Resets the enumerator index to the start.
		/// </summary>
		public void Reset()
		{
			_currentIndex = 0;
		}
		/// <summary>
		/// Retrieves the next input object.
		/// </summary>
		/// <returns>
		/// The next input object.
		/// </returns>
		public T Next()
		{
			return this[_currentIndex++];
		}
	}

}
