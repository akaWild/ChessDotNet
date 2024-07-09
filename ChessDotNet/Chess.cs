using ChessDotNet.Public;

namespace ChessDotNet
{
    public class Chess
    {
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        public void SetHeader(PngHeader pngHeader) => _headers[pngHeader.Key] = pngHeader.Value;

        public PngHeader[] GetHeaders() => _headers.Select(kv => new PngHeader(kv.Key, kv.Value)).ToArray();

        public bool RemoveHeader(string key) => _headers.Remove(key);

        public static FenValidationResult ValidateFen(string fen)
        {
            return FenValidator.ValidateFen(fen);
        }
    }
}
