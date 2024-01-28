using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable<byte>
	{
		private byte[] array;
		private int hash;
		public int Length { get; }

		public ReadonlyBytes(params byte[] values)
		{
			array = values;
			try
			{
				Length = array.Count();
			}
			catch(Exception)
			{
				throw new ArgumentNullException();
			}
		}

		public byte this[int index]
		{
			get
			{
                if (index < 0 || index >= array.Count()) throw new IndexOutOfRangeException();
                return array[index];
			}
		}

		public override bool Equals(object obj)
		{
			var array2 = obj as ReadonlyBytes;
			if (array2 == null || array2.GetType() != typeof(ReadonlyBytes) 
				|| array.Length != array2.Length) return false;
			for(int i = 0; i < array2.Length; i++)
				if (array[i] != array2[i]) return false;
			return true;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				if (hash != 0) return hash;
				hash = 0;
				foreach(byte item in array)
					hash = hash * 1039 + item;
			}
            return hash;
        }

		public override string ToString()
		{
			var result = new StringBuilder();
			if (Length == 0) return "[]";
			result.Append("[");
			foreach(byte item in array)
				result.Append($"{item.ToString()}, ");
			result.Remove(result.Length - 2, 2);
			result.Append("]");
			return result.ToString();
		}

		public IEnumerator<byte> GetEnumerator()
		{
			foreach(byte item in array)
				yield return item;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}