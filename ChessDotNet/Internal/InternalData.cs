using ChessDotNet.Public;
using System.Collections.Frozen;

namespace ChessDotNet.Internal
{
    internal class InternalData
    {
        public const int Empty = -1;

        public const int Rank1 = 7;
        public const int Rank2 = 6;
        public const int Rank7 = 1;
        public const int Rank8 = 0;

        public static FrozenDictionary<ChessSquare, int> Ox88 = new Dictionary<ChessSquare, int>
        {
            {new ChessSquare("a8"), 0},
            {new ChessSquare("b8"), 1},
            {new ChessSquare("c8"), 2},
            {new ChessSquare("d8"), 3},
            {new ChessSquare("e8"), 4},
            {new ChessSquare("f8"), 5},
            {new ChessSquare("g8"), 6},
            {new ChessSquare("h8"), 7},

            {new ChessSquare("a7"), 16},
            {new ChessSquare("b7"), 17},
            {new ChessSquare("c7"), 18},
            {new ChessSquare("d7"), 19},
            {new ChessSquare("e7"), 20},
            {new ChessSquare("f7"), 21},
            {new ChessSquare("g7"), 22},
            {new ChessSquare("h7"), 23},

            {new ChessSquare("a6"), 32},
            {new ChessSquare("b6"), 33},
            {new ChessSquare("c6"), 34},
            {new ChessSquare("d6"), 35},
            {new ChessSquare("e6"), 36},
            {new ChessSquare("f6"), 37},
            {new ChessSquare("g6"), 38},
            {new ChessSquare("h6"), 39},

            {new ChessSquare("a5"), 48},
            {new ChessSquare("b5"), 49},
            {new ChessSquare("c5"), 50},
            {new ChessSquare("d5"), 51},
            {new ChessSquare("e5"), 52},
            {new ChessSquare("f5"), 53},
            {new ChessSquare("g5"), 54},
            {new ChessSquare("h5"), 55},

            {new ChessSquare("a4"), 64},
            {new ChessSquare("b4"), 65},
            {new ChessSquare("c4"), 66},
            {new ChessSquare("d4"), 67},
            {new ChessSquare("e4"), 68},
            {new ChessSquare("f4"), 69},
            {new ChessSquare("g4"), 70},
            {new ChessSquare("h4"), 71},

            {new ChessSquare("a3"), 80},
            {new ChessSquare("b3"), 81},
            {new ChessSquare("c3"), 82},
            {new ChessSquare("d3"), 83},
            {new ChessSquare("e3"), 84},
            {new ChessSquare("f3"), 85},
            {new ChessSquare("g3"), 86},
            {new ChessSquare("h3"), 87},

            {new ChessSquare("a2"), 96},
            {new ChessSquare("b2"), 97},
            {new ChessSquare("c2"), 98},
            {new ChessSquare("d2"), 99},
            {new ChessSquare("e2"), 100},
            {new ChessSquare("f2"), 101},
            {new ChessSquare("g2"), 102},
            {new ChessSquare("h2"), 103},

            {new ChessSquare("a1"), 112},
            {new ChessSquare("b1"), 113},
            {new ChessSquare("c1"), 114},
            {new ChessSquare("d1"), 115},
            {new ChessSquare("e1"), 116},
            {new ChessSquare("f1"), 117},
            {new ChessSquare("g1"), 118},
            {new ChessSquare("h1"), 119},

        }.ToFrozenDictionary();

        public static FrozenDictionary<ChessColor, int[]> PawnOffsets = new Dictionary<ChessColor, int[]>
        {
            { ChessColor.Black, new []{16, 32, 17, 15}},
            { ChessColor.White, new []{-16, -32, -17, -15}},
        }.ToFrozenDictionary();

        public static FrozenDictionary<ChessPieceType, int[]> PieceOffsets = new Dictionary<ChessPieceType, int[]>
        {
            { ChessPieceType.Knight, new []{-18, -33, -31, -14, 18, 33, 31, 14}},
            { ChessPieceType.Bishop, new []{-17, -15, 17, 15}},
            { ChessPieceType.Rook, new []{-16, 1, 16, -1}},
            { ChessPieceType.Queen, new []{-17, -16, -15, 1, 17, 16, 15, -1}},
            { ChessPieceType.King, new []{-17, -16, -15, 1, 17, 16, 15, -1}},
        }.ToFrozenDictionary();

        public static FrozenDictionary<ChessPieceType, int> PieceMasks = new Dictionary<ChessPieceType, int>
        {
            { ChessPieceType.Pawn, 0x1},
            { ChessPieceType.Knight, 0x2},
            { ChessPieceType.Bishop, 0x4},
            { ChessPieceType.Rook, 0x8},
            { ChessPieceType.Queen, 0x10},
            { ChessPieceType.King, 0x20},
        }.ToFrozenDictionary();

        public static FrozenDictionary<ChessPieceType, Bits> Sides = new Dictionary<ChessPieceType, Bits>
        {
            { ChessPieceType.King, Bits.KSideCastle},
            { ChessPieceType.Queen, Bits.QSideCastle},
        }.ToFrozenDictionary();

        public static FrozenDictionary<ChessColor, FrozenDictionary<int, int>> Rooks = new Dictionary<ChessColor, FrozenDictionary<int, int>>
        {
            { ChessColor.White, new Dictionary<int, int> {{Ox88[new ChessSquare("a1")], (int)Bits.QSideCastle}, { Ox88[new ChessSquare("h1")], (int)Bits.KSideCastle } }.ToFrozenDictionary()},
            { ChessColor.Black, new Dictionary<int, int> {{Ox88[new ChessSquare("a8")], (int)Bits.QSideCastle}, { Ox88[new ChessSquare("h8")], (int)Bits.KSideCastle } }.ToFrozenDictionary()},
        }.ToFrozenDictionary();

        public static FrozenDictionary<ChessColor, int> SecondRank = new Dictionary<ChessColor, int>
        {
            { ChessColor.Black, Rank7},
            { ChessColor.White, Rank2},
        }.ToFrozenDictionary();

        public static readonly int[] Attacks = {
            20, 0, 0, 0, 0, 0, 0, 24,  0, 0, 0, 0, 0, 0,20, 0,
            0,20, 0, 0, 0, 0, 0, 24,  0, 0, 0, 0, 0,20, 0, 0,
            0, 0,20, 0, 0, 0, 0, 24,  0, 0, 0, 0,20, 0, 0, 0,
            0, 0, 0,20, 0, 0, 0, 24,  0, 0, 0,20, 0, 0, 0, 0,
            0, 0, 0, 0,20, 0, 0, 24,  0, 0,20, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,20, 2, 24,  2,20, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 2,53, 56, 53, 2, 0, 0, 0, 0, 0, 0,
            24,24,24,24,24,24,56,  0, 56,24,24,24,24,24,24, 0,
            0, 0, 0, 0, 0, 2,53, 56, 53, 2, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,20, 2, 24,  2,20, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0,20, 0, 0, 24,  0, 0,20, 0, 0, 0, 0, 0,
            0, 0, 0,20, 0, 0, 0, 24,  0, 0, 0,20, 0, 0, 0, 0,
            0, 0,20, 0, 0, 0, 0, 24,  0, 0, 0, 0,20, 0, 0, 0,
            0,20, 0, 0, 0, 0, 0, 24,  0, 0, 0, 0, 0,20, 0, 0,
            20, 0, 0, 0, 0, 0, 0, 24,  0, 0, 0, 0, 0, 0,20
        };

        public static readonly int[] Rays = {
            17,  0,  0,  0,  0,  0,  0, 16,  0,  0,  0,  0,  0,  0, 15, 0,
            0, 17,  0,  0,  0,  0,  0, 16,  0,  0,  0,  0,  0, 15,  0, 0,
            0,  0, 17,  0,  0,  0,  0, 16,  0,  0,  0,  0, 15,  0,  0, 0,
            0,  0,  0, 17,  0,  0,  0, 16,  0,  0,  0, 15,  0,  0,  0, 0,
            0,  0,  0,  0, 17,  0,  0, 16,  0,  0, 15,  0,  0,  0,  0, 0,
            0,  0,  0,  0,  0, 17,  0, 16,  0, 15,  0,  0,  0,  0,  0, 0,
            0,  0,  0,  0,  0,  0, 17, 16, 15,  0,  0,  0,  0,  0,  0, 0,
            1,  1,  1,  1,  1,  1,  1,  0, -1, -1,  -1,-1, -1, -1, -1, 0,
            0,  0,  0,  0,  0,  0,-15,-16,-17,  0,  0,  0,  0,  0,  0, 0,
            0,  0,  0,  0,  0,-15,  0,-16,  0,-17,  0,  0,  0,  0,  0, 0,
            0,  0,  0,  0,-15,  0,  0,-16,  0,  0,-17,  0,  0,  0,  0, 0,
            0,  0,  0,-15,  0,  0,  0,-16,  0,  0,  0,-17,  0,  0,  0, 0,
            0,  0,-15,  0,  0,  0,  0,-16,  0,  0,  0,  0,-17,  0,  0, 0,
            0,-15,  0,  0,  0,  0,  0,-16,  0,  0,  0,  0,  0,-17,  0, 0,
            -15,  0,  0,  0,  0,  0,  0,-16,  0,  0,  0,  0,  0,  0,-17
        };

        public static readonly ChessPieceType[] Promotions = { ChessPieceType.Knight, ChessPieceType.Bishop, ChessPieceType.Rook, ChessPieceType.Queen };

        public static readonly string[] TerminationMarkers = { "1-0", "0-1", "1/2-1/2", "*" };
    }
}
