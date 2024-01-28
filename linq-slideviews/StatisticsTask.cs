using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            return visits.OrderBy(s => s.DateTime)
                .GroupBy(s => s.UserId)
                .SelectMany(s => s.Bigrams().Where(x => x.Item1.SlideType == slideType))
                .Select(s => s.Item2.DateTime.Subtract(s.Item1.DateTime))
                .Where(x => x.TotalMinutes > 1 && x.TotalHours < 2)
                .Select(x => x.TotalMinutes)
                .DefaultIfEmpty()
                .Median();
        }
    }
}
