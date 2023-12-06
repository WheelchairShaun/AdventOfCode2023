using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
	public class ToyBoatRace
	{
		private readonly long _time;
		private readonly long _distance;

		public ToyBoatRace(long time, long distance)
        {
			_time = time;
			_distance = distance;
		}

		public long Time => _time;

		public long Distance => _distance;

		public long[] GetPossibleVictories()
		{
			long[] results = new long[_time + 1];

			for (int held = 0; held <= _time; held++)
			{
				results[held] = CalculateDistance(held);
			}

			return results.Where(r => r > _distance).ToArray();
		}

		private long CalculateDistance(long held)
		{
			long travelTime = _time - held;

			return travelTime * held;
		}
	}
}
