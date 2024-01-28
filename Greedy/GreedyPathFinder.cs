using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using System.Linq;
using Point = Greedy.Architecture.Point;

namespace Greedy
{
    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            List<Point> list = new List<Point>();
            var energy = state.InitialEnergy;
            DijkstraPathFinder pathFinder = new DijkstraPathFinder();
            List<Point> pathToGoal = new List<Point>();
            List<Point> chests = new List<Point>();
            chests.AddRange(state.Chests);
            if (chests.Count < state.Goal) 
                return list;
            while (state.Scores < state.Goal)
            {
                var toChestsPath = pathFinder.GetPathsByDijkstra(
                state, state.Position, chests);
                var toBestChestPath = toChestsPath.FirstOrDefault();
                if (Equals(toChestsPath.FirstOrDefault(), null)) return list;
                toBestChestPath = toChestsPath
                .SkipWhile(path => path.Path == new List<Point>())
                .FirstOrDefault();
                energy -= toBestChestPath.Cost;
                if (energy < 0) return list;
                pathToGoal.AddRange(toBestChestPath.Path.Skip(1));
                state.Position = toBestChestPath.Path.Last();
                chests.Remove(state.Position);
                state.Scores++;
            }

            return pathToGoal;
        }
    }
}
