﻿// See https://aka.ms/new-console-template for more information
using Day_12;
//using System.Linq;

//var lines = File.ReadAllLines("Records.txt");

//var springs = (from line in lines
//			   let temp = line.Split(' ')
//			   let c = temp[1]?.Split(',').Select(int.Parse).ToArray()
//			   select new Spring(temp[0], c))
//			   .ToList();

//springs.ForEach(spring => spring.FindCombinations());

//var score = springs.Sum(s => s.Combinations);

//Console.WriteLine(score);

//springs.ForEach(spring => spring.Unfold(5));
//springs.ForEach(spring => spring.FindCombinations());

//score = springs.Sum(s => s.Combinations);

//Console.WriteLine(score);

var input = File.ReadAllLines("Records.txt");

var part1 = 0L;
var part2 = 0L;
var cache = new Dictionary<string, long>();

foreach (var line in input.Select(l => l.Split(' ')))
{
	var springs = line[0];
	var groups = line[1].Split(',').Select(int.Parse).ToList();

	part1 += Calculate(springs, groups);

	springs = string.Join('?', Enumerable.Repeat(springs, 5));
	groups = Enumerable.Repeat(groups, 5).SelectMany(g => g).ToList();

	part2 += Calculate(springs, groups);
}

Console.WriteLine($"Part1: {part1}");
Console.WriteLine($"Part2: {part2}");
return;

long Calculate(string springs, List<int> groups)
{
	var key = $"{springs},{string.Join(',', groups)}";  // Cache key: spring pattern + group lengths

	if (cache.TryGetValue(key, out var value))
	{
		return value;
	}

	value = GetCount(springs, groups);
	cache[key] = value;

	return value;
}

long GetCount(string springs, List<int> groups)
{
	while (true)
	{
		if (groups.Count == 0)
		{
			return springs.Contains('#') ? 0 : 1; // No more groups to match: if there are no springs left, we have a match
		}

		if (string.IsNullOrEmpty(springs))
		{
			return 0; // No more springs to match, although we still have groups to match
		}

		if (springs.StartsWith('.'))
		{
			springs = springs.Trim('.'); // Remove all dots from the beginning
			continue;
		}

		if (springs.StartsWith('?'))
		{
			return Calculate("." + springs[1..], groups) + Calculate("#" + springs[1..], groups); // Try both options recursively
		}

		if (springs.StartsWith('#')) // Start of a group
		{
			if (groups.Count == 0)
			{
				return 0; // No more groups to match, although we still have a spring in the input
			}

			if (springs.Length < groups[0])
			{
				return 0; // Not enough characters to match the group
			}

			if (springs[..groups[0]].Contains('.'))
			{
				return 0; // Group cannot contain dots for the given length
			}

			if (groups.Count > 1)
			{
				if (springs.Length < groups[0] + 1 || springs[groups[0]] == '#')
				{
					return 0; // Group cannot be followed by a spring, and there must be enough characters left
				}

				springs = springs[(groups[0] + 1)..]; // Skip the character after the group - it's either a dot or a question mark
				groups = groups[1..];
				continue;
			}

			springs = springs[groups[0]..]; // Last group, no need to check the character after the group
			groups = groups[1..];
			continue;
		}

		throw new Exception("Invalid input");
	}
}
