using System.Linq;

var input = File.ReadAllText("patterns.txt");

var temp = input.Split("\r\n\r\n");

var grids = new List<char[,]>();

foreach (var (lines, rows, cols, grid) in from string t in temp
									  let lines = t.Split("\r\n", StringSplitOptions.TrimEntries)
									  let rows = lines.Length
									  let cols = lines.First().Length
									  let grid = new char[cols, rows]
									  select (lines, rows, cols, grid))
{
	for (int row = 0; row < rows; row++)
	{
		for (int col = 0; col < cols; col++)
		{
			grid[col, row] = lines[row].ElementAt(col);
		}
	}

	grids.Add(grid);
}

var summary = 0;

foreach (var grid in grids)
{
	FindVerticalSymmetry(grid);
	FindHorizontalSymmetry(grid);
}

Console.WriteLine(summary.ToString());

summary = 0;

foreach (var grid in grids)
{
	FindVerticalSymmetry(grid, true);
	FindHorizontalSymmetry(grid, true);
}

Console.WriteLine(summary.ToString());

void FindVerticalSymmetry(char[,] grid, bool part2 = false)
{
	var columns = grid.GetLength(0);

	for(int col = 0; col < columns - 1; col++)
	{
		var match = true;
		var smudges = 0;
		for (int next = 0; next < columns; next++)
		{
			var left = col - next;
			var right = col + 1 + next;

			if (0 <= left && left < right && right < columns)
			{
				for(int row = 0; row < grid.GetLength(1); row++)
				{
					if (grid[left, row] != grid[right, row])
					{
						match = false;
						smudges++;
					}
				}
			}
		}

		if (!part2 && match)
		{
			summary += col + 1;
		}

		if (part2 && smudges == 1)
		{
			summary += col + 1;
		}
	}
}

void FindHorizontalSymmetry(char[,] grid, bool part2 = false)
{
	var rows = grid.GetLength(1);

	for (int row = 0; row < rows - 1; row++)
	{
		var match = true;
		var smudges = 0;

		for (int next = 0; next < rows; next++)
		{
			var above = row - next;
			var below = row + 1 + next;

			if (0 <= above && above < below && below < rows)
			{
				for (int col = 0; col < grid.GetLength(0); col++)
				{
					if (grid[col, above] != grid[col, below])
					{
						match = false;
						smudges++;
					}
				}
			}
		}

		if (!part2 && match)
		{
			summary += 100 * (row + 1);
		}

		if (part2 && smudges == 1)
		{
			summary += 100 * (row + 1);
		}
	}
}

void Print()
{
	if (grids?.Count > 0)
	{
		foreach (var g in grids)
		{
			for (int y = 0; y < g.GetLength(1); y++)
			{
				for (int x = 0; x < g.GetLength(0); x++)
				{
					Console.Write(g[x, y]);
				}
				Console.Write('\n');
			}

			Console.WriteLine();
		}
	}
}
