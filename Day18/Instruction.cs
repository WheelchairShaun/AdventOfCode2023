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
        public (long x, long y) Direction { get; set; }
		public (long x, long y) CorrectedDirection { get; set; }
		public long Meters { get; set; }
		public long CorrectedMeters { get; set; }
		public string HexColor { get; set; } = string.Empty;

        public Instruction(char direction, long meters, string hexColor)
        {
			Direction = Directions.ParseDirection(direction);
			Meters = meters;
			HexColor = hexColor;

			DecipherHexColor(HexColor);

		}

		private void DecipherHexColor(string hexColor)
		{
			var h = hexColor.Remove(0, 1);
			CorrectedDirection = Directions.ParseDirection(h[^1]);
			CorrectedMeters = Convert.ToInt64(h.Substring(0, h.Length - 1), 16);
		}
	}
}
