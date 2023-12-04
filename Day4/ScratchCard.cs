using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
	public class ScratchCard
	{
        public ScratchCard(int id, int[] winningNumbers, int[] numbers)
        {
            Id = id;
			WinningNumbers = winningNumbers;
			Numbers = numbers;
		}

		private int _id;

		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private int[] _numbers;

		public int[] Numbers
		{
			get { return _numbers; }
			set { _numbers = value; }
		}

		private int[] _winning;

		public int[] WinningNumbers
		{
			get { return _winning; }
			set { _winning = value; }
		}

		public int GetScore()
		{
			int n = Numbers.Intersect(WinningNumbers).Count();

			if (n > 0)
			{
				return (int) Math.Pow(2, n - 1);
			}

			return 0;
		}
	}
}
