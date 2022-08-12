using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ChessAIForms
{
    public abstract class Piece
    {
        private Type _piece;
        private Player _player;
        private bool _moved;

        public Coordinate _position;

        public Type GetPiece { get => _piece;}
        public Player Player { get => _player;}
        public bool Moved { get => _moved;}

        public void SetMoved(bool moved)
        {
            _moved = moved;
        }

        public void ChangePosition(int position)
        {
            _position = new Coordinate(position);
        }

        public Piece(Type piece, Player player, int position)
        {
            _position = new Coordinate(position);
            _player = player;
            _piece = piece;
            _moved = false;

        }

        /// <summary>
        /// Get piece legal movements
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public abstract List<int> LegalMoves(int tile, Board board);


    }
}
