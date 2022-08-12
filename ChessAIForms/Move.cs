using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAIForms
{
    public class Move
    {
        public int Tile { get; set; }
        public int Next { get; set; }

        public Move() { }
        public Move(int tile, int next)
        {
            this.Tile = tile;
            this.Next = next;
        }
    }
}
