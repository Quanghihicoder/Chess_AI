using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ChessAIForms
{
    public class King : Piece
    {
        public King(Type type, Player player, int position) : base(type, player, position)
        {
        }

        private List<int> offsets = new List<int>()
        {
            Vector.up, Vector.down, Vector.left, Vector.right,
            Vector.upLeft, Vector.upRight, Vector.lowLeft, Vector.lowRight
        };

        public override List<int> LegalMoves(int tile, Board board)
        {
            List<int> legalMoves = MoveGen.GetKingMoves(offsets, tile, board);

            return legalMoves;
        }

    }
}
