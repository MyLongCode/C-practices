	using System;
	using System.Collections.Generic;

	namespace LimitedSizeStack;

	public class LimitedSizeStack<T>
	{
		LinkedList<T> values = new LinkedList<T>();
		private int limit;
		public LimitedSizeStack(int undoLimit)
		{
			limit = undoLimit;
		}

		public void Push(T item)
		{
			values.AddFirst(item);
			if (values.Count > limit) values.RemoveLast();
		}

		public T Pop()
		{
			T result = values.First.Value;
			values.RemoveFirst();
			return result;
		}

		public int Count 
		{
			get { return values.Count; }
		}
	}