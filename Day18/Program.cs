using Day18;
using System.Text.RegularExpressions;

string instructionPattern = @"(D|L|R|U) (\d*) \((#[a-f|0-9]{6})\)";

var queue = new Queue<Instruction>(Regex.Matches(File.ReadAllText("DigPlan.txt"), instructionPattern)
	.Select(x => new Instruction(x.Groups[1].Value[0], long.Parse(x.Groups[2].Value), x.Groups[3].Value)).ToList());

var vertices = new List<(long x, long y)>() { (0,0) };
var b = 0L;

// Part 2
var cVertices = new List<(long x, long y)>() { (0, 0) };
var cb = 0L;

while (queue.Count > 0)
{
	var instruction = queue.Dequeue();

	(long row, long col) = vertices[^1];

	vertices.Add((row + instruction.Direction.x * instruction.Meters,
				col + instruction.Direction.y * instruction.Meters));

	(long cRow, long cCol) = cVertices[^1];

	cVertices.Add((cRow + instruction.CorrectedDirection.x * instruction.CorrectedMeters,
		cCol + instruction.CorrectedDirection.y * instruction.CorrectedMeters));

	b += instruction.Meters;
	cb += instruction.CorrectedMeters;
}

// Shoelace Formula
long area = ShoelaceFormula(vertices);

// Pick's Theorem
long i = PicksTheorem(area, b);

Console.WriteLine(i + b);


// Part 2
// Shoelace Formula
area = ShoelaceFormula(cVertices);

// Pick's Theorem
i = PicksTheorem(area, cb);


Console.WriteLine(i + cb);

long ShoelaceFormula(List<(long x, long y)> input)
{
	long area = 0L;

	for (int i = 0; i < vertices.Count - 1; i++)
	{
		var v = input[i];
		var nv = input[i + 1];

		area += (v.y + nv.y) * (v.x - nv.x);
	}

	return area / 2;
}

long PicksTheorem(long area, long points)
{
	return area - (points / 2) + 1;
}
