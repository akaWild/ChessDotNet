using ChessDotNet.Public;

namespace ChessDotNet
{
    public class Chess
    {
        private ChessColor _turn = ChessColor.White;
        private int _epSquare = -1;
        private int _halfMoves = 0;
        private int _moveNumber = 0;

        private ChessPiece[] _board = new ChessPiece[128];

        private readonly List<ChessHistory> _history = new();

        private Dictionary<ChessColor, int> _kings = new()
        {
            { ChessColor.White, InternalData.Empty},
            { ChessColor.Black, InternalData.Empty},
        };
        private Dictionary<ChessColor, int> _castling = new()
        {
            { ChessColor.White, 0},
            { ChessColor.Black, 0},
        };
        private readonly Dictionary<string, string> _headers = new();
        private readonly Dictionary<string, string> _comments = new();
        private readonly Dictionary<string, int> _positionCount = new();

        public void SetHeader(PngHeader pngHeader) => _headers[pngHeader.Key] = pngHeader.Value;

        public PngHeader[] GetHeaders() => _headers.Select(kv => new PngHeader(kv.Key, kv.Value)).ToArray();

        public bool RemoveHeader(string key) => _headers.Remove(key);

        public static FenValidationResult ValidateFen(string fen)
        {
            return FenValidator.ValidateFen(fen);
        }
    }
}
