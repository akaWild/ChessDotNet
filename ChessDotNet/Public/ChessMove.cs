namespace ChessDotNet.Public
{
    public record ChessMove(ChessColor Color, ChessSquare From, ChessSquare To, ChessPieceType Piece, ChessPieceType? Captured, ChessPieceType? Promotion, string Flags, string San, string Lan, string Before, string After);
}
