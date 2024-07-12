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

    public class MovesAsStringTestData : TheoryData<string, string>
    {
        public MovesAsStringTestData()
        {
            Add("7k/3R4/3p2Q1/6Q1/2N1N3/8/8/3R3K w - - 0 1", "Rd8# Re7 Rf7 Rg7 Rh7# R7xd6 Rc7 Rb7 Ra7 Qf7 Qe8# Qg7# Qg8# Qh7# Q6h6# Q6h5# Q6f5 Q6f6# Qe6 Qxd6 Q5f6# Qe7 Qd8# Q5h6# Q5h5# Qh4# Qg4 Qg3 Qg2 Qg1 Qf4 Qe3 Qd2 Qc1 Q5f5 Qe5+ Qd5 Qc5 Qb5 Qa5 Na5 Nb6 Ncxd6 Ne5 Ne3 Ncd2 Nb2 Na3 Nc5 Nexd6 Nf6 Ng3 Nf2 Ned2 Nc3 Rd2 Rd3 Rd4 Rd5 R1xd6 Re1 Rf1 Rg1 Rc1 Rb1 Ra1 Kg2 Kh2 Kg1");
            Add("1r3k2/P1P5/8/8/8/8/8/R3K2R w KQ - 0 1", "a8=Q a8=R a8=B a8=N axb8=Q+ axb8=R+ axb8=B axb8=N c8=Q+ c8=R+ c8=B c8=N cxb8=Q+ cxb8=R+ cxb8=B cxb8=N Ra2 Ra3 Ra4 Ra5 Ra6 Rb1 Rc1 Rd1 Kd2 Ke2 Kf2 Kf1 Kd1 Rh2 Rh3 Rh4 Rh5 Rh6 Rh7 Rh8+ Rg1 Rf1+ O-O+ O-O-O");
            Add("5rk1/8/8/8/8/8/2p5/R3K2R w KQ - 0 1", "Ra2 Ra3 Ra4 Ra5 Ra6 Ra7 Ra8 Rb1 Rc1 Rd1 Kd2 Ke2 Rh2 Rh3 Rh4 Rh5 Rh6 Rh7 Rh8+ Rg1+ Rf1");
            Add("5rk1/8/8/8/8/8/2p5/R3K2R b KQ - 0 1", "Rf7 Rf6 Rf5 Rf4 Rf3 Rf2 Rf1+ Re8+ Rd8 Rc8 Rb8 Ra8 Kg7 Kf7 c1=Q+ c1=R+ c1=B c1=N");
            Add("r3k2r/p2pqpb1/1n2pnp1/2pPN3/1p2P3/2N2Q1p/PPPB1PPP/R3K2R w KQkq c6 0 2", "gxh3 Qxf6 Qxh3 Nxd7 Nxf7 Nxg6 dxc6 dxe6 Rg1 Rf1 Ke2 Kf1 Kd1 Rb1 Rc1 Rd1 g3 g4 Be3 Bf4 Bg5 Bh6 Bc1 b3 a3 a4 Qf4 Qf5 Qg4 Qh5 Qg3 Qe2 Qd1 Qe3 Qd3 Na4 Nb5 Ne2 Nd1 Nb1 Nc6 Ng4 Nd3 Nc4 d6 O-O O-O-O");
            Add("k7/8/K7/8/3n3n/5R2/3n4/8 b - - 0 1", "N2xf3 Nhxf3 Nd4xf3 N2b3 Nc4 Ne4 Nf1 Nb1 Nhf5 Ng6 Ng2 Nb5 Nc6 Ne6 Ndf5 Ne2 Nc2 N4b3 Kb8");
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
