using System.Collections.Generic;

namespace Rivals
{
    public class RivalsTask
    {
        private static Queue<OwnedLocation> AddRegionToQueue(Queue<OwnedLocation> queue, OwnedLocation location)
        {
            for (var dx = -1; dx <= 1; dx++)
                for (var dy = -1; dy <= 1; dy++)
                    if ((dy == 0 || dx == 0) && dy != dx)
                        queue.Enqueue(new OwnedLocation(location.Owner,
                        new Point(location.Location.X + dx, location.Location.Y + dy),
                        location.Distance + 1));
            return queue;
        }

        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            HashSet<Point> visited = new HashSet<Point>();
            Queue<OwnedLocation> queue = new Queue<OwnedLocation>();
            Point[] players = map.Players;

            for (int i = 0; i < players.Length; i++)
                queue.Enqueue(new OwnedLocation(i, players[i], 0));
            while (queue.Count > 0)
            {
                OwnedLocation location = queue.Dequeue();
                if (!map.InBounds(location.Location) ||
                    visited.Contains(location.Location) ||
                    map.Maze[location.Location.X, location.Location.Y] != MapCell.Empty)
                    continue;
                visited.Add(location.Location);
                yield return location;
                queue = AddRegionToQueue(queue, location);
            }
        }
    }
}

