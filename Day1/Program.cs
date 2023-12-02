using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

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
		string pattern = @"([1-9]|one|two|three|four|five|six|seven|eight|nine)";

		//var matches = Regex.Matches(line, pattern);

		var first = ConvertWordToDigit(Regex.Match(line, pattern).Value);
		var last = ConvertWordToDigit(Regex.Match(line, pattern, RegexOptions.RightToLeft).Value);
		var result = int.Parse(first + last);

		return result;
	}

	private static string ConvertWordToDigit(string input)
	{
		if (input.Length == 1)
		{
			return input;
		}

		switch (input)
		{
			case "one":
				input = "1";
				break;
			case "two": 
				input = "2"; 
				break;
			case "three":
				input = "3";
				break;
			case "four":
				input = "4";
				break;
			case "five":

				input = "5";
				break;
			case "six":
				input = "6";
				break;
			case "seven":
				input = "7";
				break;
			case "eight":
				input = "8";
				break;
			case "nine":
				input = "9";
				break;
		};

		return input;
	}
}