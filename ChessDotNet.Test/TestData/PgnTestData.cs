namespace ChessDotNet.Tests.TestData
{
    public class PgnTestData : TheoryData<string, string>
    {
        public PgnTestData()
        {
            Add("1. e4 {good move} e5 {classical response}", "1. e4 {good move} e5 {classical response}");
            Add("1. e4 e5; romantic era\r\n 2. Nf3 Nc6; common continuation", "1. e4 e5 {romantic era} 2. Nf3 Nc6 {common continuation}");
            Add("1. e4 {good!} e5; standard response\r\n 2. Nf3 Nc6 {common}", "1. e4 {good!} e5 {standard response} 2. Nf3 Nc6 {common}");
            Add("1. e4 {good\r\nmove} e5 {classical\r\nresponse}", "1. e4 {good move} e5 {classical response}");
            Add("{ great game }\r\n1. e4 e5", "{ great game } 1. e4 e5");
            Add("1. e4 {}", "1. e4 {}");
            Add("1. e4;\r\ne5", "1. e4 {} e5");
            Add("1. e4 {Δ, Й, ק ,م, ๗, あ, 叶, 葉, and 말}", "1. e4 {Δ, Й, ק ,م, ๗, あ, 叶, 葉, and 말}");
            Add("1. e4 { a classic; well-studied } e5", "1. e4 { a classic; well-studied } e5");
            Add("1. e4 e5 ; a classic {well-studied}", "1. e4 e5 {a classic {well-studied}}");
            Add("1. e4 e5 {($1) 1. e4 is good}", "1. e4 e5 {($1) 1. e4 is good}");
            Add("1. e4 e5; ($1) 1. e4 is good", "1. e4 e5 {($1) 1. e4 is good}");
            Add("\r\n[SetUp \"1\"]\r\n[FEN \"r4rk1/p1nq1pp1/1p1pp2p/8/P2PR3/1QP2N2/1P3PPP/R5K1 b - - 0 16\"]\r\n\r\n{test comment} 16...Rfb8", "[SetUp \"1\"]\r\n[FEN \"r4rk1/p1nq1pp1/1p1pp2p/8/P2PR3/1QP2N2/1P3PPP/R5K1 b - - 0 16\"]\r\n\r\n{test comment} 16. ... Rfb8");
        }
    }

    public class PgnFileTestData : TheoryData<string, string[], int, string?, string, string?, string>
    {
        public PgnFileTestData()
        {
            Add("d4 d5 Nf3 Nc6 e3 e6 Bb5 g5 O-O Qf6 Nc3 Bd7 Bxc6 Bxc6 Re1 O-O-O a4 Bb4 a5 b5 axb6 axb6 Ra8+ Kd7 Ne5+ Kd6 Rxd8+ Qxd8 Nxf7+ Ke7 Nxd5+ Qxd5 c3 Kxf7 Qf3+ Qxf3 gxf3 Bxf3 cxb4 e5 dxe5 Ke6 b3 Kxe5 Bb2+ Ke4 Bxh8 Nf6 Bxf6 h5 Bxg5 Bg2 Kxg2 Kf5 Bh4 Kg4 Bg3 Kf5 e4+ Kg4 e5 h4 Bxh4 Kxh4 e6 c5 bxc5 bxc5 e7 c4 bxc4 Kg4 e8=Q Kf5 Qe5+ Kg4 Re4#", new[]
            {
                "White",
                "Jeff Hlywa",
                "Black",
                "Steve Bragg",
                "GreatestGameEverPlayed?",
                "True"
            }, 19, "<br />", Path.Combine("TestData", "Pgn", "0.txt"), null, "8/8/8/4Q3/2P1R1k1/8/5PKP/8 b - - 4 39");

            Add("c4 e6 Nf3 d5 d4 Nf6 Nc3 Be7 Bg5 O-O e3 h6 Bh4 b6 cxd5 Nxd5 Bxe7 Qxe7 Nxd5 exd5 Rc1 Be6 Qa4 c5 Qa3 Rc8 Bb5 a6 dxc5 bxc5 O-O Ra7 Be2 Nd7 Nd4 Qf8 Nxe6 fxe6 e4 d4 f4 Qe7 e5 Rb8 Bc4 Kh8 Qh3 Nf8 b3 a5 f5 exf5 Rxf5 Nh7 Rcf1 Qd8 Qg3 Re7 h4 Rbb7 e6 Rbc7 Qe5 Qe8 a4 Qd8 R1f2 Qe8 R2f3 Qd8 Bd3 Qe8 Qe4 Nf6 Rxf6 gxf6 Rxf6 Kg8 Bc4 Kh8 Qf4", new[]
            {
                "Event",
                "Reykjavik WCh",
                "Site",
                "Reykjavik WCh",
                "Date",
                "1972.01.07",
                "EventDate",
                "?",
                "Round",
                "6",
                "Result",
                "1-0",
                "White",
                "Robert James Fischer",
                "Black",
                "Boris Spassky",
                "ECO",
                "D59",
                "WhiteElo",
                "?",
                "BlackElo",
                "?",
                "PlyCount",
                "81",
            }, 65, null, Path.Combine("TestData", "Pgn", "1.txt"), null, "4q2k/2r1r3/4PR1p/p1p5/P1Bp1Q1P/1P6/6P1/6K1 b - - 4 41");

            Add("f3 e5 g4 Qh4#", new string[] { }, 1, null, Path.Combine("TestData", "Pgn", "2.txt"), null, "rnb1kbnr/pppp1ppp/8/4p3/6Pq/5P2/PPPPP2P/RNBQKBNR w KQkq - 1 3");

            Add("Ba5 O-O d6 d4", new string[] { }, 20, null, Path.Combine("TestData", "Pgn", "3.txt"), "r1bqk1nr/pppp1ppp/2n5/4p3/1bB1P3/2P2N2/P2P1PPP/RNBQK2R b KQkq - 0 1", "r1bqk1nr/ppp2ppp/2np4/b3p3/2BPP3/2P2N2/P4PPP/RNBQ1RK1 b kq - 0 3");
        }
    }
}
