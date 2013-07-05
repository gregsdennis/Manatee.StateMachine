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
 
	File Name:		ConcurrentDictionary.cs
	Namespace:		Manatee.StateMachine.Internal
	Class Name:		ConcurrentDictionary
	Purpose:		Add thread-safe functionality to an implementation of
					IDictionary<TKey, TValue>.

***************************************************************************************/

using System.Collections;
using System.Collections.Generic;

namespace Manatee.StateMachine.Internal
{
	internal class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private readonly object _lock;
		private readonly IDictionary<TKey, TValue> _innerDictionary;

		public ConcurrentDictionary(IDictionary<TKey, TValue> innerDictionary)
		{
			_innerDictionary = innerDictionary;
			_lock = new object();
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			lock (_lock)
			{
				return _innerDictionary.GetEnumerator();
			}
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			lock (_lock)
			{
				_innerDictionary.Add(item);
			}
		}
		public void Clear()
		{
			lock (_lock)
			{
				_innerDictionary.Clear();
			}
		}
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			lock (_lock)
			{
				return _innerDictionary.Contains(item);
			}
		}
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			lock (_lock)
			{
				_innerDictionary.CopyTo(array, arrayIndex);
			}
		}
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			lock (_lock)
			{
				return _innerDictionary.Remove(item);
			}
		}
		public int Count
		{
			get
			{
				lock (_lock)
				{
					return _innerDictionary.Count;
				}
			}
		}
		public bool IsReadOnly
		{
			get
			{
				lock (_lock)
				{
					return _innerDictionary.IsReadOnly;
				}
			}
		}
		public bool ContainsKey(TKey key)
		{
			lock (_lock)
			{
				return _innerDictionary.ContainsKey(key);
			}
		}
		public void Add(TKey key, TValue value)
		{
			lock (_lock)
			{
				_innerDictionary.Add(key, value);
			}
		}
		public bool Remove(TKey key)
		{
			lock (_lock)
			{
				return _innerDictionary.Remove(key);
			}
		}
		public bool TryGetValue(TKey key, out TValue value)
		{
			lock (_lock)
			{
				return _innerDictionary.TryGetValue(key, out value);
			}
		}
		public TValue this[TKey key]
		{
			get
			{
				lock (_lock)
				{
					return _innerDictionary[key];
				}
			}
			set
			{
				lock (_lock)
				{
					_innerDictionary[key] = value;
				}
			}
		}
		public ICollection<TKey> Keys
		{
			get
			{
				lock (_lock)
				{
					return _innerDictionary.Keys;
				}
			}
		}
		public ICollection<TValue> Values
		{
			get
			{
				lock (_lock)
				{
					return _innerDictionary.Values;
				}
			}
		}
	}
}