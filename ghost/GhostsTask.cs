using System;
using System.Reflection.Metadata;
using System.Text;

namespace hashes;

public class GhostsTask :
	IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
	IMagic
{
	private byte[] arrayBytes = { 12, 47, 32, 64 };
    private Cat cat = new Cat("Murzik", "Maine Coon", new DateTime());
	private Vector vector1 = new Vector(1, 2);
	private Robot robot = new Robot("robot1", 31.12);
	private Vector vector2 = new Vector(2, 3);
	private Segment segment = new Segment(new Vector(2, 4), new Vector(3, 4));

    public void DoMagic()
	{
		cat.Rename("Barsik");
		vector1 = vector1.Add(vector2);
        segment.Start.Add(vector1);
		arrayBytes[0] = 1;
        Robot.BatteryCapacity -= 5;
    }
    #region
    Vector IFactory<Vector>.Create()
	{
		return vector1;
	}

	Segment IFactory<Segment>.Create()
	{
		return segment;
	}

    Document IFactory<Document>.Create()
	{
        return new Document("Hello world", Encoding.UTF8, arrayBytes);
    }

    Cat IFactory<Cat>.Create()
    {
        return cat;
    }

    Robot IFactory<Robot>.Create()
    {
        return robot;
    }
    #endregion
}