using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class LoadFenFailedTestData : TheoryData<string>
    {
        public LoadFenFailedTestData()
        {
            Add("");
            Add("nbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            Add("rnbqkbnr/pppppppp/9/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            Add("1nbqkbn1/pppp1ppX/8/4p3/4P3/8/PPPP1PPP/1NBQKBN1 b - - 1 2");
            Add("1nbqkbn1/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/1NBQ1BN1 b - - 1 2");
            Add("1nbq1bn1/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/1NBQKBN1 b - - 1 2");
            Add("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e9 0 1");
        }
    }
    public class LoadFenCorrectTestData : TheoryData<string>
    {
        public LoadFenCorrectTestData()
        {
            Add(PublicData.DefaultChessPosition);
            Add("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b");
            Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            Add("rnbqkbnr/pppp1ppp/8/4p3/2P5/8/PP1PPPPP/RNBQKBNR w KQkq e6 0 2");
            Add("3r2k1/p1q2pp1/2nr1n1p/2p1p3/4P2B/P1P2Q1P/B4PP1/1R2R1K1 b - - 3 20");
        }
    }
}
