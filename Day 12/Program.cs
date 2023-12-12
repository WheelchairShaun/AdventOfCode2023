using Day_12;

var lines = File.ReadAllLines("Records.txt");

var springs = (from line in lines
			   let temp = line.Split(' ')
			   let c = temp[1]?.Split(',').Select(int.Parse).ToArray()
			   select new Spring(temp[0], c))
			   .ToList();

springs.ForEach(spring => spring.FindCombinations());

var score = springs.Sum(s => s.Combinations);

Console.WriteLine(score);

springs.ForEach(spring => spring.Unfold(5));
springs.ForEach(spring => spring.FindCombinations());

score = springs.Sum(s => s.Combinations);

Console.WriteLine(score);
