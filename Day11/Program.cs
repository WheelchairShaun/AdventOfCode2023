﻿var lines = File.ReadAllLines("Space.txt");

Console.WriteLine(SumGalaxyDistances(lines, 2));

var grow = 1000000;
Console.WriteLine(SumGalaxyDistances(lines, grow));

long SumGalaxyDistances(string[] lines, int grow)
{
	var galaxies = ParseGalaxies(lines);
	ExpandGalaxies(galaxies, grow);

	var pairs = galaxies
		.SelectMany((g1, i) => galaxies.Skip(i + 1).Select(g2 => (g1, g2)));

	return pairs.Select(ManhattanDistance).Sum();
}

long ManhattanDistance(((int y, int x) a, (int y, int x) b) p)
	=> Math.Abs(p.a.x - p.b.x) + Math.Abs(p.a.y - p.b.y);

void ExpandGalaxies((int y, int x)[] galaxies, int grow)
{
	grow--;

	// expand x
	var maxX = galaxies.Max(g => g.x);
	for (var x = 0; x < maxX; x++)
		if (galaxies.All(g => g.x != x))
		{
			for (var i = 0; i < galaxies.Length; i++)
				if (galaxies[i].x > x)
					galaxies[i] = (galaxies[i].y, galaxies[i].x + grow);
			maxX += grow; x += grow;
		}

	// expand y
	var maxY = galaxies.Max(g => g.y);
	for (var y = 0; y < maxY; y++)
		if (galaxies.All(g => g.y != y))
		{
			for (var i = 0; i < galaxies.Length; i++)
				if (galaxies[i].y > y)
					galaxies[i] = (galaxies[i].y + grow, galaxies[i].x);
			maxY += grow; y += grow;
		}
}

(int y, int x)[] ParseGalaxies(string[] lines)
	=> [.. lines
			.SelectMany((line, y) => line.Select((c, x) => (c, y, x)))
			.Where(t => t.c == '#')
			.Select(t => (t.y, t.x))];