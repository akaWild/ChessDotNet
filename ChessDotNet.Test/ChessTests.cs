using ChessDotNet.Public;

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
    }
}
