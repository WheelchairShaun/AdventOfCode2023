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

		var totalCards = CalculateTotalScratchCards();
		Console.WriteLine($"You end up with {totalCards} scratchcards.");
	}

	private static int CalculateTotalScratchCards()
	{
		int total = 0;

		var winners = GetWinners(cards);

		foreach( var winner in winners )
		{
			for( int i = 0; i < winner.Instances; i++ )
			{
				AddCopies(winner);
			}
			
		}

		total = cards.Sum(c => c.Instances);

		return total;

	}

	private static void AddCopies(ScratchCard card)
	{
		int n = card.GetMatchesCount();
		int id = card.Id;

		for( int i = 0; i < n; i++ )
		{
			if (id + i < cards.Count)
			{
				cards[id + i].Instances++;
			}
		}
	}

	private static List<ScratchCard> GetWinners(List<ScratchCard> scratchCards)
	{
		return scratchCards.Where(c => c.GetMatchesCount() > 0).ToList();
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