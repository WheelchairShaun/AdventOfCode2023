using System;
using System.Text.RegularExpressions;

namespace Day2;

public class Day2
{
	private static List<Game> _games = new List<Game>();

	public static void Main(string[] args)
	{
		ReadGamesFromFile();

		var possibleGamesSum = _games.Where(g => g.Possible == true)
									 .Sum(g => g.Id);

		Console.WriteLine($"The sum of possible games in Part 1 is {possibleGamesSum}.");

		var sumOfPowers = CalculateSumOfPowers();

		Console.WriteLine($"The sum of the power of the minimum set of cubes in Part 2 is {sumOfPowers}.");
	}

	private static int CalculateSumOfPowers()
	{
		int result = 0;

		foreach (Game game in _games)
		{
			int minGems = 1;

			foreach (int gem in game.MinimumGems()) 
			{
				minGems *= gem;
			}

			result += minGems;
		}

		return result;
	}

	private static void ReadGamesFromFile()
	{
		var lines = File.ReadAllLines("GameResults.txt");

		foreach (var line in lines)  
		{
			// Parse the game
			var game = ParseLineToGame(line);

			// Add game to list
			_games.Add(game);
		}
	}

	private static Game ParseLineToGame(string line)
	{
		var temp = line.Split(":");

		var game = CreateGame(temp[0]);

		var handfulls = temp[1].Trim().Split(";");

		foreach (var handfull in handfulls)
		{
			game.AddRound(CreateRound(handfull));
		}

		return game;
	}

	private static Game CreateGame(string gameId)
	{
		string idPattern = @"\d+";

		var id = Regex.Match(gameId, idPattern).Value;

		return new Game(int.Parse(id));
	}

	private static Round CreateRound(string handfull)
	{
		int red = 0, green = 0, blue = 0;

		var gems = handfull.Trim().Split(",");

		foreach (string gem in gems)
		{
			var g = gem.Trim().Split(" ");

			int quantity = int.Parse(g[0]);
			string color = g[1];

			switch (color)
			{
				case "red":
					red = quantity;
					break;

				case "green":
					green = quantity;
					break;

				case "blue":
					blue = quantity;
					break;
			};
		}
		var round = new Round(red, green, blue);
		return round;
	}
}