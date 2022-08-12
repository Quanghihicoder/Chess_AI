using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ChessAIForms
{
    public class Knight : Piece
    {
        public Knight(Type type, Player player, int position) : base(type, player, position)
        {

        }

        private List<int> _offsets = new List<int>()
        {
            Vector.nUpLeft, Vector.nUpRight, Vector.nDownLeft, Vector.nDownRight,
            Vector.nLeftUp, Vector.nLeftDown, Vector.nRightUp, Vector.nRightDown
        };

        public override List<int> LegalMoves(int tile, Board board)
        {
            List<int> legalMoves = MoveGen.GetKnightMoves(_offsets, board.Pieces, tile);

            return legalMoves;
        }
    }
}
