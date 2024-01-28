using NUnit.Framework;

namespace rocket_bot;

[TestFixture]
public class BotTests_CorrectTaskCreation : BotTests_Base
{
	private const int CorrectnessIterations = 50;
	private const int CorrectnessMoves = 4;

	[TestCase(1)]
	[TestCase(2)]
	[TestCase(4)]
	public void CorrectTasksCount(int threadCount)
	{
		var level = new Level(rocket, new[] { new Vector(100, 90), new Vector(100, 0) },
			LevelsFactory.StandardPhysics);
		channel.AppendIfLastItemIsUnchanged(level.InitialRocket, null);

		var bot = new Bot(level, channel, CorrectnessMoves, CorrectnessIterations, random, threadCount);
		var tasks = bot.CreateTasks(level.InitialRocket);

		Assert.AreEqual(tasks.Count, threadCount);
	}
}