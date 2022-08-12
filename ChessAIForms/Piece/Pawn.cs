using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ChessAIForms
{
    public class Pawn : Piece 
    {
        public Pawn(Type type, Player player, int position) : base(type, player, position)
        {

        }

        public override List<int> LegalMoves(int tile, Board board)
        {
            List<int> legalMoves = MoveGen.GetPawnMoves(board.Pieces, tile);

            return legalMoves;
        }
    }
}
