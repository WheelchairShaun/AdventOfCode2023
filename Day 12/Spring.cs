using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_12
{
	public class Spring
	{
		private Dictionary<string, long> cache;

		public string Record { get; set; }
		public int[] Conditions { get; set; }
		public long Combinations { get; set; } = 0L;

		public Spring(string record, int[] conditions)
		{
			Record = record;
			Conditions = conditions;
			cache = new Dictionary<string, long>();
		}
		public void FindCombinations()
		{
			Combinations = 0L;

			Combinations += Calculate(Record, Conditions);
		}

		// Solution thanks to yfilipov, Learned a lot from this!
		// https://www.reddit.com/r/adventofcode/comments/18ge41g/2023_day_12_solutions/kd0u7ej/
		private long Calculate(string record, int[] conditions)
		{
			var key = $"{record},{string.Join(',', conditions)}";  // Cache key: spring pattern + group lengths

			if (cache.TryGetValue(key, out var value))
			{
				return value;
			}

			value = GetCount(record, conditions);
			cache[key] = value;

			return value;
		}

		long GetCount(string record, int[] conditions)
		{
			while (true)
			{
				if (conditions.Length == 0)
				{
					return record.Contains('#') ? 0 : 1; // No more conditions to match: if there are no record left, we have a match
				}

				if (string.IsNullOrEmpty(record))
				{
					return 0; // No more record to match, although we still have conditions to match
				}

				if (record.StartsWith('.'))
				{
					record = record.Trim('.'); // Remove all dots from the beginning
					continue;
				}

				if (record.StartsWith('?'))
				{
					return Calculate("." + record[1..], conditions) + Calculate("#" + record[1..], conditions); // Try both options recursively
				}

				if (record.StartsWith('#')) // Start of a group
				{
					if (conditions.Length == 0)
					{
						return 0; // No more conditions to match, although we still have a spring in the input
					}

					if (record.Length < conditions[0])
					{
						return 0; // Not enough characters to match the group
					}

					if (record[..conditions[0]].Contains('.'))
					{
						return 0; // Group cannot contain dots for the given length
					}

					if (conditions.Length > 1)
					{
						if (record.Length < conditions[0] + 1 || record[conditions[0]] == '#')
						{
							return 0; // Group cannot be followed by a spring, and there must be enough characters left
						}

						record = record[(conditions[0] + 1)..]; // Skip the character after the group - it's either a dot or a question mark
						conditions = conditions[1..];
						continue;
					}

					record = record[conditions[0]..]; // Last group, no need to check the character after the group
					conditions = conditions[1..];
					continue;
				}

				throw new Exception("Invalid input");
			}
		}

			private int TryCombinations(string possible, int[] condition, int position)
		{
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
			Record = string.Join('?', Enumerable.Repeat(Record, times));
			Conditions = Enumerable.Repeat(Conditions, times).SelectMany(c  => c).ToArray();
		}
	}
}
