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

            chess.SetHeader(new PgnHeader("White", "Magnus Carlsen"));

            var headers = chess.GetHeaders();

            Assert.Collection(headers,
                header => Assert.Equal(header, new PgnHeader("White", "Magnus Carlsen")));
        }

        [Fact]
        public void GetHeaders_Input2EqualHeaders_ReturnsArrayWith1Header()
        {
            var chess = new Chess();

            chess.SetHeader(new PgnHeader("White", "Magnus Carlsen"));
            chess.SetHeader(new PgnHeader("White", "Magnus Carlsen"));

            var headers = chess.GetHeaders();

            Assert.Collection(headers,
                header => Assert.Equal(header, new PgnHeader("White", "Magnus Carlsen")));
        }

        [Fact]
        public void GetHeaders_InputTwoHeaders_ReturnsArrayWith2PutHeaders()
        {
            var chess = new Chess();

            chess.SetHeader(new PgnHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PgnHeader("Black", "Garry Kasparov"));

            var headers = chess.GetHeaders();

            Assert.Collection(headers,
                header => Assert.Equal(header, new PgnHeader("White", "Viswanathan Anand")),
                header => Assert.Equal(header, new PgnHeader("Black", "Garry Kasparov"))
            );
        }

        [Fact]
        public void GetHeaders_LoadMessyPgn_ReturnsDesiredHeaders()
        {
            var chess = new Chess();

            var pgn = "    \t   [ Event\"Reykjavik WCh\"    ]\n[Site \"Reykjavik WCh\"]       \n[Date \"1972.01.07\"]\n[EventDate \"?\"]\n[\tRound \"6\"]\n[Result \"1-0\"]\n[White \"Robert James Fischer\"]\r\n[Black \"Boris Spassky\"]\n[ECO \"D59\"]\n[WhiteElo \"?\"]\n[BlackElo \"?\"]\n[PlyCount \"81\"]                \n            \r\n1. c4 e6 2. Nf3 d5 3. d4 Nf6 4. Nc3 Be7 5. Bg5 O-O 6. e3 h6\n7. Bh4 b6 8. cxd5 Nxd5 9. Bxe7 Qxe7 10. Nxd5 exd5 11. Rc1 Be6\n12. Qa4 c5 13. Qa3 Rc8 14. Bb5 a6 15. dxc5 bxc5 16. O-O Ra7\n17. Be2 Nd7 18. Nd4 Qf8 19. Nxe6 fxe6 20. e4 d4 21. f4 Qe7\r\n22. e5 Rb8 23. Bc4 Kh8 24. Qh3 Nf8 25. b3 a5 26. f5 exf5\n27. Rxf5 Nh7 28. Rcf1 Qd8 29. Qg3 Re7 30. h4 Rbb7 31. e6 Rbc7\r\n32. Qe5 Qe8 33. a4 Qd8 34. R1f2 Qe8 35. R2f3 Qd8 36. Bd3 Qe8\r\n37. Qe4 Nf6 38. Rxf6 gxf6 39. Rxf6 Kg8 40. Bc4 Kh8 41. Qf4 1-0\n";

            chess.LoadPgn(pgn);

            var headers = chess.GetHeaders();

            Assert.Equal("Reykjavik WCh", headers.First(h => h.Key == "Event").Value);
            Assert.Equal("6", headers.First(h => h.Key == "Round").Value);
        }

        [Fact]
        public void RemoveHeaders_RemoveExistentHeader_ReturnsTrue()
        {
            var chess = new Chess();

            chess.SetHeader(new PgnHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PgnHeader("Black", "Garry Kasparov"));

            var removeResult = chess.RemoveHeader("Black");

            Assert.True(removeResult);
        }

        [Fact]
        public void RemoveHeaders_RemoveAbsentHeader_ReturnsFalse()
        {
            var chess = new Chess();

            chess.SetHeader(new PgnHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PgnHeader("Black", "Garry Kasparov"));

            var removeResult = chess.RemoveHeader("Blue");

            Assert.False(removeResult);
        }

        [Fact]
        public void RemoveHeaders_CompareHeaders_ReturnsSameHeaders()
        {
            var chess1 = new Chess();

            chess1.LoadPgn(" [White \"Paul Morphy\"]\r\n  [Black \"Duke Karl / Count Isouard\"]\r\n  [fEn \"1n2kb1r/p4ppp/4q3/4p1B1/4P3/8/PPP2PPP/2KR4 w k - 0 17\"]\r\n\r\n  17.Rd8# 1-0");

            var chess2 = new Chess();

            chess2.LoadPgn("[Black \"Duke Karl / Count Isouard\"]\r\n  [fEn \"1n2kb1r/p4ppp/4q3/4p1B1/4P3/8/PPP2PPP/2KR4 w k - 0 17\"]\r\n\r\n  17.Rd8# 1-0");

            chess1.RemoveHeader("White");

            var headers1 = chess1.GetHeaders();
            var headers2 = chess2.GetHeaders();

            foreach (var header1 in headers1)
                Assert.Contains(header1, headers2);
        }

        #endregion

        #region Load

        [Theory]
        [ClassData(typeof(LoadFenFailedTestData))]
        public void Load_FailedInputData_ThrowsFenValidationException(string fen)
        {
            var chess = new Chess();

            Assert.Throws<FenValidationException>(() => chess.LoadFen(fen));
        }

        [Fact]
        public void Load_FailedInputDataSkipValidation_NotThrowsAnyException()
        {
            var chess = new Chess();

            var fen = "1nbqkbn1/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/1NBQ1BN1 b - - 1 2";

            Assert.Null(Record.Exception(() => chess.LoadFen(fen, skipValidation: true)));
        }

        [Theory]
        [ClassData(typeof(LoadFenCorrectTestData))]
        public void Load_CorrectInputData_NotThrowsAnyException(string fen)
        {
            var chess = new Chess();

            Assert.Null(Record.Exception(() => chess.LoadFen(fen)));
        }

        [Fact]
        public void Load_CorrectInputDataPreserveHeadersFalse_ReturnsEmptyHeadersArray()
        {
            var chess = new Chess();

            chess.SetHeader(new PgnHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PgnHeader("Black", "Garry Kasparov"));

            chess.LoadFen(PublicData.DefaultChessPosition);

            var headers = chess.GetHeaders();

            Assert.Empty(headers);
        }

        [Fact]
        public void Load_CorrectInputDataPreserveHeadersTrue_ReturnsArrayWith2PutHeaders()
        {
            var chess = new Chess();

            chess.SetHeader(new PgnHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PgnHeader("Black", "Garry Kasparov"));

            chess.LoadFen(PublicData.DefaultChessPosition, preserveHeaders: true);

            var headers = chess.GetHeaders();

            Assert.Collection(headers,
                header => Assert.Equal(header, new PgnHeader("White", "Viswanathan Anand")),
                header => Assert.Equal(header, new PgnHeader("Black", "Garry Kasparov"))
            );
        }

        #endregion

        #region Load PGN

        [Theory]
        [ClassData(typeof(LoadPgnStrictFalseCorrectTestData))]
        public void LoadPgn_InputPgn_ReturnsCorrectFen(string pgn, string? fen)
        {
            var chess = new Chess();

            chess.LoadPgn(pgn);

            if (fen != null)
                Assert.Equal(fen, chess.GetFen());
            else
                Assert.Null(Record.Exception(() => chess.LoadPgn(pgn)));
        }

        [Theory]
        [ClassData(typeof(LoadPgnStrictFalseInvalidPgnMoveTestData))]
        public void LoadPgn_InputPgn_ThrowsInvalidPgnMoveException(string pgn)
        {
            var chess = new Chess();

            Assert.Throws<InvalidPgnMoveException>(() => chess.LoadPgn(pgn));
        }

        [Theory]
        [ClassData(typeof(LoadPgnStrictFalseInvalidFenTestData))]
        public void LoadPgn_InputPgn_ThrowsFenValidationException(string pgn)
        {
            var chess = new Chess();

            Assert.Throws<FenValidationException>(() => chess.LoadPgn(pgn));
        }

        [Theory]
        [ClassData(typeof(LoadPgnStrictTrueCorrectTestData))]
        public void LoadPgn_InputPgnStrictMode_ReturnsCorrectFen(string pgn, string fen)
        {
            var chess = new Chess();

            chess.LoadPgn(pgn, true);

            Assert.Equal(fen, chess.GetFen());
        }

        [Theory]
        [ClassData(typeof(LoadPgnStrictTrueInvalidPgnMoveTestData))]
        public void LoadPgn_InputPgnStrictMode_ThrowsInvalidPgnMoveException(string pgn)
        {
            var chess = new Chess();

            Assert.Throws<InvalidPgnMoveException>(() => chess.LoadPgn(pgn, true));
        }

        [Theory]
        [ClassData(typeof(LoadPgnStrictTrueInvalidFenTestData))]
        public void LoadPgn_InputPgnStrictMode_ThrowsFenValidationException(string pgn)
        {
            var chess = new Chess();

            Assert.Throws<FenValidationException>(() => chess.LoadPgn(pgn, true));
        }

        #endregion

        #region Fen

        [Fact]
        public void Fen_Constructor_ReturnsDefaultPosition()
        {
            var chess = new Chess();

            var fen = chess.GetFen();

            Assert.Equal(PublicData.DefaultChessPosition, fen);
        }

        [Theory]
        [ClassData(typeof(FenCorrectTestData))]
        public void Fen_LoadCorrectInputData_ReturnsTheSameFen(string fen)
        {
            var chess = new Chess(fen);

            Assert.Equal(fen, chess.GetFen());
        }

        [Theory]
        [ClassData(typeof(FenWithMoveCorrectTestData))]
        public void Fen_LoadFenMakeMove_ReturnsCorrectFen(string inputFen, string move, string outputFen)
        {
            var chess = new Chess(inputFen);

            chess.Move(move);

            Assert.Equal(outputFen, chess.GetFen());
        }

        #endregion

        #region Pgn

        [Theory]
        [ClassData(typeof(PgnTestData))]
        public void Pgn_LoadPgn_ReturnsCorrectOutput(string inputPgn, string outputPgn)
        {
            var chess = new Chess();

            chess.LoadPgn(inputPgn);

            Assert.Equal(outputPgn, chess.GetPgn());
        }

        [Theory]
        [ClassData(typeof(PgnFileTestData))]
        public void Pgn_LoadPgnFile_ReturnsCorrectOutput(string moves, string[] headers, int maxWidth, string? newLine, string pgnFile, string? startingPosition, string fen)
        {
            var chess = new Chess();

            if (startingPosition != null)
                chess.LoadFen(startingPosition);

            foreach (var move in moves.Split())
                chess.Move(move);

            Assert.Equal(fen, chess.GetFen());

            for (var i = 0; i < headers.Length; i += 2)
                chess.SetHeader(new PgnHeader(headers[i], headers[i + 1]));

            var pgn = File.ReadAllText(pgnFile).Trim();

            Assert.Equal(pgn, chess.GetPgn(newLine ?? Environment.NewLine, maxWidth));
        }

        [Fact]
        public void Pgn_NoComments_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.Move("e4");

            Assert.Equal("1. e4", chess.GetPgn());
        }

        [Fact]
        public void Pgn_CommentForInitialPosition_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.SetComment("Starting position");

            Assert.Equal("{Starting position}", chess.GetPgn());
        }

        [Fact]
        public void Pgn_CommentForFirstMove_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.Move("e4");
            chess.SetComment("Good move");
            chess.Move("e5");

            Assert.Equal("1. e4 {Good move} e5", chess.GetPgn());
        }

        [Fact]
        public void Pgn_CommentForLastMove_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.Move("e4");
            chess.Move("e6");
            chess.SetComment("Dubious move");

            Assert.Equal("1. e4 e6 {Dubious move}", chess.GetPgn());
        }

        [Fact]
        public void Pgn_CommentWithManyMoves_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.SetComment("Starting position");

            Assert.Equal("{Starting position}", chess.GetPgn());

            chess.Move("e4");
            chess.SetComment("Good move");

            Assert.Equal("{Starting position} 1. e4 {Good move}", chess.GetPgn());

            chess.Move("e6");
            chess.SetComment("Dubious move");

            Assert.Equal("{Starting position} 1. e4 {Good move} e6 {Dubious move}", chess.GetPgn());
        }

        [Fact]
        public void Pgn_DeleteComments_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.SetComment("Starting position");
            chess.Move("e4");
            chess.SetComment("Good move");
            chess.Move("e6");
            chess.SetComment("Dubious move");

            chess.DeleteComment();

            Assert.Equal("{Starting position} 1. e4 {Good move} e6", chess.GetPgn());

            chess.DeleteComments();

            Assert.Equal("1. e4 e6", chess.GetPgn());
        }

        [Fact]
        public void Pgn_PruneComments_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.Move("e4");
            chess.SetComment("Tactical");
            chess.Undo();
            chess.Move("d4");
            chess.SetComment("Positional");

            Assert.Equal("1. d4 {Positional}", chess.GetPgn());
        }

        [Fact]
        public void Pgn_FormatComments_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.Move("e4");
            chess.SetComment("Good   move");
            chess.Move("e5");
            chess.SetComment("Classical response");

            Assert.Equal("1. e4 {Good   move} e5 {Classical response}", chess.GetPgn());
            Assert.Equal(string.Join("\n", new string[]
            {
                "1. e4 {Good",
                "move} e5",
                "{Classical",
                "response}"
            }), chess.GetPgn(maxWidth: 16, newLine: "\n"));
            Assert.Equal(string.Join("\n", new string[]
            {
                "1.",
                "e4",
                "{Good",
                "move}",
                "e5",
                "{Classical",
                "response}"
            }), chess.GetPgn(maxWidth: 2, newLine: "\n"));
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

            Assert.Equal(outputFen, chess.GetFen());

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

            Assert.Equal(outputFen, chess.GetFen());
        }

        #endregion

        #region Clear

        [Fact]
        public void Clear_PreserveHeadersFalse_ReturnsEmptyBoardAndNoHeaders()
        {
            var chess = new Chess();

            chess.SetHeader(new PgnHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PgnHeader("Black", "Garry Kasparov"));

            chess.ClearBoard();

            var headers = chess.GetHeaders();

            Assert.Equal("8/8/8/8/8/8/8/8 w - - 0 1", chess.GetFen());
            Assert.Empty(headers);
        }

        [Fact]
        public void Clear_PreserveHeadersTrue_ReturnsEmptyBoardAndSameHeaders()
        {
            var chess = new Chess();

            chess.SetHeader(new PgnHeader("White", "Viswanathan Anand"));
            chess.SetHeader(new PgnHeader("Black", "Garry Kasparov"));

            chess.ClearBoard(true);

            var headers = chess.GetHeaders();

            Assert.Equal("8/8/8/8/8/8/8/8 w - - 0 1", chess.GetFen());
            Assert.Collection(headers,
                header => Assert.Equal(header, new PgnHeader("White", "Viswanathan Anand")),
                header => Assert.Equal(header, new PgnHeader("Black", "Garry Kasparov"))
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

            Assert.Equal(string.Join("\n", outputTable), chess.ToAscii());
        }

        #endregion

        #region Board

        [Theory]
        [ClassData(typeof(BoardCorrectTestData))]
        public void Board_CorrectInput_ReturnsCorrectBoardItems(string fen, BoardItem?[][] boardItemsExpected)
        {
            var chess = new Chess(fen);

            var boardItemsActual = chess.GetBoard();

            for (int i = 0; i < boardItemsExpected.GetLength(0); i++)
            {
                for (int j = 0; j < boardItemsExpected[i].Length; j++)
                    Assert.Equal(boardItemsActual[i][j], boardItemsExpected[i][j]);
            }
        }

        #endregion

        #region History

        [Theory]
        [ClassData(typeof(HistoryFenSetupTestData))]
        public void GetHistory_FenSetup_ReturnsCorrectFenAndHistory(string fen, string[] moves)
        {
            var chess = new Chess();

            foreach (var move in moves)
                chess.Move(move);

            var history = chess.GetHistory();

            Assert.Equal(fen, chess.GetFen());
            Assert.Equal(history.Length, moves.Length);

            foreach (var move in moves)
                Assert.Contains(move, history);
        }

        [Theory]
        [ClassData(typeof(HistoryVerboseFenSetupTestData))]
        public void GetHistoryVerbose_FenSetup_ReturnsCorrectFenAndHistory(string fen, ChessMove[] moves)
        {
            var chess = new Chess();

            foreach (var move in moves)
                chess.Move(move.San);

            var history = chess.GetHistoryVerbose();

            Assert.Equal(fen, chess.GetFen());
            Assert.Equal(history.Length, moves.Length);

            foreach (var move in moves)
                Assert.Contains(move, history);
        }

        [Theory]
        [ClassData(typeof(HistoryVerbosePngSetupTestData))]
        public void GetHistoryVerbose_PngSetup_ReturnsCorrectHistory(string pgn, ChessMove[] moves)
        {
            var chess = new Chess();

            chess.LoadPgn(pgn);

            var history = chess.GetHistoryVerbose();

            Assert.Equal(history.Length, moves.Length);

            foreach (var move in moves)
                Assert.Contains(move, history);
        }

        #endregion

        #region Attackers

        [Theory]
        [ClassData(typeof(AttackersCountPerSquareTestData))]
        public void Attackers_InputFenAndColor_ReturnsCorrectAttackersCountPerSquare(string fen, ChessColor color, int[] counts)
        {
            var chess = new Chess(fen);

            for (var i = 0; i < PublicData.Squares.Length; i++)
                Assert.Equal(chess.GetAttackers(PublicData.Squares[i], color).Length, counts[i]);
        }

        [Theory]
        [ClassData(typeof(AttackersIncludeSquaresTestData))]
        public void Attackers_InputFenSquareAndColor_ReturnsCorrectAttackerSquares(string fen, ChessSquare square, ChessColor? color, ChessSquare[] attackerSquaresExpected)
        {
            var chess = new Chess(fen);

            var attackerSquaresActual = chess.GetAttackers(square, color);

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

            var attackerSquaresActual = chess.GetAttackers(square, color);

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
                chess.ClearBoard();

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

            Assert.Equal(piece, chess.GetPiece(square));
        }

        #endregion

        #region Put

        [Fact]
        public void Put_Put2WhiteKingsOnEmptyBoard_ReturnsTrueAndFalse()
        {
            var chess = new Chess();
            chess.ClearBoard();

            Assert.True(chess.PutPiece(new ChessPiece(ChessColor.White, ChessPieceType.King), new ChessSquare("a2")));
            Assert.False(chess.PutPiece(new ChessPiece(ChessColor.White, ChessPieceType.King), new ChessSquare("a3")));
        }

        [Fact]
        public void Put_Put2BlackKingsOnEmptyBoard_ReturnsTrueAndFalse()
        {
            var chess = new Chess();
            chess.ClearBoard();

            Assert.True(chess.PutPiece(new ChessPiece(ChessColor.Black, ChessPieceType.King), new ChessSquare("e8")));
            Assert.False(chess.PutPiece(new ChessPiece(ChessColor.Black, ChessPieceType.King), new ChessSquare("d8")));
        }

        [Fact]
        public void Put_Put2WhiteKingsOnEmptyBoardOnSameSquare_ReturnsTrueAndTrue()
        {
            var chess = new Chess();
            chess.ClearBoard();

            Assert.True(chess.PutPiece(new ChessPiece(ChessColor.White, ChessPieceType.King), new ChessSquare("a2")));
            Assert.True(chess.PutPiece(new ChessPiece(ChessColor.White, ChessPieceType.King), new ChessSquare("a2")));
        }

        [Theory]
        [ClassData(typeof(PutTestData))]
        public void Put_InputFenPieceAndSquare_NotReturnsProvidedMoves(string fen, ChessPiece piece, ChessSquare square, string[] movesToTest)
        {
            var chess = new Chess(fen);

            chess.PutPiece(piece, square);

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

            Assert.Equal(new ChessPiece(ChessColor.White, ChessPieceType.Queen), chess.RemovePiece(new ChessSquare("d1")));
            Assert.Null(chess.GetPiece(new ChessSquare("d1")));
        }

        [Fact]
        public void Remove_EmptySquare_ReturnsNull()
        {
            var chess = new Chess();

            Assert.Null(chess.RemovePiece(new ChessSquare("e4")));
        }

        [Theory]
        [ClassData(typeof(RemoveTestData))]
        public void Remove_InputFenAndSquare_NotReturnsProvidedMoves(string fen, ChessSquare square, string[] movesToTest)
        {
            var chess = new Chess(fen);

            chess.RemovePiece(square);

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

            chess.ClearBoard();
            chess.Reset();

            Assert.Equal(PublicData.DefaultChessPosition, chess.GetFen());
        }

        #endregion

        #region Comments

        [Theory]
        [ClassData(typeof(CommentsPgnTestData))]
        public void GetComments_LoadPgn_ReturnsCorrectComments(string pgn, CommentInfo[] expectedComments)
        {
            var chess = new Chess();

            chess.LoadPgn(pgn);

            var actualComments = chess.GetComments();

            Assert.Equal(expectedComments.Length, actualComments.Length);

            foreach (var comment in expectedComments)
                Assert.Contains(comment, actualComments);
        }

        [Fact]
        public void Comments_NoComments_ReturnsCorrectData()
        {
            var chess = new Chess();

            Assert.Null(chess.GetComment());
            Assert.Empty(chess.GetComments());

            chess.Move("e4");

            Assert.Null(chess.GetComment());
            Assert.Empty(chess.GetComments());
        }

        [Fact]
        public void Comments_CommentForInitialPosition_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.SetComment("Starting position");

            Assert.Equal("Starting position", chess.GetComment());
            Assert.Collection(chess.GetComments(), item => Assert.Equal(item, new CommentInfo(chess.GetFen(), "Starting position")));
        }

        [Fact]
        public void Comments_CommentForFirstMove_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.Move("e4");

            var e4 = chess.GetFen();

            chess.SetComment("Good move");

            Assert.Equal("Good move", chess.GetComment());
            Assert.Collection(chess.GetComments(), item => Assert.Equal(item, new CommentInfo(e4, "Good move")));

            chess.Move("e5");

            Assert.Null(chess.GetComment());
            Assert.Collection(chess.GetComments(), item => Assert.Equal(item, new CommentInfo(e4, "Good move")));
        }

        [Fact]
        public void Comments_CommentForLastMove_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.Move("e4");
            chess.Move("e6");
            chess.SetComment("Dubious move");

            Assert.Equal("Dubious move", chess.GetComment());
            Assert.Collection(chess.GetComments(), item => Assert.Equal(item, new CommentInfo(chess.GetFen(), "Dubious move")));
        }

        [Fact]
        public void Comments_CommentWithBrackets_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.SetComment("{Starting position}");

            Assert.Equal("[Starting position]", chess.GetComment());
        }

        [Fact]
        public void Comments_CommentsWithManyMoves_ReturnsCorrectData()
        {
            var chess = new Chess();

            var initFen = chess.GetFen();

            chess.SetComment("Starting position");

            Assert.Equal("Starting position", chess.GetComment());
            Assert.Collection(chess.GetComments(), item => Assert.Equal(item, new CommentInfo(initFen, "Starting position")));

            chess.Move("e4");

            var e4 = chess.GetFen();

            chess.SetComment("Good move");

            Assert.Equal("Good move", chess.GetComment());
            Assert.Collection(chess.GetComments(), item => Assert.Equal(item, new CommentInfo(initFen, "Starting position")), item => Assert.Equal(item, new CommentInfo(e4, "Good move")));

            chess.Move("e6");

            var e6 = chess.GetFen();

            chess.SetComment("Dubious move");

            Assert.Equal("Dubious move", chess.GetComment());
            Assert.Collection(chess.GetComments(), item => Assert.Equal(item, new CommentInfo(initFen, "Starting position")), item => Assert.Equal(item, new CommentInfo(e4, "Good move")), item => Assert.Equal(item, new CommentInfo(e6, "Dubious move")));
        }

        [Fact]
        public void Comments_DeleteComments_ReturnsCorrectData()
        {
            var chess = new Chess();

            Assert.False(chess.DeleteComment());

            var initFen = chess.GetFen();

            chess.SetComment("Starting position");
            chess.Move("e4");

            var e4 = chess.GetFen();

            chess.SetComment("Good move");
            chess.Move("e6");

            var e6 = chess.GetFen();

            chess.SetComment("Dubious move");

            Assert.Collection(chess.GetComments(), item => Assert.Equal(item, new CommentInfo(initFen, "Starting position")), item => Assert.Equal(item, new CommentInfo(e4, "Good move")), item => Assert.Equal(item, new CommentInfo(e6, "Dubious move")));

            Assert.True(chess.DeleteComment());
            Assert.False(chess.DeleteComment());
        }

        [Fact]
        public void Comments_PruneComments_ReturnsCorrectData()
        {
            var chess = new Chess();

            chess.Move("e4");
            chess.SetComment("Tactical");
            chess.Undo();
            chess.Move("d4");
            chess.SetComment("Positional");

            Assert.Collection(chess.GetComments(), item => Assert.Equal(item, new CommentInfo(chess.GetFen(), "Positional")));
        }

        [Fact]
        public void Comments_ClearComments_ReturnsCorrectData()
        {
            Test(chess => chess.Reset());
            Test(chess => chess.ClearBoard());
            Test(chess => chess.LoadFen(chess.GetFen()));

            void Test(Action<Chess> action)
            {
                var chess = new Chess();

                chess.Move("e4");
                chess.SetComment("Good move");

                Assert.Collection(chess.GetComments(), item => Assert.Equal(item, new CommentInfo(chess.GetFen(), "Good move")));

                action(chess);

                Assert.Empty(chess.GetComments());
            }
        }

        #endregion

        #region Perft

        [Theory]
        [Trait("Category", "CPU_bound")]
        [InlineData(PublicData.DefaultChessPosition, 4, 197281)]
        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 3, 97862)]
        [InlineData("8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w - - 0 1", 4, 43238)]
        [InlineData("r2q1rk1/pP1p2pp/Q4n2/bbp1p3/Np6/1B3NBn/pPPP1PPP/R3K2R b KQ - 0 1", 4, 422333)]
        [InlineData("rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 3, 62379)]
        [InlineData("r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 3, 89890)]
        [InlineData("rnbqkbnr/p3pppp/2p5/1pPp4/3P4/8/PP2PPPP/RNBQKBNR w KQkq b6 0 4", 3, 23509)]
        public void Perft_InputDepth_ReturnCorrectNodesCount(string fen, int depth, int result)
        {
            var chess = new Chess(fen);

            Assert.Equal(result, chess.Perft(depth));
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

            chess.RemovePiece(new ChessSquare("e1"));
            chess.RemovePiece(new ChessSquare("e8"));

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

            chess.RemovePiece(new ChessSquare("e1"));
            chess.RemovePiece(new ChessSquare("e8"));

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

            chess.RemovePiece(new ChessSquare("e1"));
            chess.RemovePiece(new ChessSquare("e8"));

            Assert.False(chess.IsCheckmate());
        }

        #endregion

        #region GetSquareColor

        [Theory]
        [ClassData(typeof(GetSquareColorTestData))]
        public void GetSquareColor_InputSquare_ReturnsCorrectColor(ChessSquare square, ChessColor color)
        {
            var chess = new Chess();

            Assert.Equal(color, chess.GetSquareColor(square));
        }

        #endregion

        #region ChessSquare

        [Theory]
        [ClassData(typeof(ChessSquareV1TestData))]
        public void ChessSquare_ConstructorV1_SuccessOrThrowsException(char file, int rank, bool isOk)
        {
            if (isOk)
                Assert.Null(Record.Exception(() => new ChessSquare(file, rank)));
            else
                Assert.Throws<InvalidChessSquareException>(() => new ChessSquare(file, rank));
        }

        [Theory]
        [ClassData(typeof(ChessSquareV2TestData))]
        public void ChessSquare_ConstructorV2_SuccessOrThrowsException(string square, bool isOk)
        {
            if (isOk)
                Assert.Null(Record.Exception(() => new ChessSquare(square)));
            else
                Assert.Throws<InvalidChessSquareException>(() => new ChessSquare(square));
        }

        [Theory]
        [ClassData(typeof(ChessSquareImplicitConversionTestData))]
        public void ChessSquare_ImplicitConversion_SuccessOrThrowsException(string square, bool isOk)
        {
            var chess = new Chess();

            if (isOk)
            {
                var normalResult = chess.GetPiece(new ChessSquare(square));
                var implicitResult = chess.GetPiece(square);

                Assert.Equal(normalResult, implicitResult);
            }
            else
                Assert.Throws<InvalidChessSquareException>(() => chess.GetPiece(square));
        }

        #endregion

        #region Misc

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest1()
        {
            var chess = new Chess("rnbqk2r/ppp1pp1p/5n1b/3p2pQ/1P2P3/B1N5/P1PP1PPP/R3KBNR b KQkq - 3 5");

            Assert.Empty(chess.GetMovesVerbose(square: "f1"));
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest2()
        {
            var chess = new Chess("b3k2r/5p2/4p3/1p5p/6p1/2PR2P1/BP3qNP/6QK b k - 2 28");

            chess.Move(new MoveInfo("a8", "g2"));

            Assert.Equal("4k2r/5p2/4p3/1p5p/6p1/2PR2P1/BP3qbP/6QK w k - 0 29", chess.GetFen());
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest3()
        {
            var chess = new Chess("N3k3/8/8/8/8/8/5b2/4K3 w - - 0 1");

            Assert.False(chess.PutPiece(new ChessPiece(ChessColor.White, ChessPieceType.King), "a1"));

            chess.PutPiece(new ChessPiece(ChessColor.White, ChessPieceType.Queen), "a1");
            chess.RemovePiece("a1");

            Assert.Equal("Kd2 Ke2 Kxf2 Kf1 Kd1", string.Join(" ", chess.GetMoves()));
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest4()
        {
            var chess = new Chess();

            var pgn = "[SetUp \"1\"]\r\n[FEN \"7k/5K2/4R3/8/8/8/8/8 w KQkq - 0 1\"]\r\n\r\n1. Rh6#";

            chess.LoadPgn(pgn);

            Assert.Equal("7k/5K2/7R/8/8/8/8/8 b KQkq - 1 1", chess.GetFen());
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest5()
        {
            var chess = new Chess();

            var pgn = "[SetUp \"1\"]\r\n[FEN \"r4r1k/1p4b1/3p3p/5qp1/1RP5/6P1/3NP3/2Q2RKB b KQkq - 0 1\"]\r\n\r\n1. ... Qc5+";

            chess.LoadPgn(pgn);

            Assert.Equal("r4r1k/1p4b1/3p3p/2q3p1/1RP5/6P1/3NP3/2Q2RKB w KQkq - 1 2", chess.GetFen());
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest6()
        {
            var chess = new Chess();

            chess.LoadFen("4r3/8/2p2PPk/1p6/pP2p1R1/P1B5/2P2K2/3r4 w - - 1 45");
            chess.Move("f7");

            var pgn = chess.GetPgn();

            Assert.Matches(@"(45\. f7)$", pgn);
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest7()
        {
            var chess = new Chess();

            chess.LoadFen("4r3/8/2p2PPk/1p6/pP2p1R1/P1B5/2P2K2/3r4 b - - 1 45");
            chess.Move("Rf1+");

            var pgn = chess.GetPgn();

            Assert.Matches(@"(45\. \.\.\. Rf1\+)$", pgn);
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest8()
        {
            var chess = new Chess();

            var pgn = new[]
            {
                "[Event \"Test Olympiad\"]",
                "[Site \"Earth\"]",
                "[Date \"????.??.??\"]",
                "[Round \"6\"]",
                "[White \"Testy\"]",
                "[Black \"McTest\"]",
                "[Result \"*\"]",
                "[FEN \"rnbqkb1r/1p3ppp/p2ppn2/6B1/3NP3/2N5/PPP2PPP/R2QKB1R w KQkq - 0 1\"]",
                "[SetUp \"1\"]",
                "",
                "1.Qd2 Be7 *"
            };

            chess.LoadPgn(string.Join("\r\n", pgn));

            var expectedHeaders = new[]
            {
                new PgnHeader("Event", "Test Olympiad"),
                new PgnHeader("Site", "Earth"),
                new PgnHeader("Date", "????.??.??"),
                new PgnHeader("Round", "6"),
                new PgnHeader("White", "Testy"),
                new PgnHeader("Black", "McTest"),
                new PgnHeader("Result", "*"),
                new PgnHeader("FEN", "rnbqkb1r/1p3ppp/p2ppn2/6B1/3NP3/2N5/PPP2PPP/R2QKB1R w KQkq - 0 1"),
                new PgnHeader("SetUp", "1"),
            };

            var actualHeaders = chess.GetHeaders();

            Assert.Equal(expectedHeaders.Length, actualHeaders.Length);

            foreach (var header in expectedHeaders)
                Assert.Contains(header, actualHeaders);
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest9()
        {
            var chess = new Chess();

            var pgn = new[]
            {
                "[Event \"Test Olympiad\"]",
                "[Site \"Earth\"]",
                "[Date \"????.??.??\"]",
                "[Round \"6\"]",
                "[White \"Testy\"]",
                "[Black \"McTest\"]",
                "[Result \"*\"]",
                "[FEN \"rnbqkb1r/1p3ppp/p2ppn2/6B1/3NP3/2N5/PPP2PPP/R2QKB1R w KQkq - 0 1\"]",
                "[SetUp \"1\"]",
                "",
                "1.Qd2 Be7 *"
            };

            chess.LoadPgn(string.Join("\r\n", pgn));
            chess.ClearBoard();

            var actualHeaders = chess.GetHeaders();

            Assert.Empty(actualHeaders);
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest10()
        {
            var chess = new Chess();

            var pgn = new[]
            {
                "[Event \"Reykjavik WCh\"]",
                "[Site \"Reykjavik WCh\"]",
                "[Date \"1972.01.07\" ]",
                "[EventDate \"?\"]",
                "[Round \"6\"]",
                "[Result \"1-0\"]",
                "[White \"Robert James Fischer\"]",
                "[Black \"Boris Spassky\"]",
                "[ECO \"D59\"]",
                "[WhiteElo \"?\"]",
                "[BlackElo \"?\"]",
                "[PlyCount \"81\"]",
                "",
                "1. c4 e6 2. Nf3 d5 3. d4 Nf6 4. Nc3 Be7 5. Bg5 O-O 6. e3 h6",
                "7. Bh4 b6 8. cxd5 Nxd5 9. Bxe7 Qxe7 10. Nxd5 exd5 11. Rc1 Be6",
                "12. Qa4 c5 13. Qa3 Rc8 14. Bb5 a6 15. dxc5 bxc5 16. O-O Ra7",
                "17. Be2 Nd7 18. Nd4 Qf8 19. Nxe6 fxe6 20. e4 d4 21. f4 Qe7",
                "22. e5 Rb8 23. Bc4 Kh8 24. Qh3 Nf8 25. b3 a5 26. f5 exf5",
                "27. Rxf5 Nh7 28. Rcf1 Qd8 29. Qg3 Re7 30. h4 Rbb7 31. e6 Rbc7",
                "32. Qe5 Qe8 33. a4 Qd8 34. R1f2 Qe8 35. R2f3 Qd8 36. Bd3 Qe8",
                "37. Qe4 Nf6 38. Rxf6 gxf6 39. Rxf6 Kg8 40. Bc4 Kh8 41. Qf4 1-0",
            };

            chess.LoadPgn(string.Join("\r\n", pgn));

            var actualHeaders = chess.GetHeaders();

            Assert.Equal("1972.01.07", actualHeaders.FirstOrDefault(h => h.Key == "Date")?.Value);
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest11()
        {
            var chess = new Chess("4k3/8/8/8/8/4p3/8/4K3 w - - 0 1");

            Assert.Throws<InvalidMoveException>(() => chess.Move("e4"));
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest12()
        {
            var chess = new Chess();

            chess.ClearBoard();

            Assert.Throws<InvalidMoveException>(() => chess.Move("e4"));
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest13()
        {
            var chess = new Chess();

            var expectedHistory = new[]
            {
                "e4",
                "e5",
                "Nf3",
                "Nc6",
                "Bb5",
                "d6",
                "d4",
                "Bd7",
                "Nc3",
                "Nf6",
                "Bxc6",
            };

            var pgns = new[]
            {
                "1. e4 e5 2. Nf3 Nc6 3. Bb5 d6 4. d4 Bd7 5. Nc3 Nf6 6. Bxc6 {comment}",
                "1. e4 e5 2. Nf3 Nc6 3. Bb5 d6 4. d4 Bd7 5. Nc3 Nf6 6. Bxc6 {comment} *",
                "1. e4 e5 2. Nf3 Nc6 3. Bb5 d6 4. d4 Bd7 5. Nc3 Nf6 6. Bxc6 * {comment}",
                "[White \"name\"]\r\n\r\n1. e4 e5 2. Nf3 Nc6 3. Bb5 d6 4. d4 Bd7 5. Nc3 Nf6 6. Bxc6 {comment}"
            };

            string[] actualHistory;

            foreach (var pgn in pgns)
            {
                chess.LoadPgn(pgn);

                actualHistory = chess.GetHistory();

                Assert.Equal(expectedHistory.Length, actualHistory.Length);

                foreach (var history in expectedHistory)
                    Assert.Contains(history, actualHistory);

                Assert.DoesNotContain(chess.GetHeaders(), header => header.Key == "Result");
            }

            chess.LoadPgn("[White \"name\"]\r\n\r\n1. e4 e5 2. Nf3 Nc6 3. Bb5 d6 4. d4 Bd7 5. Nc3 Nf6 6. Bxc6 {comment} *");

            actualHistory = chess.GetHistory();

            Assert.Equal(expectedHistory.Length, actualHistory.Length);

            foreach (var history in expectedHistory)
                Assert.Contains(history, actualHistory);

            Assert.Equal("*", chess.GetHeaders().FirstOrDefault(h => h.Key == "Result")?.Value);

            chess.LoadPgn("[White \"name\"]\r\n\r\n1. e4 e5 2. Nf3 Nc6 3. Bb5 d6 4. d4 Bd7 5. Nc3 Nf6 6. Bxc6 1/2-1/2 {comment}");

            actualHistory = chess.GetHistory();

            Assert.Equal(expectedHistory.Length, actualHistory.Length);

            foreach (var history in expectedHistory)
                Assert.Contains(history, actualHistory);

            Assert.Equal("1/2-1/2", chess.GetHeaders().FirstOrDefault(h => h.Key == "Result")?.Value);
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest14()
        {
            var chess = new Chess();

            chess.LoadPgn("1. e4 d5 2. Nf3 Nd7 3. Bb5 Nf6 4. O-O");

            Assert.Equal("1. e4 d5 2. Nf3 Nd7 3. Bb5 Nf6 4. O-O", chess.GetPgn());
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest15()
        {
            var chess = new Chess();

            chess.LoadFen("r4rk1/4nqpp/1p1p4/2pPpp2/bPP1P3/R1B1NQ2/P4PPP/1R4K1 w - - 0 28");
            chess.Move("bxc5");
            chess.Undo();
            chess.Move("bxc5", true);
            chess.LoadFen("rnbqk2r/p1pp1ppp/1p2pn2/8/1bPP4/2N1P3/PP3PPP/R1BQKBNR w KQkq - 0 5");

            Assert.Throws<InvalidMoveException>(() => chess.Move("Nge2", true));
            Assert.Null(Record.Exception(() => chess.Move("Nge2")));
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest16()
        {
            var chess = new Chess();

            var pgn = "\r\n    [white \"player a\"]\r\n         [black \"player b\"]\r\n              [note \"whitespace after right bracket\"]\r\n\r\n            1. e4 e5";

            Assert.Null(Record.Exception(() => chess.LoadPgn(pgn)));
        }

        [Fact]
        [Trait("Category", "Regression")]
        public void RegressionTest17()
        {
            var chess = new Chess();

            var pgn = "\r\n    [white \"player a\"]\r\n         [black \"player b\"]\r\n              [note \"whitespace after right bracket and in empty line below\"]\r\n\r\n            1. e4 e5";

            Assert.Null(Record.Exception(() => chess.LoadPgn(pgn)));
        }
        #endregion
    }
}
