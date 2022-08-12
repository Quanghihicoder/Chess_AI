using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;

namespace ChessAIForms
{
    public class Board
    {
        private Piece[] _pieces = new Piece[64];
        private bool _checkMate = false;
        private Player _turn = Player.White;

        private int _firstThreeMoves = 0;

        public Piece[] Pieces { get => _pieces; set => _pieces = value; }
        public bool CheckMate { get => _checkMate; set => _checkMate = value; }
        public Player Turn { get => _turn; set => _turn = value; }
        
        /// <summary>
        /// Starting board
        /// </summary>
        public Board()
        {
            // White
            for (int i = 0; i < 8; i++)
                Pieces[8 + i] = new Pawn(Type.wPawn, Player.White, 8 + i);

            Pieces[0] = new Rook(Type.wRook, Player.White, 0);
            Pieces[1] = new Knight(Type.wKnight, Player.White, 1);
            Pieces[2] = new Bishop(Type.wBishop, Player.White, 2);
            Pieces[3] = new Queen(Type.wQueen, Player.White, 3);
            Pieces[4] = new King(Type.wKing, Player.White, 4);
            Pieces[5] = new Bishop(Type.wBishop, Player.White, 5);
            Pieces[6] = new Knight(Type.wKnight, Player.White, 6);
            Pieces[7] = new Rook(Type.wRook, Player.White, 7);

            //Black
            for (int i = 0; i < 8; i++)
                Pieces[48 + i] = new Pawn(Type.bPawn, Player.Black, 48 + i);

            Pieces[56] = new Rook(Type.bRook, Player.Black, 56);
            Pieces[57] = new Knight(Type.bKnight, Player.Black, 57);
            Pieces[58] = new Bishop(Type.bBishop, Player.Black, 58);
            Pieces[59] = new Queen(Type.bQueen, Player.Black, 59);
            Pieces[60] = new King(Type.bKing, Player.Black, 60);
            Pieces[61] = new Bishop(Type.bBishop, Player.Black, 61);
            Pieces[62] = new Knight(Type.bKnight, Player.Black, 62);
            Pieces[63] = new Rook(Type.bRook, Player.Black, 63);

            Turn = Player.White;

        }

        /// <summary>
        /// Get legal movements of a piece
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public static List<int> GetLegalMoves(int tile, Board board)
        {
            List<int> legalMoves = new List<int>();
            if (board.Pieces[tile] != null)
                legalMoves = board.Pieces[tile].LegalMoves(tile, board);

            return legalMoves;
        }

        /// <summary>
        /// Get legal movements of all pieces of faction
        /// </summary>
        /// <param name="player"></param>
        /// <param name="board"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<Move> GetAllLegalMoves(Player player, Board board, bool filter = true)
        {
            List<Move> allLegalMoves = new List<Move>();
            List<int> allPieces = GetPieces(board.Pieces, player);

            foreach (var tile in allPieces)
            {
                List<int> legalMoves = GetLegalMoves(tile, board);

                foreach (var move in legalMoves)
                {
                    allLegalMoves.Add(new Move(tile, move));  
                }
            }

            return filter == true ?
            MoveGen.FilterIlegalMoves(allLegalMoves, board) : allLegalMoves;
        }

        /// <summary>
        /// Get all pieces of faction
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static List<int> GetPieces(Piece[] pieces, Player player)
        {
            List<int> allPieces = new List<int>();

            for (int i = 0; i < pieces.Length; i++)
            {
                if (pieces[i] == null)
                {
                    continue;
                }
                else if (pieces[i].Player == player)
                {
                    allPieces.Add(i);
                }
            }
            return allPieces;
        }

        /// <summary>
        /// Move a piece
        /// </summary>
        /// <param name="board"></param>
        /// <param name="tile"></param>
        /// <param name="move"></param>
        public static void MovePiece(Board board, int tile, int move)
        {
            if (board.Pieces[tile] != null)
            {
                if (board.Pieces[tile].Moved == false)
                {
                    board.Pieces[tile].SetMoved(true);
                }


                board.Pieces[move] = board.Pieces[tile];
                board.Pieces[tile] = null;

                board.Pieces[move].ChangePosition(move);
                
                KingCastled(board, tile, move);
                PawnPromoted(board.Pieces, move);
            }
        }

        /// <summary>
        /// Make castling
        /// </summary>
        /// <param name="board"></param>
        /// <param name="tile"></param>
        /// <param name="move"></param>
        public static void KingCastled(Board board, int tile, int move)
        {
            if (tile == 4 || tile == 60)
            {
                if (board.Pieces[move].GetPiece == Type.wKing || board.Pieces[move].GetPiece == Type.bKing)
                {
                    int rank = board.Pieces[move].GetPiece == Type.wKing ? 7 : 63;

                    if (move == rank - 1 && board.Pieces[rank].Moved == false)
                        MovePiece(board, rank, rank - 2);

                    else if (move == rank - 5 && board.Pieces[rank-7].Moved == false)
                        MovePiece(board, rank - 7, rank - 4);
                }
            }
        }

        /// <summary>
        /// Yeah! Pawn becomes a Queen
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static bool PawnPromoted(Piece[] pieces, int move)
        {
            Coordinate position = new Coordinate(move);

            if(pieces[move].GetPiece == Type.wPawn && position.y == 7)
            {
                pieces[move] = new Queen(Type.wQueen, Player.White, move);
                return true;
            }

            else if (pieces[move].GetPiece == Type.bPawn && position.y == 0)
            {
                pieces[move] = new Queen(Type.bQueen, Player.Black, move);
                return true;
            }

            return false;
        }

        /// <summary>
        /// King is being checked
        /// </summary>
        /// <param name="king"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public static bool KingChecked(Type king, Board board)
        {
            MoveGen.ValidateCastling = false;

            Player opponent = king == Type.wKing ? Player.Black : Player.White;
            List<Move> legalMoves = Board.GetAllLegalMoves(opponent, board, false);

            MoveGen.ValidateCastling = true;
            foreach(Move move in legalMoves)
            {
                if(move.Next == Board.GetKingPosition(board.Pieces, king))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Where is the King?
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static int GetKingPosition(Piece[] pieces, Type piece)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                if(pieces[i] != null)
                {
                    if(pieces[i].GetPiece == piece)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Copy the current board
        /// </summary>
        /// <param name="oldBoard"></param>
        /// <returns></returns>
        public static Board CopyBoard(Board oldBoard)
        {
            Board newBoard = new Board();
            newBoard = ObjectExtensions.Copy(oldBoard);
            return newBoard;
        }

        /// <summary>
        /// Human makes movement
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="next"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool GetMove(int tile, int next, Board board)
        {
            bool valid = false;

            List<Move> legalMoves = GetAllLegalMoves(Player.White, board);

            foreach (var move in legalMoves)
               if (move.Tile == tile && move.Next == next)
                   valid = true;

            if (valid == false)
                return false;    

            MovePiece(board, tile, next);

            if (IsCheckmated(Player.White))
                return false;

            board.Save(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\book\learn.txt",tile,next,board);
            
            Turn = Player.Black;
            BlackTurn();
            return true;
        }

        /// <summary>
        /// AI makes movement
        /// </summary>
        public void BlackTurn()
        {
            AI ai = new AI(2); // pls set in range (1-3) {1,2,3}
            if (Turn == Player.Black)
            {
                Thread.Sleep(500);
                if (this._firstThreeMoves < 2)
                {
                    ai.EvaluateRandom(this);
                    this._firstThreeMoves += 1;
                }
                else
                {
                    ai.EvaluateAI(this);
                }

                if (IsCheckmated(Player.Black))
                    return;

                Turn = Player.White;
            }
        }

        /// <summary>
        /// Check mate !!! Win or Lose
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool IsCheckmated(Player player)
        {
            if (CheckMate == true)
            {
                Player winner = player == Player.White ? Player.Black : Player.White;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Save the movement for learning
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="tile"></param>
        /// <param name="next"></param>
        /// <param name="board"></param>
        public void Save(string fileName, int tile, int next, Board board)
        {
            StreamWriter writer = File.AppendText(fileName);
            try
            {
                string a;
                string b;

                if (tile < 10)
                {
                    a = "0";
                }
                else
                {
                    a = "";
                }

                if (next < 10)
                {
                    b = "0";
                }
                else
                {
                    b = "";
                }

                if (board.CheckMate == true)
                {
                    writer.WriteLine("End Game");
                }
                else
                {
                    writer.Write(board.Turn);
                    writer.Write("   ");
                    writer.Write(a + tile);
                    writer.Write("   ");
                    writer.Write(b + next);
                    writer.WriteLine("");
                }

            }
            finally
            {
                writer.Close();
            }
        }

        /// <summary>
        /// Save the status of the game
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="board"></param>
        public void SaveStatus(string fileName, Board board)
        {
            StreamWriter writer = File.AppendText(fileName);
            try
            {
                if (board._firstThreeMoves == 0)
                {
                    writer.WriteLine("New Game");
                    writer.WriteLine("");

                }
                if (board.CheckMate == true)
                {
                    writer.WriteLine("");
                    writer.WriteLine("End Game");
                    writer.WriteLine("");

                }
            }
            finally
            {
                writer.Close();
            }
        }

    }
}
