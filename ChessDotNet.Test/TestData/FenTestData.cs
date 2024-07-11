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

    public class FenWithMoveCorrectTestData : TheoryData<string, string, string>
    {
        public FenWithMoveCorrectTestData()
        {
            Add("4k3/8/8/8/5p2/8/4P3/4K3 w - - 0 1", "e4", "4k3/8/8/8/4Pp2/8/8/4K3 b - e3 0 1");
            Add("5k2/8/8/8/5p2/8/4P3/4KR2 w - - 0 1", "e4", "5k2/8/8/8/4Pp2/8/8/4KR2 b - - 0 1");
            Add("rnb1kbn1/p1p1pp2/PpPp2qr/5Pp1/8/R1P4p/1PK1P1PP/1NBQ1BNR b - - 0 1", "e5", "rnb1kbn1/p1p2p2/PpPp2qr/4pPp1/8/R1P4p/1PK1P1PP/1NBQ1BNR w - - 0 2");
            Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "e4", "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq - 0 1");
            Add("7k/3R4/3p2Q1/6Q1/2N1N3/8/8/3R3K w - - 0 1", "Rd8#", "3R3k/8/3p2Q1/6Q1/2N1N3/8/8/3R3K b - - 1 1");
        }
    }
}
