using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class IsAttackedTestData : TheoryData<string, ChessColor, bool, ChessSquare[]>
    {
        public IsAttackedTestData()
        {
            Add("4k3/4p3/8/8/8/8/4P3/4K3 w - - 0 1", ChessColor.White, true, new ChessSquare[]
            {
                new("d3"),
                new ("f3")
            });
            Add("4k3/4p3/8/8/8/8/4P3/4K3 w - - 0 1", ChessColor.Black, false, new ChessSquare[]
            {
                new("d3"),
                new ("f3")
            });
            Add("4k3/4p3/8/8/8/8/4P3/4K3 w - - 0 1", ChessColor.White, false, new ChessSquare[]
            {
                new("e4"),
                new ("e4")
            });

            Add("4k3/4p3/8/8/8/8/4P3/4K3 w - - 0 1", ChessColor.Black, true, new ChessSquare[]
            {
                new("f6"),
                new ("d6")
            });
            Add("4k3/4p3/8/8/8/8/4P3/4K3 w - - 0 1", ChessColor.White, false, new ChessSquare[]
            {
                new("f6"),
                new ("d6")
            });
            Add("4k3/4p3/8/8/8/8/4P3/4K3 w - - 0 1", ChessColor.Black, false, new ChessSquare[]
            {
                new("e6"),
                new ("e5")
            });

            Add("4k3/4p3/8/8/4N3/8/8/4K3 w - - 0 1", ChessColor.White, true, new ChessSquare[]
            {
                new("d2"),
                new ("f2"),
                new ("c3"),
                new ("g3"),
                new ("d6"),
                new ("f6"),
                new ("c5"),
                new ("g5"),
            });
            Add("4k3/4p3/8/8/4N3/8/8/4K3 w - - 0 1", ChessColor.White, false, new ChessSquare[]
            {
                new("e4"),
            });

            Add("4k3/4p3/8/8/4b3/8/8/4K3 w - - 0 1", ChessColor.Black, true, new ChessSquare[]
            {
                new("b1"),
                new ("c2"),
                new ("d3"),
                new ("f5"),
                new ("g6"),
                new ("h7"),
                new ("a8"),
                new ("b7"),
                new ("c6"),
                new ("d5"),
                new ("f3"),
                new ("g2"),
                new ("h1"),
            });
            Add("4k3/4p3/8/8/4b3/8/8/4K3 w - - 0 1", ChessColor.Black, false, new ChessSquare[]
            {
                new("e4"),
            });

            Add("4k3/4n3/8/8/8/4R3/8/4K3 w - - 0 1", ChessColor.White, true, new ChessSquare[]
            {
                new("e1"),
                new ("e2"),
                new ("e4"),
                new ("e5"),
                new ("e6"),
                new ("e7"),
                new ("a3"),
                new ("b3"),
                new ("c3"),
                new ("d3"),
                new ("f3"),
                new ("g3"),
                new ("h3"),
            });
            Add("4k3/4n3/8/8/8/4R3/8/4K3 w - - 0 1", ChessColor.White, false, new ChessSquare[]
            {
                new("e3"),
            });

            Add("4k3/4n3/8/8/8/4q3/4P3/4K3 w - - 0 1", ChessColor.Black, true, new ChessSquare[]
            {
                new("e2"),
                new ("e4"),
                new ("e5"),
                new ("e6"),
                new ("e7"),
                new ("a3"),
                new ("b3"),
                new ("c3"),
                new ("d3"),
                new ("f3"),
                new ("g3"),
                new ("h3"),
                new ("c1"),
                new ("d2"),
                new ("f4"),
                new ("g5"),
                new ("h6"),
                new ("g1"),
                new ("f2"),
                new ("d4"),
                new ("c5"),
                new ("b6"),
                new ("a7"),
            });
            Add("4k3/4n3/8/8/8/4q3/4P3/4K3 w - - 0 1", ChessColor.Black, false, new ChessSquare[]
            {
                new("e3"),
            });

            Add("4k3/4n3/8/8/8/4q3/4P3/4K3 w - - 0 1", ChessColor.White, true, new ChessSquare[]
            {
                new("e2"),
                new ("d1"),
                new ("d2"),
                new ("f1"),
                new ("f2"),
            });
            Add("4k3/4n3/8/8/8/4q3/4P3/4K3 w - - 0 1", ChessColor.White, false, new ChessSquare[]
            {
                new("e1"),
            });

            Add("4k3/4r3/8/8/8/8/4P3/4K3 w - - 0 1", ChessColor.White, true, new ChessSquare[]
            {
                new("d3"),
                new ("f3"),
            });

            Add("4k3/4n3/8/8/8/4q3/4P3/4K3 w - - 0 1", ChessColor.Black, false, new ChessSquare[]
            {
                new("e1"),
            });

            Add(PublicData.DefaultChessPosition, ChessColor.White, true, new ChessSquare[]
            {
                new("f3"),
            });
            Add(PublicData.DefaultChessPosition, ChessColor.Black, true, new ChessSquare[]
            {
                new("f6"),
            });
            Add(PublicData.DefaultChessPosition, ChessColor.White, true, new ChessSquare[]
            {
                new("e2"),
            });
            Add("4k3/4n3/8/8/8/8/4R3/4K3 w - - 0 1", ChessColor.Black, true, new ChessSquare[]
            {
                new("c6"),
            });
        }
    }
}
