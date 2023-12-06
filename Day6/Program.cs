using Day6;
using System.Text.RegularExpressions;

var races = new List<ToyBoatRace>();
ToyBoatRace big;

ReadRacesFromFile();

long ways = 1;

foreach(var race in races)
{
	ways *= race.GetPossibleVictories().Length;
}

Console.WriteLine($"The number of ways you can beat the record is {ways}.\n");
Console.WriteLine($"The long race can be the record {big.GetPossibleVictories().Length} times.");

void ReadRacesFromFile()
{
	var lines = File.ReadAllLines("Races.txt");
	long[] times = new long[0];
	long[] distances = new long[0];
	string time = "", distance = "";

	foreach (var line in lines)
	{
		var t = line.Split(":");
		var p = @"\d+";


		if (t[0] == "Time")
		{
			var matches = Regex.Matches(t[1], p);

			if (matches.Count > 1)
			{
				Array.Resize<long>(ref times, matches.Count);

				for (int i = 0; i < matches.Count; i++)
				{
					times[i] = long.Parse(matches[i].Value);
					time += matches[i].Value;
				}
			}
		}
		else if(t[0] == "Distance"){
			var matches = Regex.Matches(t[1], p);

			if (matches.Count > 1)
			{
				Array.Resize<long>(ref distances, matches.Count);

				for (int i = 0; i < matches.Count; i++)
				{
					distances[i] = long.Parse(matches[i].Value);
					distance += matches[i].Value;
				}
			}
		}

		
	}

	if (times.Length == distances.Length)
	{
		for (int i = 0;i < times.Length; i++)
		{ 
			races.Add(new ToyBoatRace(times[i], distances[i]));
		}
	}

	big = new ToyBoatRace(long.Parse(time), long.Parse(distance));
}