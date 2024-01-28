using System.Collections.Generic;
using System.Linq;

namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            var queue = new Queue<SinglyLinkedList<Point>>();
            var visited = new HashSet<Point>() { start };
            var pointStart = new SinglyLinkedList<Point>(start, null);
            var route = Walker.PossibleDirections.Select(p => new Point(p.X, p.Y));

            queue.Enqueue(pointStart);
            while (queue.Count != 0)
            {
                var result = queue.Dequeue();
                var point = result.Value;
                if (!map.InBounds(point)) continue;
                if (map.Dungeon[point.X, point.Y] == MapCell.Wall) continue;
                if (chests.Contains(point)) yield return result;

                foreach (var e in route)
                {
                    var p = new Point(point.X + e.X, point.Y + e.Y);
                    if (!visited.Contains(p))
                    {
                        queue.Enqueue(new SinglyLinkedList<Point>(p, result));
                        visited.Add(p);

                    }
                }
            }
        }
    }
}
