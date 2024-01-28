using System;

namespace func_rocket;

public class ForcesTask
{
	/// <summary>
	/// Создает делегат, возвращающий по ракете вектор силы тяги двигателей этой ракеты.
	/// Сила тяги направлена вдоль ракеты и равна по модулю forceValue.
	/// </summary>
	public static RocketForce GetThrustForce(double forceValue) => rocket =>
	{
		var x = Math.Cos(rocket.Direction) * forceValue;
		var y = Math.Sin(rocket.Direction) * forceValue;
		return new Vector(x,y);
	};

	/// <summary>
	/// Преобразует делегат силы гравитации, в делегат силы, действующей на ракету
	/// </summary>
	public static RocketForce ConvertGravityToForce(Gravity gravity, Vector spaceSize) => rocket =>
	gravity(spaceSize, rocket.Location);

	/// <summary>
	/// Суммирует все переданные силы, действующие на ракету, и возвращает суммарную силу.
	/// </summary>
	public static RocketForce Sum(params RocketForce[] forces)
	{
		return rocket =>
		{
			var summVector = new Vector(0, 0);
            foreach (var e in forces)
                summVector += e(rocket);
            return summVector;
        };
	}
}