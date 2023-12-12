using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_12
{
	public class Spring
	{
		public string Record { get; set; }
		public int[] Conditions { get; set; }
		public int Combinations { get; set; } = 0;

		public Spring(string record, int[] conditions)
		{
			Record = record;
			Conditions = conditions;
		}

		public void FindCombinations()
		{
			Combinations += TryCombinations(Record, Conditions, 0);
		}

		private int TryCombinations(string possible, int[] condition, int position)
		{
			Console.WriteLine(possible);
			// Reached the end of the record
			if (position == possible.Length)
			{
				return IsValid(possible) ? 1 : 0;
			}

			if (possible[position] == '?')
			{
				// Try the next character with # and .
				var path1 = TryCombinations(possible.Substring(0, position) + '#' + possible.Substring(position + 1), condition, position + 1);
				var path2 = TryCombinations(possible.Substring(0, position) + '.' + possible.Substring(position + 1), condition, position + 1);
				return path1 + path2;
			}
			else
			{
				
				// Go to the next string position
				return TryCombinations(possible, condition, position + 1);
			}

			
		}

		private bool IsValid(string sequence)
		{
			var current = 0;
			var seen = new List<int>();
			foreach (char c in sequence)
			{
				if (c == '.')
				{
					if (current > 0)
						seen.Add(current);
					current = 0;
				}
				else if (c == '#')
				{
					current++;
				}
				else
				{
					return false;
				}
			}

			if (current > 0)
			{
				seen.Add(current);
			}

			return Conditions.SequenceEqual(seen);
		}

		public void Unfold(int times)
		{
			--times;
			StringBuilder sb = new StringBuilder(Record);
			int[] copy = (int[])Conditions.Clone();

			for (int i = 0; i < times; i++)
			{
				sb.Append('?' + Record);
				Conditions = Conditions.Concat(copy).ToArray();
			}

			Record = sb.ToString();
		}
	}
}
