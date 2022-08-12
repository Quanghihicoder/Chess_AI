using NUnit.Framework;

namespace ChessAIForms
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestBoard()
        {
            Board board = new Board();
            int numofsquare = board.Pieces.Length;
            Assert.AreEqual(numofsquare, 64, "The Chess Board has 64 squares");
        }

        [Test]
        public void TestPawnPosition()
        {
            Board board = new Board();
            object actual = board.Pieces[8].GetType();
            object expected = typeof(Pawn);
            Assert.AreEqual(actual, expected, "True");
        }

        [Test]
        public void TestRookPosition()
        {
            Board board = new Board();
            object actual = board.Pieces[7].GetType();
            object expected = typeof(Rook);
            Assert.AreEqual(actual, expected, "True");
        }

        [Test]
        public void TestKnightPosition()
        {
            Board board = new Board();
            object actual = board.Pieces[6].GetType();
            object expected = typeof(Knight);
            Assert.AreEqual(actual, expected, "True");
        }

        [Test]
        public void TestBishopPosition()
        {
            Board board = new Board();
            object actual = board.Pieces[5].GetType();
            object expected = typeof(Bishop);
            Assert.AreEqual(actual, expected, "True");
        }

        [Test]
        public void TestKingPosition()
        {
            Board board = new Board();
            object actual = board.Pieces[4].GetType();
            object expected = typeof(King);
            Assert.AreEqual(actual, expected, "True");
        }

        [Test]
        public void TestQueenPosition()
        {
            Board board = new Board();
            object actual = board.Pieces[3].GetType();
            object expected = typeof(Queen);
            Assert.AreEqual(actual, expected, "True");
        }

        [Test]
        public void TestWhiteTurnFirst()
        {
            Board board = new Board();
            Player actual = board.Turn;
            Player expected = Player.White;
            Assert.AreEqual(actual, expected, "True");
        }

        [Test]
        public void AllFirstWhiteLegalMoves()
        {
            Board board = new Board();
            int actual = Board.GetAllLegalMoves(Player.White, board, true).Count;
            int expected = 20;
            Assert.AreEqual(actual, expected, "True");
        }
    }
}