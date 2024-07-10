using ChessDotNet.Exceptions;
using ChessDotNet.Public;
using ChessDotNet.Tests.TestData;

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
        public void Load_Constructor_ReturnsDefaultPosition()
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
    }
}
