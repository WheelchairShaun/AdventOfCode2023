namespace Day2
{
	public class Round
	{
        public Round(int red, int green, int blue)
        {
            Red = red;
			Green = green;
			Blue = blue;
        }

        public int Blue { get; init; }
        public int Red { get; init; }
		public int Green { get; init; }
    }
}