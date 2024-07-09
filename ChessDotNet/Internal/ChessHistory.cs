using ChessDotNet.Public;

namespace ChessDotNet.Internal
{
    internal record ChessHistory(InternalMove Move, Dictionary<ChessColor, int> Kings, ChessColor Turn, Dictionary<ChessColor, int> Castling, int EpSquare, int HalfMoves, int MoveNumber);
}
