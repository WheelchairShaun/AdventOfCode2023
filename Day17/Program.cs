
var input = File.ReadAllLines("Map.txt");

int[,] grid;

var _width = 0;
var _height = 0;

(int, int) North = (0, -1);

(int, int) East = (1, 0);

(int, int) South = (0, 1);

(int, int) West = (-1, 0);

ParseInput();
Console.WriteLine(Solve(0, 3));
Console.WriteLine(Solve(4, 10));

int Solve(int minSteps, int maxSteps)
{
	var queue = new PriorityQueue<(int X, int Y, (int Dx, int Dy) Direction, int Steps), int>();

	var visited = new HashSet<string>();

	queue.Enqueue((0, 0, East, 1), 0);
	queue.Enqueue((0, 0, South, 1), 0);

	var directions = new List<(int Dx, int Dy)>();

	while (queue.TryDequeue(out var item, out var cost))
	{
		if (item.X == _width - 1 && item.Y == _height - 1 && item.Steps >= minSteps - 1)
		{
			return cost;
		}

		directions.Clear();

		if (item.Steps < minSteps - 1)
		{
			directions.Add(item.Direction);
		}
		else
		{
			GetDirections(directions, item.Direction);
		}

		foreach (var direction in directions)
		{
			var newSteps = direction == item.Direction ? item.Steps + 1 : 0;

			if (newSteps == maxSteps)
			{
				continue;
			}

			var (x, y) = (item.X + direction.Dx, item.Y + direction.Dy);

			if (x < 0 || x == _width || y < 0 || y == _height)
			{
				continue;
			}

			var key = $"{item.X},{item.Y},{x},{y},{newSteps}";

			if (visited.Add(key))
			{
				queue.Enqueue((x, y, direction, newSteps), cost + grid[x, y]);
			}
		}
	}

	return 0;
}

	void GetDirections(List<(int, int)> directions, (int, int) direction)
{
	if (direction != South)
	{
		directions.Add(North);
	}

	if (direction != West)
	{
		directions.Add(East);
	}

	if (direction != North)
	{
		directions.Add(South);
	}

	if (direction != East)
	{
		directions.Add(West);
	}
}

void ParseInput()
{
	_width = input[0].Length;

	_height = input.Length;

	grid = new int[_width, _height];

	for (var y = 0; y < _height; y++)
	{
		for (var x = 0; x < _width; x++)
		{
			grid[x, y] = input[y][x] - '0';
		}
	}
}
