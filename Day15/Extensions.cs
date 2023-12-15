using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
	public static class Extensions
	{
		// Obtain the ASCII value of a character
		public static int ToAsciiValue(this char value)
		{
			return (int)value;
		}
	}
}
