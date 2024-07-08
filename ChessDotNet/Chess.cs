namespace ChessDotNet
{
    public class Chess
    {
        public static FenValidationResult ValidateFen(string fen)
        {
            return FenValidator.ValidateFen(fen);
        }
    }
}
