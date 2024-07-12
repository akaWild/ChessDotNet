using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class IsThreefoldRepetitionTestData : TheoryData<string, string[], bool>
    {
        public IsThreefoldRepetitionTestData()
        {
            Add("8/pp3p1k/2p2q1p/3r1P2/5R2/7P/P1P1QP2/7K b - - 2 30", new string[] { }, false);
            Add("8/pp3p1k/2p2q1p/3r1P2/5R2/7P/P1P1QP2/7K b - - 2 30", new string[]
            {
                "Qe5"
            }, false);
            Add("8/pp3p1k/2p2q1p/3r1P2/5R2/7P/P1P1QP2/7K b - - 2 30", new string[]
            {
                "Qe5",
                "Qh5"
            }, false);
            Add("8/pp3p1k/2p2q1p/3r1P2/5R2/7P/P1P1QP2/7K b - - 2 30", new string[]
            {
                "Qe5",
                "Qh5",
                "Qf6"
            }, false);
            Add("8/pp3p1k/2p2q1p/3r1P2/5R2/7P/P1P1QP2/7K b - - 2 30", new string[]
            {
                "Qe5",
                "Qh5",
                "Qf6",
                "Qe2"
            }, false);
            Add("8/pp3p1k/2p2q1p/3r1P2/5R2/7P/P1P1QP2/7K b - - 2 30", new string[]
            {
                "Qe5",
                "Qh5",
                "Qf6",
                "Qe2",
                "Re5"
            }, false);
            Add("8/pp3p1k/2p2q1p/3r1P2/5R2/7P/P1P1QP2/7K b - - 2 30", new string[]
            {
                "Qe5",
                "Qh5",
                "Qf6",
                "Qe2",
                "Re5",
                "Qd3"
            }, false);
            Add("8/pp3p1k/2p2q1p/3r1P2/5R2/7P/P1P1QP2/7K b - - 2 30", new string[]
            {
                "Qe5",
                "Qh5",
                "Qf6",
                "Qe2",
                "Re5",
                "Qd3",
                "Rd5"
            }, false);
            Add("8/pp3p1k/2p2q1p/3r1P2/5R2/7P/P1P1QP2/7K b - - 2 30", new string[]
            {
                "Qe5",
                "Qh5",
                "Qf6",
                "Qe2",
                "Re5",
                "Qd3",
                "Rd5",
                "Qe2"
            }, true);
            Add("8/pp3p1k/2p2q1p/3r1P2/5R2/7P/P1P1QP2/7K b - - 2 30", new string[]
            {
                "Qe5",
                "Qh5",
                "Qf6",
                "Qe2",
                "Re5",
                "Qd3",
                "Rd5",
                "Qe2",
                "a6"
            }, false);

            Add(PublicData.DefaultChessPosition, new string[] { }, false);
            Add(PublicData.DefaultChessPosition, new string[]
            {
                "Nf3"
            }, false);
            Add(PublicData.DefaultChessPosition, new string[]
            {
                "Nf3",
                "Nf6"
            }, false);
            Add(PublicData.DefaultChessPosition, new string[]
            {
                "Nf3",
                "Nf6",
                "Ng1"
            }, false);
            Add(PublicData.DefaultChessPosition, new string[]
            {
                "Nf3",
                "Nf6",
                "Ng1",
                "Ng8"
            }, false);
            Add(PublicData.DefaultChessPosition, new string[]
            {
                "Nf3",
                "Nf6",
                "Ng1",
                "Ng8",
                "Nf3"
            }, false);
            Add(PublicData.DefaultChessPosition, new string[]
            {
                "Nf3",
                "Nf6",
                "Ng1",
                "Ng8",
                "Nf3",
                "Nf6"
            }, false);
            Add(PublicData.DefaultChessPosition, new string[]
            {
                "Nf3",
                "Nf6",
                "Ng1",
                "Ng8",
                "Nf3",
                "Nf6",
                "Ng1"
            }, false);
            Add(PublicData.DefaultChessPosition, new string[]
            {
                "Nf3",
                "Nf6",
                "Ng1",
                "Ng8",
                "Nf3",
                "Nf6",
                "Ng1",
                "Ng8"
            }, true);
            Add(PublicData.DefaultChessPosition, new string[]
            {
                "Nf3",
                "Nf6",
                "Ng1",
                "Ng8",
                "Nf3",
                "Nf6",
                "Ng1",
                "Ng8",
                "e4"
            }, false);
        }
    }
}
