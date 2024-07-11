namespace ChessDotNet.Public
{
    public record ChessMove(ChessColor Color, ChessSquare From, ChessSquare To, ChessPieceType Piece, ChessPieceType? Captured, ChessPieceType? Promotion, string Flags, string San, string Lan, string Before, string After)
    {
        public string After { get; set; } = After;
        public ChessPieceType? Captured { get; set; } = Captured;
        public ChessPieceType? Promotion { get; set; } = Promotion;
        public string Lan { get; set; } = Lan;
    }
}
