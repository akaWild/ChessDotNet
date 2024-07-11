using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class BoardCorrectTestData : TheoryData<string, BoardItem?[][]>
    {
        public BoardCorrectTestData()
        {
            Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", new[]
            {
                new BoardItem[]
                {
                    new(new ChessSquare("a8"), ChessPieceType.Rook, ChessColor.Black),
                    new(new ChessSquare("b8"), ChessPieceType.Knight, ChessColor.Black),
                    new(new ChessSquare("c8"), ChessPieceType.Bishop, ChessColor.Black),
                    new(new ChessSquare("d8"), ChessPieceType.Queen, ChessColor.Black),
                    new(new ChessSquare("e8"), ChessPieceType.King, ChessColor.Black),
                    new(new ChessSquare("f8"), ChessPieceType.Bishop, ChessColor.Black),
                    new(new ChessSquare("g8"), ChessPieceType.Knight, ChessColor.Black),
                    new(new ChessSquare("h8"), ChessPieceType.Rook, ChessColor.Black),
                },
                new BoardItem[]
                {
                    new(new ChessSquare("a7"), ChessPieceType.Pawn, ChessColor.Black),
                    new(new ChessSquare("b7"), ChessPieceType.Pawn, ChessColor.Black),
                    new(new ChessSquare("c7"), ChessPieceType.Pawn, ChessColor.Black),
                    new(new ChessSquare("d7"), ChessPieceType.Pawn, ChessColor.Black),
                    new(new ChessSquare("e7"), ChessPieceType.Pawn, ChessColor.Black),
                    new(new ChessSquare("f7"), ChessPieceType.Pawn, ChessColor.Black),
                    new(new ChessSquare("g7"), ChessPieceType.Pawn, ChessColor.Black),
                    new(new ChessSquare("h7"), ChessPieceType.Pawn, ChessColor.Black),
                },
                new BoardItem?[] { null, null, null, null, null, null, null, null},
                new BoardItem?[] { null, null, null, null, null, null, null, null},
                new BoardItem?[] { null, null, null, null, null, null, null, null},
                new BoardItem?[] { null, null, null, null, null, null, null, null},
                new BoardItem[]
                {
                    new(new ChessSquare("a2"), ChessPieceType.Pawn, ChessColor.White),
                    new(new ChessSquare("b2"), ChessPieceType.Pawn, ChessColor.White),
                    new(new ChessSquare("c2"), ChessPieceType.Pawn, ChessColor.White),
                    new(new ChessSquare("d2"), ChessPieceType.Pawn, ChessColor.White),
                    new(new ChessSquare("e2"), ChessPieceType.Pawn, ChessColor.White),
                    new(new ChessSquare("f2"), ChessPieceType.Pawn, ChessColor.White),
                    new(new ChessSquare("g2"), ChessPieceType.Pawn, ChessColor.White),
                    new(new ChessSquare("h2"), ChessPieceType.Pawn, ChessColor.White),
                },
                new BoardItem[]
                {
                    new(new ChessSquare("a1"), ChessPieceType.Rook, ChessColor.White),
                    new(new ChessSquare("b1"), ChessPieceType.Knight, ChessColor.White),
                    new(new ChessSquare("c1"), ChessPieceType.Bishop, ChessColor.White),
                    new(new ChessSquare("d1"), ChessPieceType.Queen, ChessColor.White),
                    new(new ChessSquare("e1"), ChessPieceType.King, ChessColor.White),
                    new(new ChessSquare("f1"), ChessPieceType.Bishop, ChessColor.White),
                    new(new ChessSquare("g1"), ChessPieceType.Knight, ChessColor.White),
                    new(new ChessSquare("h1"), ChessPieceType.Rook, ChessColor.White),
                }
            });

            Add("r3k2r/ppp2p1p/2n1p1p1/8/2B2P1q/2NPb1n1/PP4PP/R2Q3K w kq - 0 8", new[]
{
                new BoardItem?[]
                {
                    new(new ChessSquare("a8"), ChessPieceType.Rook, ChessColor.Black),
                    null,
                    null,
                    null,
                    new(new ChessSquare("e8"), ChessPieceType.King, ChessColor.Black),
                    null,
                    null,
                    new(new ChessSquare("h8"), ChessPieceType.Rook, ChessColor.Black),
                },
                new BoardItem?[]
                {
                    new(new ChessSquare("a7"), ChessPieceType.Pawn, ChessColor.Black),
                    new(new ChessSquare("b7"), ChessPieceType.Pawn, ChessColor.Black),
                    new(new ChessSquare("c7"), ChessPieceType.Pawn, ChessColor.Black),
                    null,
                    null,
                    new(new ChessSquare("f7"), ChessPieceType.Pawn, ChessColor.Black),
                    null,
                    new(new ChessSquare("h7"), ChessPieceType.Pawn, ChessColor.Black),
                },
                new BoardItem?[]
                {
                    null,
                    null,
                    new(new ChessSquare("c6"), ChessPieceType.Knight, ChessColor.Black),
                    null,
                    new(new ChessSquare("e6"), ChessPieceType.Pawn, ChessColor.Black),
                    null,
                    new(new ChessSquare("g6"), ChessPieceType.Pawn, ChessColor.Black),
                    null
                },
                new BoardItem?[] { null, null, null, null, null, null, null, null},
                new BoardItem?[]
                {
                    null,
                    null,
                    new(new ChessSquare("c4"), ChessPieceType.Bishop, ChessColor.White),
                    null,
                    null,
                    new(new ChessSquare("f4"), ChessPieceType.Pawn, ChessColor.White),
                    null,
                    new(new ChessSquare("h4"), ChessPieceType.Queen, ChessColor.Black),
                },
                new BoardItem?[]
                {
                    null,
                    null,
                    new(new ChessSquare("c3"), ChessPieceType.Knight, ChessColor.White),
                    new(new ChessSquare("d3"), ChessPieceType.Pawn, ChessColor.White),
                    new(new ChessSquare("e3"), ChessPieceType.Bishop, ChessColor.Black),
                    null,
                    new(new ChessSquare("g3"), ChessPieceType.Knight, ChessColor.Black),
                    null
                },
                new BoardItem?[]
                {
                    new(new ChessSquare("a2"), ChessPieceType.Pawn, ChessColor.White),
                    new(new ChessSquare("b2"), ChessPieceType.Pawn, ChessColor.White),
                    null,
                    null,
                    null,
                    null,
                    new(new ChessSquare("g2"), ChessPieceType.Pawn, ChessColor.White),
                    new(new ChessSquare("h2"), ChessPieceType.Pawn, ChessColor.White),
                },
                new BoardItem?[]
                {
                    new(new ChessSquare("a1"), ChessPieceType.Rook, ChessColor.White),
                    null,
                    null,
                    new(new ChessSquare("d1"), ChessPieceType.Queen, ChessColor.White),
                    null,
                    null,
                    null,
                    new(new ChessSquare("h1"), ChessPieceType.King, ChessColor.White),
                }
            });
        }
    }
}
