using Greedy.Architecture;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Point = Greedy.Architecture.Point;

namespace Greedy
{
    public class DijkstraData
    {
        public int HighestPrice { get; set; }
        public Point? Previous { get; set; }
    }

    public class DijkstraPathFinder
    {
        private void UpdateCandidatesOpen(HashSet<Point> candidatesToOpen,
HashSet<Point> visitedNodes, IEnumerable<Point> incidentNodes)
        {
            foreach (Point node in incidentNodes)
            {
                if (!visitedNodes.Contains(node))
                    candidatesToOpen.Add(node);
            }
        }

        private void UpdateTrack(Dictionary<Point, DijkstraData> MainTrack,
IEnumerable<Point> incidentNodes, Point? toOpen, State state)
        {
            foreach (Point item in incidentNodes)
            {
                var price = state.CellCost[item.X, item.Y] + MainTrack[toOpen.Value].HighestPrice;
                if (!MainTrack.ContainsKey(item) || MainTrack[item].HighestPrice > price)
                    MainTrack[item] = new DijkstraData { Previous = toOpen, HighestPrice = price };
            }
        }

        private Point? FindNodeWithLowestPrice(HashSet<Point>
candidatesToOpen, Dictionary<Point, DijkstraData> MainTrack)
        {
            Point? toOpen = null;
            int maxScore = int.MaxValue;
            foreach (var candidate in candidatesToOpen)
            {
                if (MainTrack[candidate].HighestPrice < maxScore)
                {
                    maxScore = MainTrack[candidate].HighestPrice;
                    toOpen = candidate;
                }
            } 
            return toOpen;
        }

        public IEnumerable<PathWithCost>
GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
        {
            var pathChests = new HashSet<Point>(targets);
            var candidatesToOpen = new HashSet<Point>();
            var visitedNodes = new HashSet<Point>();
            var MainPath = new Dictionary<Point, DijkstraData>();
            MainPath[start] = new DijkstraData { HighestPrice = 0, Previous = null };
            candidatesToOpen.Add(start);
            while (true)
            {
                Point? toOpen = FindNodeWithLowestPrice(candidatesToOpen, MainPath);
                if (toOpen == null) yield break;
                if (pathChests.Contains(toOpen.Value)) yield return CreatePath(MainPath, toOpen.Value);
                var incidentNodes = GetIncidentNodes(toOpen.Value, state);
                UpdateCandidatesOpen(candidatesToOpen, visitedNodes, incidentNodes);
                UpdateTrack(MainPath, incidentNodes, toOpen, state);
                candidatesToOpen.Remove(toOpen.Value);
                visitedNodes.Add(toOpen.Value);
            }
        }
        private IEnumerable<Point> GetIncidentNodes(Point node, State state)
        {
            return new Point[]
            {
                new Point(node.X, node.Y+1),
                new Point(node.X, node.Y-1),
                new Point(node.X+1, node.Y),
                new Point(node.X-1, node.Y)
            }.Where(point => state.InsideMap(point) && !state.IsWallAt(point));
        }
        private PathWithCost CreatePath(Dictionary<Point, DijkstraData> MainTrack, Point end)
        {
            List<Point> resultList = new List<Point>();
            Point? currentPoint = end;
            while (currentPoint != null)
            {
                resultList.Add(currentPoint.Value);
                currentPoint = MainTrack[currentPoint.Value].Previous;
            }

            resultList.Reverse();
            PathWithCost resultPath = new PathWithCost(MainTrack[end].HighestPrice, resultList.ToArray());
            return resultPath;
        }
    }
}