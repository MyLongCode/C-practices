using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace func_rocket;

public class LevelsTask
{
    static readonly Physics standardPhysics = new();

    public static IEnumerable<Level> CreateLevels()
    {
        var rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
        var target = new Vector(600, 200);

        yield return new Level("Zero", rocket, target, (size, v) => Vector.Zero, standardPhysics);
        yield return new Level("Heavy", rocket, target, (size, v) => new Vector(0, 0.9), standardPhysics);
        yield return new Level("Up", rocket, new Vector(700,500),
            (size, v) => new Vector(0, -300 / (300 + (size.Y - v.Y))), standardPhysics);
        yield return new Level("WhiteHole", rocket, target,
            (size, v) => WhiteHole(v, target), standardPhysics);
        yield return new Level("BlackHole", rocket, target,
            (size, v) => BlackHole(v, target, rocket), standardPhysics);
        yield return new Level("BlackAndWhite", rocket, target,
            (size, v) => BlackAndWhite(v, target, rocket), standardPhysics);
    }

    public static Vector WhiteHole(Vector vector, Vector target)
    {
        var d = (target - vector).Length;
        return (target - vector).Normalize() * (-140 * d) / (d * d + 1);
    }

    public static Vector BlackHole(Vector vector, Vector target, Rocket rocket)
    {
        var blackHolePosition = (target + rocket.Location) / 2;
        var d = (blackHolePosition - vector).Length;
        return (blackHolePosition - vector).Normalize() * 300 * d / (d * d + 1);
    }

    public static Vector BlackAndWhite(Vector vector, Vector target, Rocket rocket)
    {
        var white = WhiteHole(vector, target);
        var black = BlackHole(vector, target, rocket);
        return (black + white) / 2;
    }
}