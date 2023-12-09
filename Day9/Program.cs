
using System.Text.RegularExpressions;

var readings = new List<int[]>();

ReadHistoryFromFile();

var one = readings.Sum(r => ExtrapolateNextValue(r));

Console.WriteLine($"Part 1: {one}");

var two = readings.Sum(r => ExtrapolatePreviousValue(r));

Console.WriteLine($"Part 2: {two}");

int ExtrapolateNextValue(IList<int> numbers)
{
	if (numbers.All(x => x == 0))
	{
		return 0;
	}

	var differences = numbers.Skip(1).Select((x, i) => x - numbers[i]).ToArray();

	return numbers[^1] + ExtrapolateNextValue(differences);
}

int ExtrapolatePreviousValue(IList<int> numbers)
{
	if (numbers.All(x => x == 0))
	{
		return 0;
	}

	var differences = numbers.Skip(1).Select((x, i) => x - numbers[i]).ToArray();

	return numbers[0] - ExtrapolatePreviousValue(differences);
}

void ReadHistoryFromFile()
{
	var lines = File.ReadAllLines("History.txt");
	var pattern = @"-?\d+";

	foreach (var line in lines)
	{
		var matches = Regex.Matches(line, pattern);

		int[] reading = new int[matches.Count];
		for (int i = 0; i < matches.Count; i++)
		{
			reading[i] = int.Parse(matches[i].Value);
		}

		readings.Add(reading);
	}
}