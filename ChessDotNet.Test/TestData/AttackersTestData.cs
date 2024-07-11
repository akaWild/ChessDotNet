using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class AttackersCountPerSquareTestData : TheoryData<string, ChessColor, int[]>
    {
        public AttackersCountPerSquareTestData()
        {
            Add(PublicData.DefaultChessPosition, ChessColor.White, new[]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                2, 2, 3, 2, 2, 3, 2, 2,
                1, 1, 1, 4, 4, 1, 1, 1,
                0, 1, 1, 1, 1, 1, 1, 0,
            });
            Add(PublicData.DefaultChessPosition, ChessColor.Black, new[]
            {
                0, 1, 1, 1, 1, 1, 1, 0,
                1, 1, 1, 4, 4, 1, 1, 1,
                2, 2, 3, 2, 2, 3, 2, 2,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
            });
            Add("r3kb1r/1b3ppp/pqnppn2/1p6/4PBP1/PNN5/1PPQBP1P/2KR3R b kq - 0 1", ChessColor.White, new[]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 2, 0, 0, 0, 1,
                1, 2, 1, 3, 1, 2, 1, 1,
                1, 1, 1, 2, 1, 1, 1, 0,
                1, 1, 2, 3, 3, 1, 3, 0,
                1, 1, 2, 4, 2, 0, 0, 2,
                1, 2, 3, 5, 3, 3, 2, 1,
            });
            Add("r3kb1r/1b3ppp/pqnppn2/1p6/4PBP1/PNN5/1PPQBP1P/2KR3R b kq - 0 1", ChessColor.Black, new[]
            {
                1, 2, 2, 4, 2, 2, 2, 0,
                3, 1, 1, 2, 3, 1, 1, 2,
                3, 0, 2, 1, 1, 1, 2, 1,
                2, 2, 2, 2, 2, 1, 0, 1,
                1, 1, 1, 2, 1, 0, 1, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
            });
            Add("Q4K1k/1Q5p/2Q5/3Q4/4Q3/5Q2/6Q1/7Q w - - 0 1", ChessColor.White, new[]
            {
                1, 2, 3, 2, 4, 2, 3, 0,
                2, 2, 2, 3, 3, 4, 3, 3,
                3, 2, 2, 2, 3, 2, 3, 2,
                2, 3, 2, 2, 2, 3, 2, 3,
                3, 2, 3, 2, 2, 2, 3, 2,
                2, 3, 2, 3, 2, 2, 2, 3,
                3, 2, 3, 2, 3, 2, 2, 2,
                2, 3, 2, 3, 2, 3, 2, 1,
            });
            Add("Q4K1k/1Q5p/2Q5/3Q4/4Q3/5Q2/6Q1/7Q w - - 0 1", ChessColor.Black, new[]
            {
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
            });
        }
    }

    public class AttackersIncludeSquaresTestData : TheoryData<string, ChessSquare, ChessColor?, ChessSquare[]>
    {
        public AttackersIncludeSquaresTestData()
        {
            Add("2b5/4kp2/2r5/3q2n1/8/8/4P3/4K3 w - - 0 1", new ChessSquare("e6"), ChessColor.Black, new ChessSquare[]
            {
                new("c6"),
                new("c8"),
                new("d5"),
                new("e7"),
                new("f7"),
                new("g5"),
            });
            Add("4k3/8/8/8/5Q2/5p1R/4PK2/4N2B w - - 0 1", new ChessSquare("f3"), null, new ChessSquare[]
            {
                new("e1"),
                new("e2"),
                new("f2"),
                new("f4"),
                new("h1"),
                new("h3"),
            });
            Add("B3k3/8/8/2K4R/3QPN2/8/8/8 w - - 0 1", new ChessSquare("d5"), ChessColor.White, new ChessSquare[]
            {
                new("a8"),
                new("c5"),
                new("d4"),
                new("e4"),
                new("f4"),
                new("h5"),
            });
            Add("2r5/1b1p4/1kp1q3/4n3/8/8/8/4K3 b - - 0 1", new ChessSquare("c6"), null, new ChessSquare[]
            {
                new("b6"),
                new("b7"),
                new("c8"),
                new("d7"),
                new("e5"),
                new("e6"),
            });
            Add("r1bqkbnr/ppp2ppp/2np4/1B2p3/3PP3/5N2/PPP2PPP/RNBQK2R b KQkq - 0 4", new ChessSquare("d4"), ChessColor.Black, new ChessSquare[]
            {
                new("c6"),
                new("e5"),
            });
            Add("r1bqkbnr/ppp2ppp/2np4/1B2p3/3PP3/5N2/PPP2PPP/RNBQK2R b KQkq - 0 4", new ChessSquare("e5"), ChessColor.Black, new ChessSquare[]
            {
                new("c6"),
                new("d6"),
            });
            Add("3k4/8/8/8/3b4/3R4/4Pq2/4K3 w - - 0 1", new ChessSquare("f2"), ChessColor.White, new ChessSquare[]
            {
                new("e1")
            });
            Add("5k2/8/3N1N2/2NBQQN1/3R1R2/2NPRPN1/3N1N2/4K3 w - - 0 1", new ChessSquare("e4"), ChessColor.White, new ChessSquare[]
            {
                new("c3"),
                new("c5"),
                new("d2"),
                new("d3"),
                new("d4"),
                new("d5"),
                new("d6"),
                new("e3"),
                new("e5"),
                new("f2"),
                new("f3"),
                new("f4"),
                new("f5"),
                new("f6"),
                new("g3"),
                new("g5"),
            });
            Add(PublicData.DefaultChessPosition, new ChessSquare("e4"), ChessColor.White, new ChessSquare[] { });
        }
    }

    public class AttackersIncludeSquaresWithMovesTestData : TheoryData<string, string[], ChessSquare, ChessColor?, ChessSquare[]>
    {
        public AttackersIncludeSquaresWithMovesTestData()
        {
            Add(PublicData.DefaultChessPosition, new string[] { }, new ChessSquare("c3"), null, new ChessSquare[]
            {
                new("b1"),
                new("b2"),
                new("d2")
            });
            Add(PublicData.DefaultChessPosition, new string[] { }, new ChessSquare("c6"), null, new ChessSquare[] { });

            Add(PublicData.DefaultChessPosition, new string[] { "e4" }, new ChessSquare("c3"), null, new ChessSquare[] { });
            Add(PublicData.DefaultChessPosition, new string[] { "e4" }, new ChessSquare("c6"), null, new ChessSquare[]
            {
                new("b7"),
                new("b8"),
                new("d7")
            });

            Add(PublicData.DefaultChessPosition, new string[] { "e4", "e5" }, new ChessSquare("c3"), null, new ChessSquare[]
            {
                new("b1"),
                new("b2"),
                new("d2")
            });
            Add(PublicData.DefaultChessPosition, new string[] { "e4", "e5" }, new ChessSquare("c6"), null, new ChessSquare[] { });

            Add(PublicData.DefaultChessPosition, new string[] { }, new ChessSquare("f3"), null, new ChessSquare[]
            {
                new("e2"),
                new("g2"),
                new("g1")
            });
            Add(PublicData.DefaultChessPosition, new string[] { }, new ChessSquare("e2"), null, new ChessSquare[]
            {
                new("d1"),
                new("e1"),
                new("f1"),
                new("g1")
            });
            Add(PublicData.DefaultChessPosition, new string[] { }, new ChessSquare("f6"), null, new ChessSquare[] { });

            Add(PublicData.DefaultChessPosition, new string[] { "e4" }, new ChessSquare("f6"), null, new ChessSquare[]
            {
                new("g8"),
                new("e7"),
                new("g7")
            });
            Add(PublicData.DefaultChessPosition, new string[] { "e4" }, new ChessSquare("f3"), ChessColor.White, new ChessSquare[]
            {
                new("g2"),
                new("d1"),
                new("g1")
            });

            Add("4k3/4n3/8/8/8/8/4R3/4K3 w - - 0 1", new string[] { }, new ChessSquare("c6"), ChessColor.Black, new ChessSquare[]
            {
                new("e7"),
            });
        }
    }
}
