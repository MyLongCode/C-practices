using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace yield;

public static class MovingMaxTask
{
	public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
	{
        var queue = new Queue<double>();
        var list = new LinkedList<double>();
        var number = 0.0;
        int count = 0;
        foreach (var item in data)
        {
            var result = new DataPoint(item.X, item.OriginalY);
            queue.Enqueue(item.OriginalY);
            count++;
            if (count > windowWidth)
            {
                count--;
                number = queue.Dequeue();
                if (number == list.First.Value)
                    list.RemoveFirst();
            }
            while (list.Count > 0 && item.OriginalY > list.Last.Value)
                list.RemoveLast();
            list.AddLast(item.OriginalY);
            result = result.WithMaxY(list.First.Value);
            yield return result;
        }
	}
}