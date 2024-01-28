using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot;

public partial class Bot
{
    public Rocket GetNextMove(Rocket rocket)
    {
        Task[] taskArray = new Task[threadsCount];
        var moves = new ConcurrentBag<Tuple<Turn, double>>();
        for (int i = 0; i < threadsCount; i++)
        {
            async Task Search()
            {
                await Task.Run(() =>
                {
                    var bestMove = SearchBestMove(rocket, new Random(), iterationsCount / threadsCount).ToTuple();
                    moves.Add(bestMove);
                });
            };
            taskArray[i] = Search();
            taskArray[i].Wait();
        }
        var resultMove = moves.OrderBy(x => x.Item2).First();
        return rocket.Move(resultMove.Item1, level);
    }

    public List<Task<(Turn Turn, double Score)>> CreateTasks(Rocket rocket)
    {
        return new() { Task.Run(() => SearchBestMove(rocket, new Random(random.Next()), iterationsCount)) };
    }
}