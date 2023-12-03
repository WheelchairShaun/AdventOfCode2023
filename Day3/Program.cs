
using System.Text;

namespace Day3;

public class Program
{
	public static EngineSchematic? schematic;

	public static void Main(string[] args)
	{
		schematic = new EngineSchematic();

		ReadSchematicFromFile();

		schematic.FindParts();

		var sum = schematic.Parts.Sum(p => p.Value);
		Console.WriteLine($"The sum of the parts is {sum}");

		var gearRatio = schematic.CalculateGearRatio();
		Console.WriteLine($"The gear ratio is {gearRatio}");
	}

	private static void ReadSchematicFromFile()
	{
		var lines = File.ReadAllLines("EngineSchematic.txt");

		var rowLength = lines[0].Count();

		schematic.AddBlankRow(rowLength);

		foreach (var line in lines)
		{
			StringBuilder sb = new StringBuilder(".");
			sb.Append(line);
			sb.Append('.');

			schematic.AddRow(sb.ToString());
		}

		schematic.AddBlankRow(rowLength);
	}
}