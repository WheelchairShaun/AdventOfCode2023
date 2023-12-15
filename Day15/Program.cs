using Day15;
using System.Text.RegularExpressions;

string test = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";

var input = File.ReadAllText("Initialization.txt").Replace("\r\n", "");

var sequences = input.Split(',');

var sum = sequences.Sum(s => ToHash(s));

Console.WriteLine(sum);

var hashmap = new Dictionary<int, List<Lens>>();

foreach (var sequence in sequences)
{
	PerformOperation(sequence);
}

var power = 0;

foreach (var box in hashmap.Keys) { 
	if (hashmap[box].Count > 0)
	{
		for (int slot = 0;  slot < hashmap[box].Count; slot++)
		{
			power += CalculateFocusPower(box, slot, hashmap[box].ElementAt(slot).focalLength);
		}
	}
}

Console.WriteLine(power);

int ToHash(string sequence)
{
	var value = 0;

	foreach(char c in sequence)
	{
		value += c.ToAsciiValue();
		value = value * 17;
		value = value % 256;
	}

	return value;
}

void PerformOperation(string sequence)
{
	if (sequence.Contains('-'))
	{
		RemoveLens(sequence.Split('-'));
	}
	else if (sequence.Contains("="))
	{
		AddLens(sequence.Split("="));
	}
}

void AddLens(string[] input)
{
	var box = ToHash(input[0]);
	var lens = new Lens(input[0], int.Parse(input[1]));

	if (!hashmap.ContainsKey(box))
	{
		hashmap.Add(box, new List<Lens>());
	}

	if (!hashmap[box].Any(l => l.label == lens.label))
	{
		hashmap[box].Add(lens);
	}
	else
	{
		var pos = hashmap[box].FindIndex(l => l.label == lens.label);
		hashmap[box][pos] = lens;
	}
}

void RemoveLens(string[] input)
{
	var box = ToHash(input[0]);

	if (hashmap.ContainsKey(box) && hashmap[box].Any(l => l.label == input[0]))
	{
		var l = hashmap[box].FirstOrDefault(l => l.label == input[0]);
		hashmap[box].Remove(l);
	}
}

int CalculateFocusPower(int box, int slot, int focalLength)
{
	return (box + 1) * (slot + 1) * focalLength;
}

struct Lens
{
	public string label;
	public int focalLength;

	public Lens(string label, int focalLength)
	{
		this.label = label;
		this.focalLength = focalLength;
	}
}