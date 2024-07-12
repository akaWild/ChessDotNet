using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class GetSquareColorTestData : TheoryData<ChessSquare, ChessColor>
    {
        public GetSquareColorTestData()
        {
            Add(new ChessSquare("a3"), ChessColor.Black);
            Add(new ChessSquare("b6"), ChessColor.Black);
            Add(new ChessSquare("e5"), ChessColor.Black);
            Add(new ChessSquare("g7"), ChessColor.Black);
            Add(new ChessSquare("a2"), ChessColor.White);
            Add(new ChessSquare("c4"), ChessColor.White);
            Add(new ChessSquare("f3"), ChessColor.White);
            Add(new ChessSquare("h5"), ChessColor.White);
        }
    }
}
