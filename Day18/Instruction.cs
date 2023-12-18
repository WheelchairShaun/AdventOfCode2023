using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
	public class Instruction
	{
        public (int x, int y) Direction { get; set; }
        public int Meters { get; set; }
        public string HexColor { get; set; } = string.Empty;

        public Instruction(char direction, int meters, string hexColor)
        {
			Direction = Directions.ParseDirection(direction);
			Meters = meters;
			HexColor = hexColor;
        }
    }
}
