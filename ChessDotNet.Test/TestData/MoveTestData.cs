using ChessDotNet.Public;
using ChessDotNet.Tests.TestUtils;

namespace ChessDotNet.Tests.TestData
{
    public class MoveStringFailedTestData : TheoryData<string, string, bool>
    {
        public MoveStringFailedTestData()
        {
            Add("r2qkbnr/ppp2ppp/2n5/1B2pQ2/4P3/8/PPP2PPP/RNB1K2R b KQkq - 3 7", "Nge7", true);
            Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "e5", false);
        }
    }

    public class MoveObjectFailedTestData : TheoryData<string, MoveInfo, bool>
    {
        public MoveObjectFailedTestData()
        {
            Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", new MoveInfo("e2", "e5"), false);
        }
    }

    public class MoveStringCorrectTestData : TheoryData<string, string, string, MoveResultTestObject>
    {
        public MoveStringCorrectTestData()
        {
            Add("rnbqkbnr/pp3ppp/2pp4/4pP2/4P3/8/PPPP2PP/RNBQKBNR w KQkq e6 0 1", "fxe6", "rnbqkbnr/pp3ppp/2ppP3/8/4P3/8/PPPP2PP/RNBQKBNR b KQkq - 0 1", new MoveResultTestObject("f5", "e6", null, 'p', "e"));
            Add("rnbqkbnr/pppp2pp/8/4p3/4Pp2/2PP4/PP3PPP/RNBQKBNR b KQkq e3 0 1", "fxe3", "rnbqkbnr/pppp2pp/8/4p3/8/2PPp3/PP3PPP/RNBQKBNR w KQkq - 0 2", new MoveResultTestObject("f4", "e3", null, 'p', "e"));
            Add("r2qkbnr/ppp2ppp/2n5/1B2pQ2/4P3/8/PPP2PPP/RNB1K2R b KQkq - 3 7", "Ne7", "r2qkb1r/ppp1nppp/2n5/1B2pQ2/4P3/8/PPP2PPP/RNB1K2R w KQkq - 4 8", new MoveResultTestObject("g8", "e7", null, null, "n"));
            Add("r2qkbnr/ppp2ppp/2n5/1B2pQ2/4P3/8/PPP2PPP/RNB1K2R b KQkq - 3 7", "Nge7", "r2qkb1r/ppp1nppp/2n5/1B2pQ2/4P3/8/PPP2PPP/RNB1K2R w KQkq - 4 8", new MoveResultTestObject("g8", "e7", 'n', null, null));
            Add("r2qkbnr/ppp2ppp/2n5/1B2pQ2/4P3/8/PPP2PPP/RNB1K2R b KQkq - 3 7", "Ne7", "r2qkb1r/ppp1nppp/2n5/1B2pQ2/4P3/8/PPP2PPP/RNB1K2R w KQkq - 4 8", new MoveResultTestObject("g8", "e7", 'n', null, null));
            Add("r1bqk2r/p1p2pp1/2n1pn2/1p5p/2pP4/bPNB1PN1/PB1Q2PP/R3K2R w KQkq - 0 12", "Ba3", "r1bqk2r/p1p2pp1/2n1pn2/1p5p/2pP4/BPNB1PN1/P2Q2PP/R3K2R b KQkq - 0 12", new MoveResultTestObject("b2", "a3", 'b', null, null));
            Add("rnbqkbnr/pppp1ppp/8/4p3/4PP2/8/PPPP2PP/RNBQKBNR b KQkq - 0 2", "ef4", "rnbqkbnr/pppp1ppp/8/8/4Pp2/8/PPPP2PP/RNBQKBNR w KQkq - 0 3", new MoveResultTestObject("e5", "f4", 'p', null, null));
            Add("rnbqkbnr/pppp1ppp/8/8/4PpP1/8/PPPP3P/RNBQKBNR b KQkq g3 0 3", "fg3", "rnbqkbnr/pppp1ppp/8/8/4P3/6p1/PPPP3P/RNBQKBNR w KQkq - 0 4", new MoveResultTestObject("f4", "g3", 'p', null, null));
        }
    }
}
