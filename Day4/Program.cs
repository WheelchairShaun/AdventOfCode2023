using Day4;
using System.Text.RegularExpressions;

internal class Program
{
	private static List<ScratchCard> cards = new List<ScratchCard>();
	private static void Main(string[] args)
	{
		ReadCardsFromFile();

		var worth = cards.Sum(c => c.GetScore());
		Console.WriteLine($"The scratchcards are worth {worth} points.");
	}

	private static int ReadCardId(string input)
	{
		string pattern = @"\d+";

		var id = Regex.Match(input, pattern).Value;

		return int.Parse(id);
	}

	private static int[] ReadNumbers(string input)
	{
		var pattern = @"\d+";

		var t = Regex.Matches(input, pattern);

		int[] numbers = new int[t.Count];

		for (int i = 0; i < numbers.Length; i++)
		{
			numbers[i] = int.Parse(t[i].Value);
		}

		return numbers;
	}

	private static void ReadCardsFromFile()
	{
		var lines = File.ReadAllLines("cards.txt");

		foreach (var line in lines)
		{
			var t = line.Split(':');

			var id = ReadCardId(t[0]);

			var r = t[1].Trim().Split('|');

			var w = ReadNumbers(r[0].Trim());

			var n = ReadNumbers(r[1].Trim());

			cards.Add(new ScratchCard(id, w, n));
		}
	}
}