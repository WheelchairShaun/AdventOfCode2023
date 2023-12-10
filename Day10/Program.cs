using Day10;
using System.Collections.Generic;
using System.Runtime.Serialization;

Pipe[,] maze;

ReadMazeFromFile();

var start = FindStart();

var farthest = GetFarthestPipe(start);

Console.WriteLine(farthest);

int GetFarthestPipe((int y, int x) start)
{
	var traversed = new List<(int, int)>()
	{
		start
	};

	var queue = new Queue<(int y, int x)>();
	queue.Enqueue(start);

	while (queue.Any())
	{
		(int y, int x) c = queue.Dequeue();
		var current = maze[c.y, c.x];

		
		// Try moving north
		if (c.y > 0 && current.MoveNorth && maze[c.y-1, c.x].ReceiveNorth && !traversed.Contains((c.y-1, c.x)))
		{
			traversed.Add((c.y, c.x));
			queue.Enqueue((c.y-1, c.x));
		}
		
		// Try moving south
		if (c.y < maze.GetLength(1) && current.MoveSouth && maze[c.y+1, c.x].ReceiveSouth && !traversed.Contains((c.y + 1, c.x)))
		{
			traversed.Add((c.y, c.x));
			queue.Enqueue((c.y + 1, c.x));
		}
	
		// Try moving east
		if (c.x < maze.GetLength(0) && current.MoveEast && maze[c.y, c.x + 1].ReceiveEast && !traversed.Contains((c.y, c.x + 1)))
		{
			traversed.Add((c.y, c.x));
			queue.Enqueue((c.y, c.x + 1));
		}

		// Try moving west
		if (c.x > 0 && current.MoveWest && maze[c.y, c.x - 1].ReceiveWest && !traversed.Contains((c.y, c.x - 1)))
		{
			traversed.Add((c.y, c.x));
			queue.Enqueue((c.y, c.x - 1));
		}
	}

	return traversed.Count / 2;
}

(int x, int y) FindStart()
{
	var s = (-1, -1);

	for (int i = 0; i < maze.GetLength(0); i++)
	{
		for (int j = 0; j < maze.GetLength(1); j++)
		{
			if (!maze[i, j].StartPoint)
			{ 
				continue;
			}

			s = (i, j);
			break;
		}

		if (s != (-1, -1))
		{
			break;
		}
	}

	return s;
}

void ReadMazeFromFile()
{
	// Convert file to string array
	var lines = File.ReadAllLines("Maze.txt").ToList();

	// Determine rows and columns
	maze = new Pipe[lines.Count(), lines.First().Length];

	for (var i = 0; i < maze.GetLength(0); i++)
	{
		for (var j = 0; j < maze.GetLength(1); j++)
		{
			maze[i, j] = new Pipe(lines.ToArray()[i][j]);
		}
	}
}