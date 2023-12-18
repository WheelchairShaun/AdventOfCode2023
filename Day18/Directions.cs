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
				case '3':
					return Up;
				case 'R':
				case '0':
					return Right;
				case 'D':
				case '1':
					return Down;
				case 'L':
				case '2':
					return Left;
				default:
					return (0, 0);
			}
		}
	}
}
