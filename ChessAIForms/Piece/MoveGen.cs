using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAIForms
{
    public class MoveGen
    {
        public static bool ValidateCastling = true;

        /// <summary>
        /// When the king is checked, the player must escape the king from being checked
        /// </summary>
        /// <param name="moveList"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public static List<Move> FilterIlegalMoves(List<Move> moveList, Board board)
        {
            Type king = board.Turn == Player.White ? Type.wKing : Type.bKing;

            foreach (Move move in moveList.ToArray())
            {
                Board nextBoard = Board.CopyBoard(board);
                Board.MovePiece(nextBoard, move.Tile, move.Next);

                if (Board.KingChecked(king, nextBoard))
                    moveList.Remove(move);
                
            }

            if (moveList.Count == 0)
            {
                board.CheckMate = true;
            }
            
            return moveList;
        }

        // ------------------------ Piece Movement -----------------------------

        /// <summary>
        /// Get the King movements
        /// </summary>
        /// <param name="offsets"></param>
        /// <param name="tile"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public static List<int> GetKingMoves(List<int> offsets, int tile, Board board)
        {
            List<int> moveList = new List<int>();

            foreach (var offset in offsets)
            {
                moveList.Add(tile + offset);
            }

            foreach (var move in moveList.ToArray())
            {
                if (move < 0 || move > 63)
                {
                    moveList.Remove(move);
                }
                else if (board.Pieces[move] != null)
                {
                    if (board.Pieces[tile].Player == board.Pieces[move].Player)
                    {
                        moveList.Remove(move);
                    }
                }

                Coordinate tilePosition = new Coordinate(tile);
                Coordinate movePosition = new Coordinate(move);

                if (Math.Abs((int)(movePosition.x - tilePosition.x)) > 1)
                {
                    moveList.Remove(move);
                }
            }

            if (ValidateCastling == true)
            {
                int rank = board.Turn == Player.White ? 7 : 63;
                moveList.AddRange(GetCastling(tile, rank, board));
            }
            return moveList;
        }

        /// <summary>
        /// Castling 
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="rank"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public static List<int> GetCastling(int tile, int rank, Board board)
        {
            Type rook = rank == 7 ? Type.wRook : Type.bRook;
            List<int> moveList = new List<int>();

            if (board.Pieces[tile].Moved == false)
            {
                ValidateCastling = false;

                if(board.Pieces[rank] != null && board.Pieces[rank-1] == null && board.Pieces[rank - 2] == null)
                {
                    if (board.Pieces[rank].GetPiece == rook && board.Pieces[rank].Moved == false  
                        && TileAttacked(rank-1, board, false) == false && TileAttacked(rank-2, board,false) == false)
                    {
                        moveList.Add(rank - 1);
                    }
                }

                if (board.Pieces[rank - 7] != null && board.Pieces[rank - 6] == null && board.Pieces[rank - 5] == null)
                {
                    if (board.Pieces[rank - 7].GetPiece == rook && board.Pieces[rank - 7].Moved == false
                       && TileAttacked(rank - 5, board, false) == false && TileAttacked(rank - 4, board, false) == false)
                    {
                        moveList.Add(rank - 5);
                    }
                }

            }
            ValidateCastling = true;
            return moveList;
        }

        /// <summary>
        /// The tile (square) is under attack
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="board"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool TileAttacked(int tile, Board board, bool filter = true)
        {
            Player opponent = board.Turn == Player.White ? Player.Black : Player.White;
            List<Move> legalMoves = Board.GetAllLegalMoves(opponent, board, filter);


            foreach (var move in legalMoves)
            {
                if(move.Next == tile)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get the Knight movements
        /// </summary>
        /// <param name="offsets"></param>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static List<int> GetKnightMoves(List<int> offsets, Piece[] pieces, int tile)
        {
            List<int> moveList = new List<int>();

            foreach (var offset in offsets)
            {
                moveList.Add(tile + offset);
            }

            foreach (var move in moveList.ToArray())
            {
                if (move < 0 || move > 63)
                {
                    moveList.Remove(move);
                }

                else if (pieces[move] != null)
                {
                    if(pieces[tile].Player == pieces[move].Player)
                    {
                        moveList.Remove(move);
                    }
                }

                Coordinate tilePosition = new Coordinate(tile);
                Coordinate movePosition = new Coordinate(move);

                if (Math.Abs((int)(movePosition.x - tilePosition.x)) > 2)
                {
                    moveList.Remove(move);
                }
            }
            return moveList;
        }

        /// <summary>
        /// Get the Queen movements
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static List<int> GetQueenMoves(Piece[] pieces, int tile)
        {
            List<int> moveList = new List<int>();

            moveList.AddRange(up(pieces, tile));
            moveList.AddRange(down(pieces, tile));
            moveList.AddRange(left(pieces, tile));
            moveList.AddRange(right(pieces, tile));

            moveList.AddRange(upperLeft(pieces, tile));
            moveList.AddRange(upperRight(pieces, tile));
            moveList.AddRange(lowerLeft(pieces, tile));
            moveList.AddRange(lowerRight(pieces, tile));

            return moveList;
        }

        /// <summary>
        /// Get the Rook movements
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static List<int> GetRookMoves(Piece[] pieces, int tile)
        {
            List<int> moveList = new List<int>();

            moveList.AddRange(up(pieces, tile));
            moveList.AddRange(down(pieces, tile));
            moveList.AddRange(left(pieces, tile));
            moveList.AddRange(right(pieces, tile));

            return moveList;
        }

        /// <summary>
        /// Get the Bishop movements
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static List<int> GetBishopMoves(Piece[] pieces, int tile)
        {
            List<int> moveList = new List<int>();

            moveList.AddRange(upperLeft(pieces, tile));
            moveList.AddRange(upperRight(pieces, tile));
            moveList.AddRange(lowerLeft(pieces, tile));
            moveList.AddRange(lowerRight(pieces, tile));

            return moveList;
        }

        /// <summary>
        /// Get the Pawn movements
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public static List<int> GetPawnMoves(Piece[] pieces, int tile)
        {
            List<int> moveList = new List<int>();
            Coordinate position = new Coordinate(tile);

            if (pieces[tile].Player == Player.White)
            {
                if (position.y < 7)
                {
                    if (pieces[tile + Vector.up] == null)
                    {
                        moveList.Add(tile + Vector.up);

                        if (position.y == 1)
                        {
                            if (pieces[tile + Vector.up * 2] == null)
                                moveList.Add(tile + Vector.up * 2);
                        }

                    }
                }

                if (position.y < 7 && position.x > 0) 
                {
                    if (pieces[tile + Vector.upLeft] != null)
                    {
                        if (pieces[tile].Player != pieces[tile + Vector.upLeft].Player)
                            moveList.Add(tile + Vector.upLeft);
                    }
                }

                if (position.y < 7 && position.x < 7) 
                {
                    if (pieces[tile + Vector.upRight] != null)
                    {
                        if (pieces[tile].Player != pieces[tile + Vector.upRight].Player)
                            moveList.Add(tile + Vector.upRight);
                    }
                }
            }

            else if (pieces[tile].Player == Player.Black)
            {
                if (position.y > 0)
                {
                    if (pieces[tile + Vector.down] == null)
                    {
                        moveList.Add(tile + Vector.down);

                        if (position.y == 6)
                        {
                            if (pieces[tile + Vector.down * 2] == null)
                                moveList.Add(tile + Vector.down * 2);
                        }

                    }

                }

                if (position.y > 0 && position.x > 0) 
                {
                    if (pieces[tile + Vector.lowLeft] != null)
                    {
                        if (pieces[tile].Player != pieces[tile + Vector.lowLeft].Player)
                            moveList.Add(tile + Vector.lowLeft);
                    }
                }

                if (position.y > 0 && position.x < 7) 
                {
                    if (pieces[tile + Vector.lowRight] != null)
                    {
                        if (pieces[tile].Player != pieces[tile + Vector.lowRight].Player)
                            moveList.Add(tile + Vector.lowRight);
                    }
                }
            }

            return moveList;
        }

        /// <summary>
        /// Add up movement to the piece
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        private static List<int> up(Piece[] pieces, int tile)
        {
            List<int> legalMoves = new List<int>();
            Coordinate move = new Coordinate(tile);

            Player player = pieces[tile].Player;
            while(move.y < 7)
            {
                if (pieces[tile + Vector.up] == null)
                {
                    legalMoves.Add(tile + Vector.up);
                }
                else if (player != pieces[tile + Vector.up].Player)
                {
                    legalMoves.Add(tile + Vector.up);
                    break;
                }
                else break;

                tile += Vector.up;
                move = new Coordinate(tile);
            }
            return legalMoves;
        }

        /// <summary>
        /// Add down movement to the piece
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        private static List<int> down(Piece[] pieces, int tile)
        {
            List<int> legalMoves = new List<int>();
            Coordinate move = new Coordinate(tile);

            Player player = pieces[tile].Player;
            while (move.y > 0)
            {
                if (pieces[tile + Vector.down] == null)
                {
                    legalMoves.Add(tile + Vector.down);
                }
                else if (player != pieces[tile + Vector.down].Player)
                {
                    legalMoves.Add(tile + Vector.down);
                    break;
                }
                else break;

                tile += Vector.down;
                move = new Coordinate(tile);
            }
            return legalMoves;
        }

        /// <summary>
        /// Add left movement to the piece
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        private static List<int> left(Piece[] pieces, int tile)
        {
            List<int> legalMoves = new List<int>();
            Coordinate move = new Coordinate(tile);

            Player player = pieces[tile].Player;
            while (move.x > 0)
            {
                if (pieces[tile + Vector.left] == null)
                {
                    legalMoves.Add(tile + Vector.left);
                }
                else if (player != pieces[tile + Vector.left].Player)
                {
                    legalMoves.Add(tile + Vector.left);
                    break;
                }
                else break;

                tile += Vector.left;
                move = new Coordinate(tile);
            }
            return legalMoves;
        }

        /// <summary>
        /// Add right movement to the piece
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        private static List<int> right(Piece[] pieces, int tile)
        {
            List<int> legalMoves = new List<int>();
            Coordinate move = new Coordinate(tile);

            Player player = pieces[tile].Player;
            while (move.x < 7)
            {
                if (pieces[tile + Vector.right] == null)
                {
                    legalMoves.Add(tile + Vector.right);
                }
                else if (player != pieces[tile + Vector.right].Player)
                {
                    legalMoves.Add(tile + Vector.right);
                    break;
                }
                else break;

                tile += Vector.right;
                move = new Coordinate(tile);
            }
            return legalMoves;
        }

        /// <summary>
        /// Add up left movement to the piece
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        private static List<int> upperLeft(Piece[] pieces, int tile)
        {
            List<int> legalMoves = new List<int>();
            Coordinate move = new Coordinate(tile);

            Player player = pieces[tile].Player;
            while (move.x > 0 && move.y < 7)
            {
                if (pieces[tile + Vector.upLeft] == null)
                {
                    legalMoves.Add(tile + Vector.upLeft);
                }
                else if (player != pieces[tile + Vector.upLeft].Player)
                {
                    legalMoves.Add(tile + Vector.upLeft);
                    break;
                }
                else break;

                tile += Vector.upLeft;
                move = new Coordinate(tile);
            }
            return legalMoves;
        }

        /// <summary>
        /// Add up right movement to the piece
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        private static List<int> upperRight(Piece[] pieces, int tile)
        {
            List<int> legalMoves = new List<int>();
            Coordinate move = new Coordinate(tile);

            Player player = pieces[tile].Player;
            while (move.x < 7 && move.y < 7)
            {
                if (pieces[tile + Vector.upRight] == null)
                {
                    legalMoves.Add(tile + Vector.upRight);
                }
                else if (player != pieces[tile + Vector.upRight].Player)
                {
                    legalMoves.Add(tile + Vector.upRight);
                    break;
                }
                else break;

                tile += Vector.upRight;
                move = new Coordinate(tile);
            }
            return legalMoves;
        }

        /// <summary>
        /// Add down left movement to the piece
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        private static List<int> lowerLeft(Piece[] pieces, int tile)
        {
            List<int> legalMoves = new List<int>();
            Coordinate move = new Coordinate(tile);

            Player player = pieces[tile].Player;
            while (move.x > 0 && move.y > 0)
            {
                if (pieces[tile + Vector.lowLeft] == null)
                {
                    legalMoves.Add(tile + Vector.lowLeft);
                }
                else if (player != pieces[tile + Vector.lowLeft].Player)
                {
                    legalMoves.Add(tile + Vector.lowLeft);
                    break;
                }
                else break;

                tile += Vector.lowLeft;
                move = new Coordinate(tile);
            }
            return legalMoves;
        }

        /// <summary>
        /// Add down right movement to the piece
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        private static List<int> lowerRight(Piece[] pieces, int tile)
        {
            List<int> legalMoves = new List<int>();
            Coordinate move = new Coordinate(tile);

            Player player = pieces[tile].Player;
            while (move.x < 7 && move.y > 0)
            {
                if (pieces[tile + Vector.lowRight] == null)
                {
                    legalMoves.Add(tile + Vector.lowRight);
                }
                else if (player != pieces[tile + Vector.lowRight].Player)
                {
                    legalMoves.Add(tile + Vector.lowRight);
                    break;
                }
                else break;

                tile += Vector.lowRight ;
                move = new Coordinate(tile);
            }
            return legalMoves;
        }

    }
}
