using System;
namespace XamarinTestApp
{
	public static class MathUtils
	{
		public static double GetRandomNumber(double minimum, double maximum)
		{
			Random random = new Random();
			return random.NextDouble() * (maximum - minimum) + minimum;
		}

		public static double GetRandomSign()
		{
			Random random = new Random();
			var sign = random.Next(0, 2) * 2 - 1; // Gives 1 or -1
			return sign;
		}
	}
}
