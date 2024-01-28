using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
	public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
	{
        var queue = new Queue<double>();
        var count = 0;
        var sum = 0.0;
        foreach(var item in data)
        {
            var result = new DataPoint(item.X, item.OriginalY);
            if (count == windowWidth)
            {
                count--;
                sum -= queue.Dequeue();
            }
            queue.Enqueue(item.OriginalY);
            count++;
            sum += item.OriginalY;
            result = result.WithAvgSmoothedY(sum / count);
            yield return result;
        }
	}
}