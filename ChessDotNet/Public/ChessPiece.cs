namespace ChessDotNet.Public
{
    public record ChessPiece(ChessColor Color, ChessPieceType PieceType)
    {
        public ChessPieceType PieceType { get; set; } = PieceType;
    }
}
