using System;

namespace func_rocket;

public class ControlTask
{
    private static double angle = 0.0;

	public static Turn ControlRocket(Rocket rocket, Vector target)
	{
        CalculateAngle(rocket, target);
        if (angle > 0) return Turn.Right;
        if (angle < 0) return Turn.Left;
        return Turn.None;
    }

    public static void CalculateAngle(Rocket rocket, Vector target)
    {
        Vector distance = target - rocket.Location;

        if (Check(rocket, distance)) angle = distance.Angle - (rocket.Velocity.Angle + rocket.Direction) / 2;
        else angle = distance.Angle - rocket.Direction;
    }

    private static bool Check(Rocket rocket, Vector distance)
    {
        bool a = Math.Abs(rocket.Direction - distance.Angle) < 0.5;
        bool b = Math.Abs(rocket.Velocity.Angle - distance.Angle) < 0.5;
        return a || b;
    }
}