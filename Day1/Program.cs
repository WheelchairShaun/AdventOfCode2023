using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1;

public class Program
{
	private static List<int> _calibrations = new List<int>();

	public static void Main(string[] args)
	{
		ReadCalibrationsFromFile();

		var sum = _calibrations.Sum();
		Console.WriteLine($"The sum of all of the calibration values is {sum}");
	}

	private static void ReadCalibrationsFromFile()
	{
		// Read calibration lines from document
		var lines = File.ReadAllLines("CalibrationDocument.txt");

		// Parse calibrations
		foreach (var line in lines)
		{
			var calibration = ParseCalibrationFromLine(line);

			// Add calibration to list
			_calibrations.Add(calibration);
		}
	}

	private static int ParseCalibrationFromLine(string line)
	{
		char[] numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

		var first = line[line.IndexOfAny(numbers)];
		var second = line[line.LastIndexOfAny(numbers)];

		var result = int.Parse(String.Concat(first, second));

		return result;
	}
}
