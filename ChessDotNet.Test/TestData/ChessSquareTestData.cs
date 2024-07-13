namespace ChessDotNet.Tests.TestData
{
    public class ChessSquareV1TestData : TheoryData<char, int, bool>
    {
        public ChessSquareV1TestData()
        {
            Add('a', 2, true);
            Add('b', 6, true);
            Add('h', 8, true);
            Add('e', 3, true);
            Add('x', 2, false);
            Add('\0', 8, false);
            Add('b', 0, false);
            Add('x', 11, false);
        }
    }

    public class ChessSquareV2TestData : TheoryData<string, bool>
    {
        public ChessSquareV2TestData()
        {
            Add("a4", true);
            Add("g2", true);
            Add("h8", true);
            Add("d6", true);
            Add("x8", false);
            Add("c12", false);
            Add("", false);
            Add("xx", false);
        }
    }

    public class ChessSquareImplicitConversionTestData : TheoryData<string, bool>
    {
        public ChessSquareImplicitConversionTestData()
        {
            Add("a4", true);
            Add("g2", true);
            Add("h8", true);
            Add("d6", true);
            Add("x8", false);
            Add("c12", false);
            Add("", false);
            Add("xx", false);
        }
    }
}
