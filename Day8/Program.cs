
var directions = new List<string>();
var map = new List<Node>();

ReadMapFromFile();

// Part 1
int steps = 0;
Node current = map.First<Node>(n => n.Location == "AAA");

while (current.Location != "ZZZ")
{
	foreach (string d in directions)
	{
		steps++;
		if (d == "L")
		{
			current = map.First<Node>(n => n.Location == current.Left);
		}
		else if (d == "R")
		{
			current = map.First<Node>(n => n.Location == current.Right);
		}

		//if (current.Location == "ZZZ")
		//	break;
	}
}
Console.WriteLine($"{steps} steps are required to reach ZZZ.");

// Part 2
List<Node> currents = map.FindAll(c => c.Location.EndsWith('A'));
long[] sets = new long[currents.Count];

// Calculate steps for each starting point
while (!AllLocationsEndWithZ(currents))
{
	foreach (string d in directions)
	{
		
		for (int i = 0; i< currents.Count; i++)
		{
			if (!currents[i].Location.EndsWith('Z'))
			{
				sets[i]++;
				if (d == "L")
				{
					currents[i] = map.First<Node>(n => n.Location == currents[i].Left);
				}
				else if (d == "R")
				{
					currents[i] = map.First<Node>(n => n.Location == currents[i].Right);
				}
			}
		}
	}
}

// Find Lowest Common Multiple

var lcm = 1L;

for (int i = 0; i < sets.Length; i++)
{
	lcm = LCM(lcm, sets[i]);
}

Console.WriteLine($"{lcm} steps before you're only on nodes that end with Z.");


long LCM(long a, long b)
{
	long num1, num2;

	if (a > b)
	{
		num1 = a;
		num2 = b;
	}
	else
	{
		num1 = b;
		num2 = a;
	}

	for (int i = 1; i <= num2; i++)
	{
		if ((num1 * i) % num2 == 0)
		{
			return i * num1;
		}
	}
	return num2;
}
bool AllLocationsEndWithZ(List<Node> locations)
{
	foreach(Node location in locations)
	{
		if (!location.Location.EndsWith('Z'))
			return false;
	}
	return true;
}

void ReadMapFromFile()
{
	var lines = File.ReadAllLines("Map.txt");
	int lineNumber = 0;

	// Add directions until empty line
	for (int i = lineNumber; i < lines.Length; i++)
	{
		lineNumber++;

		if (lines[i] == "")
		{
			break;
		}

		ParseDirections(lines[i]);
	}

	// Add nodes until end of file
	for (int i = lineNumber; i < lines.Length; i++)
	{
		ParseNodes(lines[i]);
	}
}

void ParseNodes(string line)
{
	var temp = line.Split(" = ");
	var n = new Node(temp[0]);

	var connections = temp[1].Substring(1, temp[1].Length - 2).Split(", ");
	n.Left = connections[0];
	n.Right = connections[1];

	map.Add(n);
}

void ParseDirections(string line)
{
	foreach (char d in line)
	{
		if (d == 'L' || d == 'R')
			directions.Add(d.ToString());
	}
}