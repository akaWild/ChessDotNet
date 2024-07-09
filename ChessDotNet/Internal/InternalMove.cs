using ChessDotNet.Public;

namespace ChessDotNet.Internal
{
    internal record InternalMove(ChessColor Color, int From, int To, ChessPieceType Piece, ChessPieceType? Captured, ChessPieceType? Promotion, Bits Flags);
}
