﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public static class ExtensionsTask
{
	/// <summary>
	/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
	/// Медиана списка из четного количества элементов — это среднее арифметическое 
    /// двух серединных элементов списка после сортировки.
	/// </summary>
	/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
	public static double Median(this IEnumerable<double> items)
	{
		double median;
		var arrayItems = items.ToArray();
		int count = arrayItems.Count();
		Array.Sort(arrayItems);
		int halfIndex = count / 2;
		if (count == 0) throw new InvalidOperationException();
		else if (count % 2 == 0)
			median = (arrayItems[halfIndex] + arrayItems[halfIndex - 1]) / 2;
		else
			median = arrayItems[halfIndex];
		return median;
	}

	/// <returns>
	/// Возвращает последовательность, состоящую из пар соседних элементов.
	/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
	/// </returns>
	public static IEnumerable<(T First, T Second)> Bigrams<T>(this IEnumerable<T> items)
	{
		IEnumerable<(T First, T Second)> bigrams;
		T second = default(T);
		bool flag = true;
		foreach(var item in items)
		{
			if (flag)
			{
				second = item;
				flag = false;
				continue;
			}
			yield return (second, item);
			second = item;
		}
	}
}