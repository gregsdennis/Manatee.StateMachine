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
 
	File Name:		InputStream.cs
	Namespace:		Manatee.StateMachine
	Class Name:		InputStream<T>
	Purpose:		Represents a stream of input parameters for a StateMachine
					object.  Functions as a queue, but can be reset to the beginning
					of the collection.

***************************************************************************************/
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
