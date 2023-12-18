namespace Day18
{
	public static class Directions
	{
		public static (int, int) Up = (0, -1);

		public static (int, int) Right = (1, 0);

		public static (int, int) Down = (0, 1);

		public static (int, int) Left = (-1, 0);

		public static (int, int) ParseDirection(char direction)
		{
			switch (direction)
			{
				case 'U':
					return Up;
				case 'R':
					return Right;
				case 'D':
					return Down;
				case 'L':
					return Left;
				default:
					return (0, 0);
			}
		}
	}
}
