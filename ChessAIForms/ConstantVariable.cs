using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAIForms
{
    public enum Player
    {
        Black,
        White,
    }
    public enum Type
    {
        wRook,
        wKnight,
        wBishop,
        wQueen,
        wKing,
        wPawn,

        bRook,
        bKnight,
        bBishop,
        bQueen,
        bKing,
        bPawn
    }
    
    public class Coordinate
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordinate(int location)
        {
            y = location / 8;
            x = location - y * 8;
        }
    }


    public struct Vector
    {
        // bishop, queen, pawn, rook, king moves
        public const int up = 8;
        public const int down = -8;
        public const int left = -1;
        public const int right = 1;
        public const int upLeft = 7;
        public const int upRight = 9;
        public const int lowLeft = -9;
        public const int lowRight = -7;

        // knight moves
        public const int nUpLeft = 15;
        public const int nUpRight = 17;
        public const int nDownLeft = -17;
        public const int nDownRight = -15;
        public const int nLeftUp = 6;
        public const int nLeftDown = -10;
        public const int nRightUp = 10;
        public const int nRightDown = -6;

    }



}
