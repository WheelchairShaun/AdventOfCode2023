using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
	public class Pipe
	{
		public Pipe(char piece)
		{
			Piece = piece;

			ConvertPieceToDirections();
		}

		public char Piece { get; }

		public bool Traversed { get; set; } = false;

		public bool StartPoint { get; set; } = false;

        public bool MoveNorth { get; set; } = false;
		public bool MoveSouth { get; set; } = false;
		public bool MoveEast { get; set; } = false;
		public bool MoveWest { get; set; } = false;

		public bool ReceiveNorth { get; set; } = false;
		public bool ReceiveSouth { get; set; } = false;
		public bool ReceiveEast { get; set; } = false;
		public bool ReceiveWest { get; set; } = false;


		private void ConvertPieceToDirections()
		{
			MoveNorth = "S|JL".Contains(Piece);
			MoveSouth = "S|7F".Contains(Piece);
			MoveEast = "S-LF".Contains(Piece);
			MoveWest = "S-J7".Contains(Piece);

			ReceiveNorth = "|F7".Contains(Piece);
			ReceiveSouth = "|JL".Contains(Piece);
			ReceiveEast = "-J7".Contains(Piece);
			ReceiveWest = "-LF".Contains(Piece);

			StartPoint = "S".Contains(Piece);
		}
	}
}
