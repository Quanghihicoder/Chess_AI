using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace ChessAIForms
{
    public partial class Form1 : Form
    {
        Board board;
        private Piece SelectedPiece { get; set; }
        public Form1()
        {
            InitializeComponent();
            board = new Board();
            board.SaveStatus(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\book\learn.txt", board);
            updateUI();
        }
        /// <summary>
        /// Update the game information
        /// </summary>
        private void updateUI()
        {
            playerTurn.Text = board.Turn.ToString();
            if (SelectedPiece != null)
            {
                if (SelectedPiece.GetType() == typeof(Pawn))
                {
                    pieceselect.Text = "Pawn";
                }
                else if (SelectedPiece.GetType() == typeof(Rook))
                {
                    pieceselect.Text = "Rook";
                }
                else if (SelectedPiece.GetType() == typeof(Knight))
                {
                    pieceselect.Text = "Knight";
                }
                else if (SelectedPiece.GetType() == typeof(Bishop))
                {
                    pieceselect.Text = "Bishop";
                }
                else if (SelectedPiece.GetType() == typeof(Queen))
                {
                    pieceselect.Text = "Queen";
                }
                else if (SelectedPiece.GetType() == typeof(King))
                {
                    pieceselect.Text = "King";
                }
                selectX.Text = (SelectedPiece._position.x + 1).ToString();
                selectY.Text = (SelectedPiece._position.y + 1).ToString();
            }
            else
            {
                pieceselect.Text = "null";
                selectX.Text = "null";
                selectY.Text = "null";
            }
            if (board.CheckMate == true)
            {
                board.SaveStatus(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\book\learn.txt", board);
                if (board.Turn == Player.White)
                {
                    List<Move> list = Board.GetAllLegalMoves(Player.Black, board);
                    foreach(Move move in list)
                    {
                        if (move.Next == Board.GetKingPosition(board.Pieces, Type.wKing)) 
                        { 
                            MessageBox.Show("You Lose!");
                            this.Close();
                        }
                    }
                    MessageBox.Show("Draw!");
                    this.Close();
                }
                if (board.Turn == Player.Black)
                {
                    List<Move> list = Board.GetAllLegalMoves(Player.White, board);

                    foreach (Move move in list)
                    {
                        if (move.Next == Board.GetKingPosition(board.Pieces, Type.bKing))
                        {
                            MessageBox.Show("You Win!");
                            this.Close();

                        }
                    }
                    MessageBox.Show("Draw!");
                    this.Close();
                }
            } 
        }
        /// <summary>
        /// Draw the pieces
        /// </summary>
        /// <param name="e"></param>
        /// <param name="player"></param>
        /// <param name="white"></param>
        /// <param name="black"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void DrawPiece(PaintEventArgs e, Player player, string white, string black, int i, int j)
        {
            Font drawFont = new Font("Arial", 26);
            SolidBrush textBrush = new SolidBrush(System.Drawing.Color.Black);
            int width = panel1.Width / 8;
            int height = panel1.Height / 8;

            switch (player)
            {
                case Player.White:
                    e.Graphics.DrawString(white, drawFont, textBrush, width / 8 + j * width, 8 * height - (7 * height / 9 + i * height));
                    break;
                case Player.Black:
                    e.Graphics.DrawString(black, drawFont, textBrush, width / 8 + j * width, 8 * height - (7 * height / 9 + i * height));
                    break;
            }
        }
        /// <summary>
        /// Draw chess board
        /// </summary>
        /// <param name="e"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawSquare(PaintEventArgs e, int x, int y)
        {
            Brush brush;
            if((x+y)% 2 == 0)
            {
                brush = Brushes.White;
                if (SelectedPiece != null && SelectedPiece._position.x == x && SelectedPiece._position.y == y - 1)
                {
                    brush = Brushes.Yellow;
                }
            }
            else if (SelectedPiece != null && SelectedPiece._position.x == x && SelectedPiece._position.y == y - 1)
            {
                brush = Brushes.Yellow; 
            }
            else
            {
                brush = Brushes.Green;
            }

            if (SelectedPiece != null)
            {
                int tile = SelectedPiece._position.x + SelectedPiece._position.y * 8;
                foreach (int move in SelectedPiece.LegalMoves(tile, board))
                {
                    int m = move / 8;
                    int n = move - m * 8;
                    if (m == y-1 && n == x)
                    {
                        brush = Brushes.Red;
                    }
                }
            }

            int xPanel, yPanel;
            int width = panel1.Width / 8;
            int height = panel1.Height / 8;
            xPanel = x * width;
            yPanel = panel1.Height - height * y;
            e.Graphics.FillRectangle(brush, xPanel, yPanel, width, height);
        }

        private void From1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Draw the board 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Brush darkSquare = Brushes.Sienna;
            Brush lightSquare = Brushes.White;
            Font drawFont = new Font("Arial", 16);
            SolidBrush textBrush = new SolidBrush(System.Drawing.Color.Black);
            int width = panel1.Width / 8;
            int height = panel1.Height / 8;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    DrawSquare(e, i, j);
                }
            }


            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int tile = x + y * 8;


                    if (board.Pieces[tile] != null)
                    {
                        Player player = board.Pieces[tile].Player;
                        if (board.Pieces[tile].GetType() == typeof(Pawn))
                        {
                            DrawPiece(e, player, "♙", "♟", y, x);
                        }
                        else if (board.Pieces[tile].GetType() == typeof(Rook))
                        {
                            DrawPiece(e, player, "♖", "♜", y, x);
                        }
                        else if (board.Pieces[tile].GetType() == typeof(Knight))
                        {
                            DrawPiece(e, player, "♘", "♞", y, x);
                        }
                        else if (board.Pieces[tile].GetType() == typeof(Bishop))
                        {
                            DrawPiece(e, player, "♗", "♝", y, x);
                        }
                        else if (board.Pieces[tile].GetType() == typeof(Queen))
                        {
                            DrawPiece(e, player, "♕", "♛", y, x);
                        }
                        else if (board.Pieces[tile].GetType() == typeof(King))
                        {
                            DrawPiece(e, player, "♔", "♚", y, x);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get what the user wants to do
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (board.Turn == Player.White)
            {
                decimal width = panel1.Width / 8;
                decimal height = panel1.Height / 8;

                decimal x = e.X / width;
                decimal i = Math.Floor(x);
                decimal y = e.Y / height;
                decimal j = 7 - Math.Floor(y);

                decimal clickedSquarePoint = (j * 8 + i);
                int clicked = Convert.ToInt32(clickedSquarePoint);
                Piece p = board.Pieces[clicked];
                if (SelectedPiece == null && p != null)
                {
                    if (p.Player == board.Turn)
                    {
                        SelectedPiece = p;
                        panel1.Invalidate();

                    }
                    else
                    {
                        MessageBox.Show("This is black piece");
                    }
                }
                else
                {
                    if (p != null && p.Player == board.Turn)
                    {
                        SelectedPiece = p;
                        panel1.Invalidate();
                    }
                    else
                    {
                        if (SelectedPiece != null)
                        {
                            int m = SelectedPiece._position.x;
                            int n = SelectedPiece._position.y;
                            int tile = (m + n * 8);

                            if(board.GetMove(tile, clicked, board) == false) {
                                MessageBox.Show("Can't move");
                            }


                            SelectedPiece = null;
                            panel1.Invalidate();

                        }
                        else
                        {
                            SelectedPiece = null;
                        }
                    }
                }
                updateUI();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void selectX_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
