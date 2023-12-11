using Day10;
using System.Collections.Generic;
using System.Runtime.Serialization;

Pipe[,] maze;

ReadMazeFromFile();

var start = FindStart();

var pipeloop = new List<Pipe>();

var farthest = GetFarthestPipe(start);




Console.WriteLine(farthest);

Print();

var inside = CalculateTotalEnclosedTiles();
Console.WriteLine(inside);

int CalculateTotalEnclosedTiles()
{
	var total = 0;

	var polygonVertices = pipeloop
		.Where(p =>
		{
			var val = maze[p.Coordinates.y, p.Coordinates.x];
			return val.Piece != '|' && val.Piece != '-';
		})
		.ToArray();

	for (var row = 0; row < maze.GetLength(0); row++)
	{
		for (var col = 0; col < maze.GetLength(1); col++)
		{
			var position = maze[row, col].Coordinates;

			// Random pipes that are not connected to the main loop also count
			if (!pipeloop.Any(p => p.Coordinates == position) && IsTileInsidePolygon(position, polygonVertices))
			{
				total++;
			}
		}
	}
	return total;
}

bool IsTileInsidePolygon((int y, int x) position, Pipe[] polygonVertices)
{
	// Ray Casting Algorithm to determine if a point is inside a polygon (pipe loop)
	var isInside = false;

	// Iterates over adjacent positions of the polygon
	// For each pair, it checks if the horizontal line extending to the right intersects with the polygon edge
	var j = polygonVertices.Length - 1;
	for (int i = 0; i < polygonVertices.Length; i++)
	{
		var vertexA = polygonVertices[i];
		var vertexB = polygonVertices[j];

		if (vertexA.Coordinates.y < position.y && vertexB.Coordinates.y >= position.y
			|| vertexB.Coordinates.y < position.y && vertexA.Coordinates.y >= position.y)
		{
			if (vertexA.Coordinates.x
					+ (position.y - vertexA.Coordinates.x)
					/ (vertexB.Coordinates.y - vertexA.Coordinates.y)
					* (vertexB.Coordinates.x - vertexA.Coordinates.x)
				< position.x)
			{
				isInside = !isInside;
			}
		}

		j = i;
	}

	return isInside;
}

void Print()
{
	var polygonVertices = pipeloop
		.Where(p =>
		{
			var val = maze[p.Coordinates.y, p.Coordinates.x];
			return val.Piece != '|' && val.Piece != '-';
		})
		.ToArray();

	for (var row = 0; row < maze.GetLength(0); row++)
	{
		for (var col = 0; col < maze.GetLength(1); col++)
		{
			var character = maze[row, col].Piece;
			var position = (row, col);

			if (pipeloop.Any(p => p.Coordinates == position))
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}
			else if (IsTileInsidePolygon(position, polygonVertices))
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}

			Console.Write(character);
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Black;
		}
		Console.WriteLine();
	}
}

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

		if (!pipeloop.Contains(current))
		{
			pipeloop.Add(current);
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
			maze[i, j] = new Pipe(lines.ToArray()[i][j], i, j);
		}
	}
}