using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class MovesTestData : TheoryData<string, ChessPieceType?, ChessSquare?, string[]>
    {
        public MovesTestData()
        {
            Add(PublicData.DefaultChessPosition, null, null, new string[]
            {
                "a3",
                "a4",
                "b3",
                "b4",
                "c3",
                "c4",
                "d3",
                "d4",
                "e3",
                "e4",
                "f3",
                "f4",
                "g3",
                "g4",
                "h3",
                "h4",
                "Na3",
                "Nc3",
                "Nf3",
                "Nh3"
            });
            Add(PublicData.DefaultChessPosition, null, new ChessSquare("e2"), new string[]
            {
                "e3",
                "e4",
            });
            Add("rnbqk1nr/pppp1ppp/4p3/8/1b1P4/2N5/PPP1PPPP/R1BQKBNR w KQkq - 2 3", null, new ChessSquare("c3"), new string[] { });
            Add("8/k7/8/8/8/8/7p/K7 b - - 0 1", null, new ChessSquare("h2"), new string[]
            {
                "h1=N",
                "h1=B",
                "h1=R+",
                "h1=Q+",
            });
            Add("r1bq1rk1/1pp2ppp/p1np1n2/2b1p3/2B1P3/2NP1N2/PPPBQPPP/R3K2R w KQ - 0 8", null, new ChessSquare("e1"), new string[]
            {
                "Kf1",
                "Kd1",
                "O-O",
                "O-O-O",
            });
            Add("r1bq1rk1/1pp2ppp/p1np1n2/2b1p3/2B1P3/2NP1N2/PPPBQPPP/R3K2R w - - 0 8", null, new ChessSquare("e1"), new string[]
            {
                "Kf1",
                "Kd1",
            });
            Add("8/7K/8/8/1R6/k7/1R1p4/8 b - - 0 1", null, new ChessSquare("a3"), new string[] { });
            Add(PublicData.DefaultChessPosition, ChessPieceType.Knight, null, new string[]
            {
                "Na3",
                "Nc3",
                "Nf3",
                "Nh3",
            });
            Add("rnbq1rk1/4bpp1/p2p1n1p/Ppp1p3/2B1P3/2NP1N1P/1PP2PP1/R1BQ1RK1 w - b6 0 10", ChessPieceType.Pawn, null, new string[]
            {
                "axb6",
                "b3",
                "b4",
                "d4",
                "g3",
                "g4",
                "h4",
            });
            Add("r1bq1rk1/1pp2ppp/p1np1n2/2b1p3/4P3/2NP1N2/PPP1QPPP/R3K2R w KQ - 0 8", ChessPieceType.Bishop, null, new string[] { });
            Add("5rk1/1p3rp1/p1n1p3/2p1p2p/2PpP1qP/P2P2P1/1P2QP1K/3R1R2 w - - 0 23", ChessPieceType.Queen, new ChessSquare("e2"), new string[]
            {
                "Qd2",
                "Qc2",
                "Qe1",
                "Qe3",
                "Qf3",
                "Qxg4",
            });
        }
    }

    public class MovesVerboseTestData : TheoryData<string, ChessPieceType?, ChessSquare?, ChessMove[]>
    {
        public MovesVerboseTestData()
        {
            Add("8/7K/8/8/1R6/k7/1R1p4/8 b - - 0 1", null, new ChessSquare("d2"), new ChessMove[]
            {
                new ChessMove(
                    ChessColor.Black,
                    new ChessSquare("d2"),
                    new ChessSquare("d1"),
                    ChessPieceType.Pawn,
                    null,
                    ChessPieceType.Queen,
                    "np",
                    "d1=Q",
                    "d2d1q",
                    "8/7K/8/8/1R6/k7/1R1p4/8 b - - 0 1",
                    "8/7K/8/8/1R6/k7/1R6/3q4 w - - 0 2"
                    ),
                new ChessMove(
                    ChessColor.Black,
                    new ChessSquare("d2"),
                    new ChessSquare("d1"),
                    ChessPieceType.Pawn,
                    null,
                    ChessPieceType.Rook,
                    "np",
                    "d1=R",
                    "d2d1r",
                    "8/7K/8/8/1R6/k7/1R1p4/8 b - - 0 1",
                    "8/7K/8/8/1R6/k7/1R6/3r4 w - - 0 2"
                ),
                new ChessMove(
                    ChessColor.Black,
                    new ChessSquare("d2"),
                    new ChessSquare("d1"),
                    ChessPieceType.Pawn,
                    null,
                    ChessPieceType.Bishop,
                    "np",
                    "d1=B",
                    "d2d1b",
                    "8/7K/8/8/1R6/k7/1R1p4/8 b - - 0 1",
                    "8/7K/8/8/1R6/k7/1R6/3b4 w - - 0 2"
                ),
                new ChessMove(
                    ChessColor.Black,
                    new ChessSquare("d2"),
                    new ChessSquare("d1"),
                    ChessPieceType.Pawn,
                    null,
                    ChessPieceType.Knight,
                    "np",
                    "d1=N",
                    "d2d1n",
                    "8/7K/8/8/1R6/k7/1R1p4/8 b - - 0 1",
                    "8/7K/8/8/1R6/k7/1R6/3n4 w - - 0 2"
                ),
            });

            Add("r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R1R3K1 w - - 0 19", ChessPieceType.Rook, null, new ChessMove[]
            {
                new ChessMove(
                    ChessColor.White,
                    new ChessSquare("a1"),
                    new ChessSquare("a2"),
                    ChessPieceType.Rook,
                    null,
                    null,
                    "n",
                    "Ra2",
                    "a1a2",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R1R3K1 w - - 0 19",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/RP2QPP1/2R3K1 b - - 1 19"
                ),
                new ChessMove(
                    ChessColor.White,
                    new ChessSquare("a1"),
                    new ChessSquare("b1"),
                    ChessPieceType.Rook,
                    null,
                    null,
                    "n",
                    "Rab1",
                    "a1b1",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R1R3K1 w - - 0 19",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/1RR3K1 b - - 1 19"
                ),
                new ChessMove(
                    ChessColor.White,
                    new ChessSquare("c1"),
                    new ChessSquare("c2"),
                    ChessPieceType.Rook,
                    null,
                    null,
                    "n",
                    "Rc2",
                    "c1c2",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R1R3K1 w - - 0 19",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1PR1QPP1/R5K1 b - - 1 19"
                ),
                new ChessMove(
                    ChessColor.White,
                    new ChessSquare("c1"),
                    new ChessSquare("c3"),
                    ChessPieceType.Rook,
                    ChessPieceType.Pawn,
                    null,
                    "c",
                    "Rxc3",
                    "c1c3",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R1R3K1 w - - 0 19",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1RP3P/1P2QPP1/R5K1 b - - 0 19"
                ),
                new ChessMove(
                    ChessColor.White,
                    new ChessSquare("c1"),
                    new ChessSquare("d1"),
                    ChessPieceType.Rook,
                    null,
                    null,
                    "n",
                    "Rd1",
                    "c1d1",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R1R3K1 w - - 0 19",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R2R2K1 b - - 1 19"
                ),
                new ChessMove(
                    ChessColor.White,
                    new ChessSquare("c1"),
                    new ChessSquare("e1"),
                    ChessPieceType.Rook,
                    null,
                    null,
                    "n",
                    "Re1",
                    "c1e1",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R1R3K1 w - - 0 19",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R3R1K1 b - - 1 19"
                ),
                new ChessMove(
                    ChessColor.White,
                    new ChessSquare("c1"),
                    new ChessSquare("f1"),
                    ChessPieceType.Rook,
                    null,
                    null,
                    "n",
                    "Rf1",
                    "c1f1",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R1R3K1 w - - 0 19",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R4RK1 b - - 1 19"
                ),
                new ChessMove(
                    ChessColor.White,
                    new ChessSquare("c1"),
                    new ChessSquare("b1"),
                    ChessPieceType.Rook,
                    null,
                    null,
                    "n",
                    "Rcb1",
                    "c1b1",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/R1R3K1 w - - 0 19",
                    "r4rk1/1p4p1/p1n1p2p/2p1p1q1/4P1N1/P1pP3P/1P2QPP1/RR4K1 b - - 1 19"
                ),
            });
        }
    }
}
