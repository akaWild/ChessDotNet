namespace ChessDotNet.Exceptions
{
    public class InvalidPgnException : Exception
    {
        public InvalidPgnException(string message) : base(message) { }
    }
}
