using System;
using System.Linq;

namespace Dungeon
{
    public class DungeonTask

    {
        private static MoveDirection MoveTo(Point start, Point finish)
        {
            var point = new Point(finish.X - start.X, finish.Y - start.Y);
            if (point.X == 1) return MoveDirection.Right;
            else if (point.X == -1) return MoveDirection.Left;
            else if (point.Y == 1) return MoveDirection.Down;
            else return MoveDirection.Up;
        }

        public static MoveDirection[] FindShortestPath(Map map)
        {
            Point[] chestsArray = map.Chests;
            Point startPoint = map.InitialPosition;
            Point finishPoint = map.Exit;
           

            var pathNoChest = BfsTask.FindPaths(map, startPoint, new Point[] { finishPoint }).FirstOrDefault();
            if (pathNoChest == null) return new MoveDirection[0];
            var moveToExit = pathNoChest.ToList();
            moveToExit.Reverse();
            if (chestsArray.Any(c => moveToExit.Contains(c)))
                return moveToExit.Zip(moveToExit.Skip(1), (MoveTo)).ToArray();
            var pathStartToChests = BfsTask.FindPaths(map, startPoint, chestsArray);
            var pathExitToChests = BfsTask.FindPaths(map, finishPoint, chestsArray).DefaultIfEmpty();
            if (pathStartToChests.FirstOrDefault() == null)
                return moveToExit.Zip(moveToExit.Skip(1), (MoveTo)).ToArray();
            var pathStartToExit = pathStartToChests.Join(pathExitToChests, f => f.Value, s => s.Value, (f, s) => new {
                Length = f.Length + s.Length,
                finishList = f.ToList(),
                startList = s.ToList()
            });
            var listsTuple = pathStartToExit.OrderBy(l => l.Length)
                .Select(v => Tuple.Create(v.finishList, v.startList)).First();
            listsTuple.Item1.Reverse();
            listsTuple.Item1.AddRange(listsTuple.Item2.Skip(1));
            return listsTuple.Item1.Zip(listsTuple.Item1.Skip(1), (MoveTo)).ToArray();
        }

        
    }
}