using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAIForms
{
    public class MoveWithBoardScore
    {
        public Move Move { get; set; }
        public int BoardScore { get; set; }
        public MoveWithBoardScore(int boardScore)
        {
            BoardScore = boardScore;
            Move = null;
        }
    }
}
