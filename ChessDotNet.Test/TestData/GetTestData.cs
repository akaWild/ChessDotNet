using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class GetTestData : TheoryData<string, ChessSquare, ChessPiece?>
    {
        public GetTestData()
        {
            Add(PublicData.DefaultChessPosition, new ChessSquare("a2"), new ChessPiece(ChessColor.White, ChessPieceType.Pawn));
            Add(PublicData.DefaultChessPosition, new ChessSquare("a7"), new ChessPiece(ChessColor.Black, ChessPieceType.Pawn));
            Add(PublicData.DefaultChessPosition, new ChessSquare("a4"), null);
            Add("rnbqkbnr/ppp1p1pp/5p2/3p4/2B1P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 1 3", new ChessSquare("c4"), new ChessPiece(ChessColor.White, ChessPieceType.Bishop));
            Add("rnb1kbnr/ppp1p1pp/5p2/3p4/2BNP3/q7/PPPP1PPP/RNBQK2R w KQkq - 4 5", new ChessSquare("a3"), new ChessPiece(ChessColor.Black, ChessPieceType.Queen));
            Add("rnb1kbnr/ppp1p1pp/5p2/3p4/2BNP3/q7/PPPP1PPP/RNBQK2R w KQkq - 4 5", new ChessSquare("g5"), null);
        }
    }
}
