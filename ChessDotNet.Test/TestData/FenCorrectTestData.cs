namespace ChessDotNet.Tests.TestData
{
    public class FenCorrectTestData : TheoryData<string>
    {
        public FenCorrectTestData()
        {
            Add("k7/8/8/8/8/8/8/7K w - - 0 1");
            Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            Add("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq - 0 1");
            Add("1nbqkbn1/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/1NBQKBN1 b - - 1 2");
        }
    }
}
