using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class PutTestData : TheoryData<string, ChessPiece, ChessSquare, string[]>
    {
        public PutTestData()
        {
            Add("r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 1", new ChessPiece(ChessColor.White, ChessPieceType.Knight), new ChessSquare("h1"), new string[]
            {
                "O-O"
            });
            Add("r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 1", new ChessPiece(ChessColor.White, ChessPieceType.Knight), new ChessSquare("a1"), new string[]
            {
                "O-O-O"
            });
            Add("r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 1", new ChessPiece(ChessColor.White, ChessPieceType.Knight), new ChessSquare("e1"), new string[]
            {
                "O-O",
                "O-O-O"
            });
            Add("r3k2r/8/8/8/8/8/8/R3K2R b KQkq - 0 1", new ChessPiece(ChessColor.Black, ChessPieceType.Knight), new ChessSquare("h8"), new string[]
            {
                "O-O"
            });
            Add("r3k2r/8/8/8/8/8/8/R3K2R b KQkq - 0 1", new ChessPiece(ChessColor.Black, ChessPieceType.Knight), new ChessSquare("a8"), new string[]
            {
                "O-O-O"
            });
            Add("r3k2r/8/8/8/8/8/8/R3K2R b KQkq - 0 1", new ChessPiece(ChessColor.Black, ChessPieceType.Knight), new ChessSquare("e8"), new string[]
            {
                "O-O",
                "O-O-O"
            });
            Add("rnbqkbnr/pppppp1p/8/8/3PPPp1/8/PPP3PP/RNBQKBNR b KQkq f3 0 3", new ChessPiece(ChessColor.White, ChessPieceType.Knight), new ChessSquare("f4"), new string[]
            {
                "gxf3"
            });
            Add("rnbqkbnr/pppppp1p/8/8/3PPPp1/8/PPP3PP/RNBQKBNR b KQkq f3 0 3", new ChessPiece(ChessColor.Black, ChessPieceType.Knight), new ChessSquare("f3"), new string[]
            {
                "gxf3"
            });
            Add("rnbqkbnr/pppppp1p/8/8/3PPPp1/8/PPP3PP/RNBQKBNR b KQkq f3 0 3", new ChessPiece(ChessColor.White, ChessPieceType.Knight), new ChessSquare("f2"), new string[]
            {
                "gxf3"
            });
            Add("rnbqkbnr/pppppp1p/8/8/3PPPp1/8/PPP3PP/RNBQKBNR b KQkq f3 0 3", new ChessPiece(ChessColor.Black, ChessPieceType.Bishop), new ChessSquare("g4"), new string[]
            {
                "gxf3"
            });
            Add("rnbqkbnr/p1pppppp/8/8/1pPPP3/8/PP3PPP/RNBQKBNR b KQkq c3 0 3", new ChessPiece(ChessColor.Black, ChessPieceType.Bishop), new ChessSquare("b4"), new string[]
            {
                "bxc3"
            });
            Add("rnbqkbnr/pppp2pp/8/4ppP1/8/8/PPPPPP1P/RNBQKBNR w KQkq f6 0 3", new ChessPiece(ChessColor.Black, ChessPieceType.Knight), new ChessSquare("f5"), new string[]
            {
                "gxf6"
            });
            Add("rnbqkbnr/pppp2pp/8/4ppP1/8/8/PPPPPP1P/RNBQKBNR w KQkq f6 0 3", new ChessPiece(ChessColor.White, ChessPieceType.Knight), new ChessSquare("f6"), new string[]
            {
                "gxf6"
            });
            Add("rnbqkbnr/pppp2pp/8/4ppP1/8/8/PPPPPP1P/RNBQKBNR w KQkq f6 0 3", new ChessPiece(ChessColor.Black, ChessPieceType.Knight), new ChessSquare("f7"), new string[]
            {
                "gxf6"
            });
            Add("rnbqkbnr/pppp2pp/8/4ppP1/8/8/PPPPPP1P/RNBQKBNR w KQkq f6 0 3", new ChessPiece(ChessColor.White, ChessPieceType.Bishop), new ChessSquare("g5"), new string[]
            {
                "gxf6"
            });
            Add("rnbqkbnr/pp2pppp/8/1Ppp4/8/8/P1PPPPPP/RNBQKBNR w KQkq c6 0 3", new ChessPiece(ChessColor.White, ChessPieceType.Bishop), new ChessSquare("b5"), new string[]
            {
                "bxc6"
            });
        }
    }
}
