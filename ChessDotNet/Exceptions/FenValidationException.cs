namespace ChessDotNet.Exceptions
{
    public class FenValidationException : Exception
    {
        public FenValidationException(string message) : base(message) { }
    }
}
