using ChessDotNet.Exceptions;
using ChessDotNet.Public;
using ChessDotNet.Tests.TestData;
using ChessDotNet.Tests.TestUtils;

namespace ChessDotNet.Tests
{
    public class ChessTests
    {
        #region Headers

        [Fact]
        public void GetHeaders_InputOneHeader_ReturnsArrayWith1PutHeader()
        {
            var chess = new Chess();

            chess.SetHeader(new PngHeader("White", "Magnus Carlsen"));

            var headers = chess.GetHeaders();

            Assert.Collection(headers,
                header => Assert.Equal(header, new PngHeader("White", "Magnus Carlsen")));
        }

        [Fact]
        public void GetHeaders_InputTwoHeaders_ReturnsArrayWith2PutHeaders()
        {
            var chess = new Chess();

            chess.SetHeader(new PngHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PngHeader("Black", "Garry Kasparov"));

            var headers = chess.GetHeaders();

            Assert.Collection(headers,
                header => Assert.Equal(header, new PngHeader("White", "Viswanathan Anand")),
                header => Assert.Equal(header, new PngHeader("Black", "Garry Kasparov"))
                );
        }

        [Fact]
        public void RemoveHeaders_RemoveExistentHeader_ReturnsTrue()
        {
            var chess = new Chess();

            chess.SetHeader(new PngHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PngHeader("Black", "Garry Kasparov"));

            var removeResult = chess.RemoveHeader("Black");

            Assert.True(removeResult);
        }

        [Fact]
        public void RemoveHeaders_RemoveAbsentHeader_ReturnsFalse()
        {
            var chess = new Chess();

            chess.SetHeader(new PngHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PngHeader("Black", "Garry Kasparov"));

            var removeResult = chess.RemoveHeader("Blue");

            Assert.False(removeResult);
        }

        #endregion

        #region Load

        [Theory]
        [ClassData(typeof(LoadFenFailedTestData))]
        public void Load_FailedInputData_ThrowsFenValidationException(string fen)
        {
            var chess = new Chess();

            Assert.Throws<FenValidationException>(() => chess.Load(fen));
        }

        [Fact]
        public void Load_FailedInputDataSkipValidation_NotThrowsAnyException()
        {
            var chess = new Chess();

            var fen = "1nbqkbn1/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/1NBQ1BN1 b - - 1 2";

            Assert.Null(Record.Exception(() => chess.Load(fen, skipValidation: true)));
        }

        [Theory]
        [ClassData(typeof(LoadFenCorrectTestData))]
        public void Load_CorrectInputData_NotThrowsAnyException(string fen)
        {
            var chess = new Chess();

            Assert.Null(Record.Exception(() => chess.Load(fen)));
        }

        [Fact]
        public void Load_CorrectInputDataPreserveHeadersFalse_ReturnsEmptyHeadersArray()
        {
            var chess = new Chess();

            chess.SetHeader(new PngHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PngHeader("Black", "Garry Kasparov"));

            chess.Load(PublicData.DefaultChessPosition);

            var headers = chess.GetHeaders();

            Assert.Empty(headers);
        }

        [Fact]
        public void Load_CorrectInputDataPreserveHeadersTrue_ReturnsArrayWith2PutHeaders()
        {
            var chess = new Chess();

            chess.SetHeader(new PngHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PngHeader("Black", "Garry Kasparov"));

            chess.Load(PublicData.DefaultChessPosition, preserveHeaders: true);

            var headers = chess.GetHeaders();

            Assert.Collection(headers,
                header => Assert.Equal(header, new PngHeader("White", "Viswanathan Anand")),
                header => Assert.Equal(header, new PngHeader("Black", "Garry Kasparov"))
            );
        }

        #endregion

        #region Fen

        [Fact]
        public void Fen_Constructor_ReturnsDefaultPosition()
        {
            var chess = new Chess();

            var fen = chess.Fen();

            Assert.Equal(PublicData.DefaultChessPosition, fen);
        }

        [Theory]
        [ClassData(typeof(FenCorrectTestData))]
        public void Fen_LoadCorrectInputData_ReturnsTheSameFen(string fen)
        {
            var chess = new Chess(fen);

            Assert.Equal(fen, chess.Fen());
        }

        [Theory]
        [ClassData(typeof(FenWithMoveCorrectTestData))]
        public void Fen_LoadFenMakeMove_ReturnsCorrectFen(string inputFen, string move, string outputFen)
        {
            var chess = new Chess(inputFen);

            chess.Move(move);

            Assert.Equal(outputFen, chess.Fen());
        }

        #endregion

        #region Move

        [Theory]
        [ClassData(typeof(MoveStringFailedTestData))]
        public void Move_IncorrectMoveString_ThrowsInvalidMoveException(string fen, string move, bool strict)
        {
            var chess = new Chess(fen);

            Assert.Throws<InvalidMoveException>(() => chess.Move(move, strict));
        }

        [Theory]
        [ClassData(typeof(MoveObjectFailedTestData))]
        public void Move_IncorrectMoveObject_ThrowsInvalidMoveException(string fen, MoveInfo move)
        {
            var chess = new Chess(fen);

            Assert.Throws<InvalidMoveException>(() => chess.Move(move));
        }

        [Theory]
        [ClassData(typeof(MoveStringCorrectTestData))]
        public void Move_CorrectMoveString_ReturnsMatchedMoveObjectAndCorrectFen(string inputFen, string move, string outputFen, MoveResultTestObject moveResult)
        {
            var chess = new Chess(inputFen);

            var outputMove = chess.Move(move);

            Assert.Equal(outputFen, chess.Fen());

            Assert.Equal(moveResult.From, outputMove.From.ToString());
            Assert.Equal(moveResult.To, outputMove.To.ToString());

            if (moveResult.Piece != null)
                Assert.Equal(moveResult.Piece, (char)outputMove.Piece);

            if (moveResult.Captured != null)
                Assert.Equal(moveResult.Captured, outputMove.Captured == null ? null : (char)outputMove.Captured);

            if (moveResult.Flags != null)
                Assert.Equal(moveResult.Flags, outputMove.Flags);
        }

        [Theory]
        [ClassData(typeof(MoveObjectCorrectTestData))]
        public void Move_CorrectMoveObject_ReturnsCorrectFen(string inputFen, MoveInfo move, string outputFen)
        {
            var chess = new Chess(inputFen);

            chess.Move(move);

            Assert.Equal(outputFen, chess.Fen());
        }

        #endregion

        #region Clear

        [Fact]
        public void Clear_PreserveHeadersFalse_ReturnsEmptyBoardAndNoHeaders()
        {
            var chess = new Chess();

            chess.SetHeader(new PngHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PngHeader("Black", "Garry Kasparov"));

            chess.Clear();

            var headers = chess.GetHeaders();

            Assert.Equal("8/8/8/8/8/8/8/8 w - - 0 1", chess.Fen());
            Assert.Empty(headers);
        }

        [Fact]
        public void Clear_PreserveHeadersTrue_ReturnsEmptyBoardAndSameHeaders()
        {
            var chess = new Chess();

            chess.SetHeader(new PngHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PngHeader("Black", "Garry Kasparov"));

            chess.Clear(true);

            var headers = chess.GetHeaders();

            Assert.Equal("8/8/8/8/8/8/8/8 w - - 0 1", chess.Fen());
            Assert.Collection(headers,
                header => Assert.Equal(header, new PngHeader("White", "Viswanathan Anand")),
                header => Assert.Equal(header, new PngHeader("Black", "Garry Kasparov"))
            );
        }

        #endregion

        #region ASCII

        [Fact]
        public void Ascii_ReturnsCorrectString()
        {
            var chess = new Chess("r4rk1/4nqpp/1p1p4/2pPpp2/bPP1P3/R1B1NQ2/P4PPP/1R4K1 w - - 0 28");

            var outputTable = new string[]
            {
                "   +------------------------+",
                " 8 | r  .  .  .  .  r  k  . |",
                " 7 | .  .  .  .  n  q  p  p |",
                " 6 | .  p  .  p  .  .  .  . |",
                " 5 | .  .  p  P  p  p  .  . |",
                " 4 | b  P  P  .  P  .  .  . |",
                " 3 | R  .  B  .  N  Q  .  . |",
                " 2 | P  .  .  .  .  P  P  P |",
                " 1 | .  R  .  .  .  .  K  . |",
                "   +------------------------+",
                "     a  b  c  d  e  f  g  h"
            };

            Assert.Equal(string.Join("\n", outputTable), chess.Ascii());
        }

        #endregion

        #region Board

        [Theory]
        [ClassData(typeof(BoardCorrectTestData))]
        public void Board_CorrectInput_ReturnsCorrectBoardItems(string fen, BoardItem?[][] boardItemsExpected)
        {
            var chess = new Chess(fen);

            var boardItemsActual = chess.Board();

            for (int i = 0; i < boardItemsExpected.GetLength(0); i++)
            {
                for (int j = 0; j < boardItemsExpected[i].Length; j++)
                    Assert.Equal(boardItemsActual[i][j], boardItemsExpected[i][j]);
            }
        }

        #endregion

        #region Attackers

        [Theory]
        [ClassData(typeof(AttackersCountPerSquareTestData))]
        public void Attackers_InputFenAndColor_ReturnsCorrectAttackersCountPerSquare(string fen, ChessColor color, int[] counts)
        {
            var chess = new Chess(fen);

            for (var i = 0; i < PublicData.Squares.Length; i++)
                Assert.Equal(chess.Attackers(PublicData.Squares[i], color).Length, counts[i]);
        }

        [Theory]
        [ClassData(typeof(AttackersIncludeSquaresTestData))]
        public void Attackers_InputFenSquareAndColor_ReturnsCorrectAttackerSquares(string fen, ChessSquare square, ChessColor? color, ChessSquare[] attackerSquaresExpected)
        {
            var chess = new Chess(fen);

            var attackerSquaresActual = chess.Attackers(square, color);

            Assert.Equal(attackerSquaresExpected.Length, attackerSquaresActual.Length);

            foreach (var expectedSquare in attackerSquaresExpected)
                Assert.Contains(expectedSquare, attackerSquaresActual);
        }

        [Theory]
        [ClassData(typeof(AttackersIncludeSquaresWithMovesTestData))]
        public void Attackers_InputFenMovesSquareAndColor_ReturnsCorrectAttackerSquares(string fen, string[] moves, ChessSquare square, ChessColor? color, ChessSquare[] attackerSquaresExpected)
        {
            var chess = new Chess(fen);

            foreach (var move in moves)
                chess.Move(move);

            var attackerSquaresActual = chess.Attackers(square, color);

            Assert.Equal(attackerSquaresExpected.Length, attackerSquaresActual.Length);

            foreach (var expectedSquare in attackerSquaresExpected)
                Assert.Contains(expectedSquare, attackerSquaresActual);
        }

        #endregion

        #region Castling rights

        [Theory]
        [ClassData(typeof(CastlingRightsTestData))]
        public void CastlingRights_InputData_ReturnsExpectedValues(string fen, bool clear, ChessColor color, CastlingRights castlingRights, bool setExpectedResult, bool getExpectedResult)
        {
            var chess = new Chess(fen);

            if (clear)
                chess.Clear();

            var setActualResult = chess.SetCastlingRights(color, castlingRights);

            Assert.Equal(setExpectedResult, setActualResult);

            var getActualResult = chess.GetCastlingRights(color);
            if (castlingRights.KingCastlingRights != null)
                Assert.Equal(getExpectedResult, getActualResult.KingCastlingRights);

            if (castlingRights.QueenCastlingRights != null)
                Assert.Equal(getExpectedResult, getActualResult.QueenCastlingRights);
        }

        #endregion

        #region Get

        [Theory]
        [ClassData(typeof(GetTestData))]
        public void Get_InputSquare_ReturnsCorrectPiece(string fen, ChessSquare square, ChessPiece? piece)
        {
            var chess = new Chess(fen);

            Assert.Equal(piece, chess.Get(square));
        }

        #endregion

        #region Put

        [Fact]
        public void Put_Put2WhiteKingsOnEmptyBoard_ReturnsTrueAndFalse()
        {
            var chess = new Chess();
            chess.Clear();

            Assert.True(chess.Put(new ChessPiece(ChessColor.White, ChessPieceType.King), new ChessSquare("a2")));
            Assert.False(chess.Put(new ChessPiece(ChessColor.White, ChessPieceType.King), new ChessSquare("a3")));
        }

        [Fact]
        public void Put_Put2BlackKingsOnEmptyBoard_ReturnsTrueAndFalse()
        {
            var chess = new Chess();
            chess.Clear();

            Assert.True(chess.Put(new ChessPiece(ChessColor.Black, ChessPieceType.King), new ChessSquare("e8")));
            Assert.False(chess.Put(new ChessPiece(ChessColor.Black, ChessPieceType.King), new ChessSquare("d8")));
        }

        [Fact]
        public void Put_Put2WhiteKingsOnEmptyBoardOnSameSquare_ReturnsTrueAndTrue()
        {
            var chess = new Chess();
            chess.Clear();

            Assert.True(chess.Put(new ChessPiece(ChessColor.White, ChessPieceType.King), new ChessSquare("a2")));
            Assert.True(chess.Put(new ChessPiece(ChessColor.White, ChessPieceType.King), new ChessSquare("a2")));
        }

        [Theory]
        [ClassData(typeof(PutTestData))]
        public void Put_InputFenPieceAndSquare_NotReturnsProvidedMoves(string fen, ChessPiece piece, ChessSquare square, string[] movesToTest)
        {
            var chess = new Chess(fen);

            chess.Put(piece, square);

            var movesActual = chess.GetMoves();

            foreach (var move in movesToTest)
                Assert.DoesNotContain(move, movesActual);
        }

        #endregion

        #region GetMoves

        [Theory]
        [ClassData(typeof(MovesTestData))]
        public void GetMoves_InputFenPieceTypeAndSquare_ReturnsCorrectMoves(string fen, ChessPieceType? pieceType, ChessSquare? square, string[] movesExpected)
        {
            var chess = new Chess(fen);

            var movesActual = chess.GetMoves(pieceType, square);

            Assert.Equal(movesExpected.Length, movesActual.Length);

            foreach (var move in movesExpected)
                Assert.Contains(move, movesActual);
        }

        [Theory]
        [ClassData(typeof(MovesAsStringTestData))]
        public void GetMoves_InputFen_ReturnsCorrectMoves(string fen, string moves)
        {
            var chess = new Chess(fen);

            var movesExpected = moves.Split();
            var movesActual = chess.GetMoves();

            Assert.Equal(movesExpected.Length, movesActual.Length);

            foreach (var move in movesExpected)
                Assert.Contains(move, movesActual);
        }

        [Theory]
        [ClassData(typeof(MovesVerboseTestData))]
        public void GetMovesVerbose_InputFenPieceTypeAndSquare_ReturnsCorrectVerboseMoves(string fen, ChessPieceType? pieceType, ChessSquare? square, ChessMove[] movesExpected)
        {
            var chess = new Chess(fen);

            var movesActual = chess.GetMovesVerbose(pieceType, square);

            Assert.Equal(movesExpected.Length, movesActual.Length);

            foreach (var move in movesExpected)
                Assert.Contains(move, movesActual);
        }
        #endregion

        #region Remove

        [Fact]
        public void Remove_NonEmptySquare_ReturnsPiece()
        {
            var chess = new Chess();

            Assert.Equal(new ChessPiece(ChessColor.White, ChessPieceType.Queen), chess.Remove(new ChessSquare("d1")));
            Assert.Null(chess.Get(new ChessSquare("d1")));
        }

        [Fact]
        public void Remove_EmptySquare_ReturnsNull()
        {
            var chess = new Chess();

            Assert.Null(chess.Remove(new ChessSquare("e4")));
        }

        [Theory]
        [ClassData(typeof(RemoveTestData))]
        public void Remove_InputFenAndSquare_NotReturnsProvidedMoves(string fen, ChessSquare square, string[] movesToTest)
        {
            var chess = new Chess(fen);

            chess.Remove(square);

            var movesActual = chess.GetMoves();

            foreach (var move in movesToTest)
                Assert.DoesNotContain(move, movesActual);
        }

        #endregion

        #region Reset

        [Fact]
        public void Reset_ClearAndResetBoard_ReturnsDefaultPosition()
        {
            var chess = new Chess();

            chess.Clear();
            chess.Reset();

            Assert.Equal(PublicData.DefaultChessPosition, chess.Fen());
        }

        #endregion

        #region IsAttacked

        [Theory]
        [ClassData(typeof(IsAttackedTestData))]
        public void IsAttacked_InputFenColorAndSquares_ReturnsCorrectInformationAboutAttackedSquares(string fen, ChessColor color, bool result, ChessSquare[] testSquares)
        {
            var chess = new Chess(fen);

            if (result)
                Assert.All(testSquares, sq => Assert.True(chess.IsAttacked(sq, color)));
            else
                Assert.All(testSquares, sq => Assert.False(chess.IsAttacked(sq, color)));
        }

        #endregion

        #region IsCheck

        [Theory]
        [InlineData(PublicData.DefaultChessPosition, false)]
        [InlineData("rnb1kbnr/pppp1ppp/8/8/4Pp1q/2N5/PPPP2PP/R1BQKBNR w KQkq - 2 4", true)]
        [InlineData("R3k3/8/4K3/8/8/8/8/8 b - - 0 1", true)]
        [InlineData("4k3/4P3/4K3/8/8/8/8/8 b - - 0 1", false)]
        public void IsCheck_ReturnsTrueIfKingOfActingTurnIsUnderCheck(string fen, bool result)
        {
            var chess = new Chess(fen);

            Assert.Equal(result, chess.IsCheck());
        }

        [Fact]
        public void IsCheck_RemovedKings_ReturnsFalse()
        {
            var chess = new Chess();

            chess.Remove(new ChessSquare("e1"));
            chess.Remove(new ChessSquare("e8"));

            Assert.False(chess.IsCheck());
        }

        #endregion

        #region IsStalemate

        [Theory]
        [InlineData("1R6/8/8/8/8/8/7R/k6K b - - 0 1", true)]
        [InlineData("8/8/5k2/p4p1p/P4K1P/1r6/8/8 w - - 0 2", true)]
        [InlineData(PublicData.DefaultChessPosition, false)]
        [InlineData("R3k3/8/4K3/8/8/8/8/8 b - - 0 1", false)]
        public void IsStalemate_ReturnsTrueIfStalemate(string fen, bool result)
        {
            var chess = new Chess(fen);

            Assert.Equal(result, chess.IsStalemate());
        }

        [Fact]
        public void IsStalemate_RemovedKings_ReturnsFalse()
        {
            var chess = new Chess();

            chess.Remove(new ChessSquare("e1"));
            chess.Remove(new ChessSquare("e8"));

            Assert.False(chess.IsStalemate());
        }

        #endregion

        #region IsThreefoldRepetition

        [Theory]
        [ClassData(typeof(IsThreefoldRepetitionTestData))]
        public void IsThreefoldRepetition_InputFenAndMoves_ReturnsCorrectIsThreefoldRepetition(string fen, string[] moves, bool result)
        {
            var chess = new Chess(fen);

            foreach (var move in moves)
                chess.Move(move);

            Assert.Equal(result, chess.IsThreefoldRepetition());
        }

        #endregion

        #region IsInsufficientMaterial

        [Theory]
        [InlineData("8/8/8/8/8/8/8/k6K w - - 0 1", true)]
        [InlineData("8/2N5/8/8/8/8/8/k6K w - - 0 1", true)]
        [InlineData("8/2b5/8/8/8/8/8/k6K w - - 0 1", true)]
        [InlineData("8/b7/3B4/8/8/8/8/k6K w - - 0 1", true)]
        [InlineData("8/b1B1b1B1/1b1B1b1B/8/8/8/8/1k5K w - - 0 1", true)]
        [InlineData(PublicData.DefaultChessPosition, false)]
        [InlineData("8/2p5/8/8/8/8/8/k6K w - - 0 1", false)]
        [InlineData("5k1K/7B/8/6b1/8/8/8/8 b - - 0 1", false)]
        [InlineData("7K/5k1N/8/6b1/8/8/8/8 b - - 0 1", false)]
        [InlineData("7K/5k1N/8/4n3/8/8/8/8 b - - 0 1", false)]
        public void IsInsufficientMaterial_ReturnsTrueIfThereIsInsufficientMaterial(string fen, bool result)
        {
            var chess = new Chess(fen);

            Assert.Equal(result, chess.IsInsufficientMaterial());
        }

        #endregion

        #region IsCheckmate

        [Theory]
        [InlineData("8/5r2/4K1q1/4p3/3k4/8/8/8 w - - 0 7")]
        [InlineData("4r2r/p6p/1pnN2p1/kQp5/3pPq2/3P4/PPP3PP/R5K1 b - - 0 2")]
        [InlineData("r3k2r/ppp2p1p/2n1p1p1/8/2B2P1q/2NPb1n1/PP4PP/R2Q3K w kq - 0 8")]
        [InlineData("8/6R1/pp1r3p/6p1/P3R1Pk/1P4P1/7K/8 b - - 0 4")]
        public void IsCheckmate_ReturnsTrue(string fen)
        {
            var chess = new Chess(fen);

            Assert.True(chess.IsCheckmate());
            Assert.False(chess.IsDraw());
        }

        [Theory]
        [InlineData(PublicData.DefaultChessPosition)]
        [InlineData("1R6/8/8/8/8/8/7R/k6K b - - 0 1")]
        public void IsCheckmate_ReturnsFalse(string fen)
        {
            var chess = new Chess(fen);

            Assert.False(chess.IsCheckmate());
        }

        [Fact]
        public void IsCheckmate_RemovedKings_ReturnsFalse()
        {
            var chess = new Chess();

            chess.Remove(new ChessSquare("e1"));
            chess.Remove(new ChessSquare("e8"));

            Assert.False(chess.IsCheckmate());
        }

        #endregion
    }
}
