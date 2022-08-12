using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ChessAIForms
{
    public class Queen : Piece 
    {
        public Queen(Type type, Player player, int position) : base(type, player, position)
        {

        }

        public override List<int> LegalMoves(int tile, Board board)
        {
            List<int> legalMoves = MoveGen.GetQueenMoves(board.Pieces, tile);

            return legalMoves;
        }
    }
}
