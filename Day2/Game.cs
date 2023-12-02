using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
	public class Game
	{
		public Game(int id) 
		{
			Id = id;
			_rounds = new List<Round>();
		}

        public int Id { get; init; }

        private List<Round> _rounds;

		public List<Round> Rounds
		{
			get { return _rounds; }
			set { _rounds = value; }
		}

		private bool _possible = true;

		public bool Possible
		{
			get { return _possible; }
			set { _possible = value; }
		}


		public void AddRound(Round round)
		{
			if(round.Red > 12 || round.Green > 13 || round.Blue > 14)
			{
				Possible = false;
			}

			Rounds.Add(round);
		}

		public int[] MinimumGems()
		{
			int red = 0, green = 0, blue = 0;

			foreach(Round round in Rounds)
			{
				if (round.Red > red)
				{
					red = round.Red;
				}

				if (round.Green > green)
				{
					green = round.Green;
				}

				if(round.Blue > blue)
				{
					blue = round.Blue;
				}
			}

			return new int[] { red, green, blue };
		}
	}
}
