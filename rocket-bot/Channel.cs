using HarfBuzzSharp;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

namespace rocket_bot;

public class Channel<T> where T : class
{
	private readonly List<T> values = new List<T>();
	public T this[int index]
	{
		get
		{
			lock(values)
			{
				if (Count > index) return values[index];
				else return null;
			}
		}
		set
		{
			lock (values)
			{
				if(Count == index) values.Add(value);
				else if(Count > index)
				{
					values[index] = value;
					values.RemoveRange(index + 1, Count - index - 1);
				}
			}
		}
	}

	public T LastItem()
	{
		lock (values)
		{
			if(Count != 0) return values.Last();
			else return null;
		}
	}

	public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
	{
		lock (values)
		{
			if(knownLastItem == LastItem()) values.Add(item);
		}
	}

	public int Count
	{
		get
		{
			lock (values)
			{
				return values.Count;
			}
		}
	}
}