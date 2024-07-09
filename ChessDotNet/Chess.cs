using ChessDotNet.Public;

namespace ChessDotNet
{
    public class Chess
    {
        private readonly Dictionary<string, string> _header = new Dictionary<string, string>();

        public void SetHeader(PngHeader pngHeader) => _header[pngHeader.Key] = pngHeader.Value;
        public PngHeader[] GetHeaders() => _header.Select(kv => new PngHeader(kv.Key, kv.Value)).ToArray();

        public static FenValidationResult ValidateFen(string fen)
        {
            return FenValidator.ValidateFen(fen);
        }
    }
}
