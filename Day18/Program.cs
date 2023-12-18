using Day18;
using System.Text.RegularExpressions;

string instructionPattern = @"(D|L|R|U) (\d*) \((#[a-f|0-9]{6})\)";

var queue = new Queue<Instruction>(Regex.Matches(File.ReadAllText("DigPlan.txt"), instructionPattern)
	.Select(x => new Instruction(x.Groups[1].Value[0], int.Parse(x.Groups[2].Value), x.Groups[3].Value)).ToList());

var vertices = new List<(int x, int y)>() { (0,0) };
var b = 0;

while (queue.Count > 0)
{
	var instruction = queue.Dequeue();

	(int row, int col) = vertices[^1];

	vertices.Add((row + instruction.Direction.x * instruction.Meters,
				col + instruction.Direction.y * instruction.Meters));

	b += instruction.Meters;
}

// Shoelace Formula
int area = ShoelaceFormula();

// Pick's Theorem
int i = PicksTheorem(area);


Console.WriteLine(i + b);

int ShoelaceFormula()
{
	int area = 0;

	for (int i = 0; i < vertices.Count - 1; i++)
	{
		var v = vertices[i];
		var nv = vertices[i + 1];

		area += (v.y + nv.y) * (v.x - nv.x);
	}

	return area / 2;
}

int PicksTheorem(int area)
{
	return area - (b / 2) + 1;
}
