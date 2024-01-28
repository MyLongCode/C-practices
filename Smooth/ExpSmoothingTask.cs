using System.Collections.Generic;

namespace yield
{
    public static class ExpSmoothingTask
    {
        public static IEnumerable<DataPoint> SmoothExponentialy
            (this IEnumerable<DataPoint> data, double alpha)
        {
            bool flag = true;
            double prevElem = 0.0;
            foreach (var item in data)
            {
                var result = new DataPoint(item.X, item.OriginalY);
                if (flag)
                {
                    prevElem = item.OriginalY;
                    result = result.WithExpSmoothedY(prevElem);
                    flag = false;
                    yield return result;
                }
                else
                {
                    prevElem = prevElem + alpha * (item.OriginalY - prevElem);
                    result = result.WithExpSmoothedY(prevElem);
                    yield return result;
                }
            }
        }
    }
}
