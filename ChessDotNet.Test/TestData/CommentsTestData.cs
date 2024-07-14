using ChessDotNet.Public;

namespace ChessDotNet.Tests.TestData
{
    public class CommentsPgnTestData : TheoryData<string, CommentInfo[]>
    {
        public CommentsPgnTestData()
        {
            Add("[Event \"Paris\"]\r\n[Site \"Paris\"]\r\n[Date \"1858.??.??\"]\r\n[EventDate \"?\"]\r\n[Round \"?\"]\r\n[Result \"1-0\"]\r\n[White \"Paul Morphy\"]\r\n[Black \"Duke Karl / Count Isouard\"]\r\n[ECO \"C41\"]\r\n[WhiteElo \"?\"]\r\n[BlackElo \"?\"]\r\n[PlyCount \"33\"]\r\n\r\n1.e4 e5 2.Nf3 d6 3.d4 Bg4 {This is a weak move already.--Fischer} 4.dxe5 Bxf3\r\n5.Qxf3 dxe5 6.Bc4 Nf6 7.Qb3 Qe7 8.Nc3 c6 9.Bg5 {Black is in what's like a\r\nzugzwang position, here. He can't develop the [Queen's] knight because the\r\npawn, is hanging, the bishop is blocked because of the Queen.--Fischer} b5\r\n10.Nxb5 cxb5 11.Bxb5+ Nbd7 12.O-O-O Rd8 13.Rxd7 Rxd7 14.Rd1 Qe6 15.Bxd7+ Nxd7\r\n16.Qb8+ Nxb8 17.Rd8# 1-0", new CommentInfo[]
            {
                new CommentInfo("rn1qkbnr/ppp2ppp/3p4/4p3/3PP1b1/5N2/PPP2PPP/RNBQKB1R w KQkq - 1 4", "This is a weak move already.--Fischer"),
                new CommentInfo("rn2kb1r/pp2qppp/2p2n2/4p1B1/2B1P3/1QN5/PPP2PPP/R3K2R b KQkq - 1 9", "Black is in what's like a zugzwang position, here. He can't develop the [Queen's] knight because the pawn, is hanging, the bishop is blocked because of the Queen.--Fischer")
            });

            Add("[Event \"2012 ROCHESTER GRAND WINTER OPEN\"]\r\n[Site \"Rochester\"]\r\n[Date \"2012.02.04\"]\r\n[Round \"1\"]\r\n[White \"Jensen, Matthew\"]\r\n[Black \"Gaustad, Kevin\"]\r\n[Result \"1-0\"]\r\n[ECO \"E01\"]\r\n[WhiteElo \"2131\"]\r\n[BlackElo \"1770\"]\r\n[Annotator \"Jensen, Matthew\"]\r\n\r\n{ Kevin and I go way back.  I checked the USCF player stats and my previous\r\nrecord against Kevin was 4 losses and 1 draw out of 5 games.  All of our\r\nprevious games were between 1992-1998. }\r\n1.d4 Nf6 2.c4 e6 3.g3 { Avrukh says\r\nto play 3.g3 instead of 3.Nf3 in case the Knight later comes to e2, as in the\r\nBogo-Indian. } 3...d5 4.Bg2 c6 5.Nf3 Be7 6.O-O Nbd7\r\n1-0", new CommentInfo[]
            {
                new CommentInfo("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", " Kevin and I go way back.  I checked the USCF player stats and my previous record against Kevin was 4 losses and 1 draw out of 5 games.  All of our previous games were between 1992-1998. "),
                new CommentInfo("rnbqkb1r/pppp1ppp/4pn2/8/2PP4/6P1/PP2PP1P/RNBQKBNR b KQkq - 0 3", " Avrukh says to play 3.g3 instead of 3.Nf3 in case the Knight later comes to e2, as in the Bogo-Indian. ")
            });

            Add("1. e4 ( 1. d4 { Queen's pawn } d5 ( 1... Nf6 ) ) e5", new CommentInfo[] { });

            Add("1. e4 c5 2. Nf3 e6 { Sicilian Defence, French Variation } 3. Nc3 a6\r\n4. Be2 Nc6 5. d4 cxd4 6. Nxd4 Qc7 7. O-O Nf6 8. Be3 Be7 9. f4 d6\r\n10. Kh1 O-O 11. Qe1 Nxd4 12. Bxd4 b5 13. Qg3 Bb7 14. a3 Rad8\r\n15. Rae1 Rd7 16. Bd3 Qd8 17. Qh3 g6? { (0.05 → 1.03) Inaccuracy.\r\nThe best move was h6. } (17... h6 18. Rd1 Re8 19. Qg3 Nh5 20. Qg4\r\nNf6 21. Qh3 Bc6 22. Kg1 Qb8 23. Qg3 Nh5 24. Qf2 Bf6 25. Be2 Bxd4\r\n26. Rxd4 Nf6 27. g3) 18. f5 e5", new CommentInfo[]
            {
                new CommentInfo("rnbqkbnr/pp1p1ppp/4p3/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R w KQkq - 0 3", " Sicilian Defence, French Variation "),
                new CommentInfo("3q1rk1/1b1rbp1p/p2ppnp1/1p6/3BPP2/P1NB3Q/1PP3PP/4RR1K w - - 0 18", " (0.05 → 1.03) Inaccuracy. The best move was h6. ")
            });

            Add("\r\n1. d4 d5 2. Bf4 Nf6 3. e3 g6 4. Nf3 (4. Nc3 Bg7 5. Nf3 O-O 6. Be2 c5)\r\n4... Bg7 5. h3 { 5. Be2 O-O 6. O-O c5 7. c3 Nc6 } 5... O-O", new CommentInfo[]
            {
                new CommentInfo("rnbqk2r/ppp1ppbp/5np1/3p4/3P1B2/4PN1P/PPP2PP1/RN1QKB1R b KQkq - 0 5", " 5. Be2 O-O 6. O-O c5 7. c3 Nc6 "),
            });
        }
    }
}
