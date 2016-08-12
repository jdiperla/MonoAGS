﻿using System;
using AGS.API;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;

namespace AGS.Engine
{
	public class AGSConcurrentHashSet<TItem> : IConcurrentHashSet<TItem>
	{
		//We're using a concurrent dictionary with a value we don't care about to simulate a hash set.
		private ConcurrentDictionary<TItem, byte> _map = new ConcurrentDictionary<TItem, byte>();

		public AGSConcurrentHashSet(int capacity = 5)
		{
			_map = new ConcurrentDictionary<TItem, byte> (2, capacity);
		}

		#region IConcurrentCollection implementation

		public int Count { get { return _map.Count; } }

		public bool Add(TItem item)
		{
			return _map.TryAdd(item, 0);
		}

		public bool Remove(TItem item)
		{
			byte weDontCare;
			return _map.TryRemove(item, out weDontCare);
		}

		public void RemoveAll(Predicate<TItem> shouldRemove)
		{
			foreach (var item in _map.Keys)
			{
				if (shouldRemove(item)) Remove(item);
			}
		}

		public bool Contains(TItem item)
		{
			return _map.ContainsKey(item);
		}

		public void Clear()
		{
			_map.Clear();
		}

		#endregion

		#region IEnumerable implementation

		public IEnumerator<TItem> GetEnumerator()
		{
			return _map.Keys.GetEnumerator();
		}

		#endregion

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)_map.Keys).GetEnumerator();
		}

		#endregion
	}
}
