using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day3
{
	public class EngineSchematic
	{
		private List<string> _schematic;
		private List<PartNumber> _partNumbers;

        public EngineSchematic()
        {
            _schematic = new List<string>();
			_partNumbers = new List<PartNumber>();
        }

        public List<PartNumber> Parts 
		{
			get
			{
				return _partNumbers;
			}
		}

        public void AddBlankRow(int length)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < length + 2; i++)
			{
				sb.Append(".");
			}

			_schematic.Add(sb.ToString());
		}

		public void PrintSchematic()
		{
			for (int i = 0; i < _schematic.Count; i++)
			{
				Console.WriteLine(_schematic[i]);
			}
		}

		public void AddRow(string row)
		{
			_schematic.Add(row);
		}

		public void FindParts()
		{
			var digits = @"\d+";
			var symbols = @"[\*\#\+\$\%\@\/\&\-\=]";

			// Analyze every schematic line except the first and last lines
			for (int i = 1; i < _schematic.Count - 1; i++)
			{
				var line = _schematic[i];

				// Find all numbers on the current line
				var matches = Regex.Matches(line, digits);

				// Check if number is a valid part number
				foreach (Match match in matches)
				{
					if (isSymbolAdjacent(match, symbols, i))
					{
						PartNumber p = new PartNumber();
						p.Row = i;
						p.Length = match.Length;
						p.Column = match.Index;
						p.Value = int.Parse(match.Value);

						_partNumbers.Add(p);
					}
				}
			}
		}

		private bool isSymbolAdjacent(Match match, string input, int row)
		{
			// Check above match
			var above = _schematic[row - 1];
			var matches = Regex.Matches(above, input);

			foreach (Match m in matches)
			{
				// Check x postion
				if (m.Index >= match.Index - 1 && m.Index <= match.Index + match.Length)
				{
					return true;
				}
			}

			// Check left of match
			string left = _schematic[row][match.Index -1].ToString();

			if (Regex.IsMatch(left, input))
			{
				return true;
			}

			// Check right of match
			string right = _schematic[row][match.Index + match.Length].ToString();

			if (Regex.IsMatch(right, input))
			{
				return true;
			}

			// Check below match
			var below = _schematic[row + 1];
			matches = Regex.Matches(below, input);

			foreach (Match m in matches)
			{
				// Check x postion
				if (m.Index >= match.Index - 1 && m.Index <= match.Index + match.Length)
				{
					return true;
				}
			}

			return false;
		}

		public int CalculateGearRatio()
		{
			int result = 0;

			// Analyze every schematic line except the first and last lines
			for (int i = 1; i < _schematic.Count - 1; i++)
			{
				var line = _schematic[i];

				// Find all numbers on the current line
				var matches = Regex.Matches(line, @"\*");

				// Check if number is a valid part number
				foreach (Match gear in matches)
				{
					var ratio = CheckGearRatio(gear, i);
					result += ratio;
				}
			}

			return result;
		}

		private int CheckGearRatio(Match gear, int row)
		{
			int part1 = 0;
			int part2 = 0;

			foreach(PartNumber part in _partNumbers)
			{
				if (part.Row >= row - 1 && part.Row <= row + 1)
				{
					if (gear.Index >= part.Column - 1 && gear.Index <= part.Column + part.Length)
					{
						if (part1 > 0)
						{
							part2 = part.Value;
							break;
						}
						else
						{
							part1 = part.Value;
						}
					}
				}
			}

			return part1 * part2;


		}
	}
}
