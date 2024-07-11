using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class CastlingRightsTestData : TheoryData<string, bool, ChessColor, CastlingRights, bool, bool>
    {
        public CastlingRightsTestData()
        {
            Add(PublicData.DefaultChessPosition, false, ChessColor.White, new CastlingRights(false, null), true, false);
            Add(PublicData.DefaultChessPosition, false, ChessColor.White, new CastlingRights(null, false), true, false);
            Add(PublicData.DefaultChessPosition, false, ChessColor.Black, new CastlingRights(false, null), true, false);
            Add(PublicData.DefaultChessPosition, false, ChessColor.Black, new CastlingRights(null, false), true, false);
            Add("r3k2r/8/8/8/8/8/8/R3K2R w - - 0 1", false, ChessColor.White, new CastlingRights(true, null), true, true);
            Add("r3k2r/8/8/8/8/8/8/R3K2R w - - 0 1", false, ChessColor.White, new CastlingRights(null, true), true, true);
            Add("r3k2r/8/8/8/8/8/8/R3K2R w - - 0 1", false, ChessColor.Black, new CastlingRights(true, null), true, true);
            Add("r3k2r/8/8/8/8/8/8/R3K2R w - - 0 1", false, ChessColor.Black, new CastlingRights(null, true), true, true);
            Add(PublicData.DefaultChessPosition, true, ChessColor.White, new CastlingRights(true, null), false, false);
            Add(PublicData.DefaultChessPosition, true, ChessColor.White, new CastlingRights(null, true), false, false);
            Add(PublicData.DefaultChessPosition, true, ChessColor.Black, new CastlingRights(true, null), false, false);
            Add(PublicData.DefaultChessPosition, true, ChessColor.Black, new CastlingRights(null, true), false, false);
        }
    }
}
