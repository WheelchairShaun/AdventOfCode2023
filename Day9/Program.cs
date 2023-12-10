
using System.Text.RegularExpressions;

var readings = new List<int[]>();

ReadHistoryFromFile();

var one = readings.Sum(r => ExtrapolateNextValue(r));

Console.WriteLine($"The sum of these extrapolated values from Part 1 is {one}.");

var two = readings.Sum(r => ExtrapolatePreviousValue(r));

Console.WriteLine($"The sum of these extrapolated values from Part 2 is {two}.");

int ExtrapolateNextValue(IList<int> numbers)
{
	if (AreAllNumbersZero(numbers))
	{
		return 0;
	}

	return numbers[^1] + ExtrapolateNextValue(GetDifferences(numbers));
}

int ExtrapolatePreviousValue(IList<int> numbers)
{
	if (AreAllNumbersZero(numbers))
	{
		return 0;
	}

	return numbers[0] - ExtrapolatePreviousValue(GetDifferences(numbers));
}

bool AreAllNumbersZero(IList<int> numbers)
{
	return numbers.All(x => x == 0);
}

IList<int> GetDifferences(IList<int> numbers)
{
	return numbers.Skip(1).Select((x, i) => x - numbers[i]).ToArray();
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