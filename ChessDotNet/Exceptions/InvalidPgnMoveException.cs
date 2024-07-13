namespace ChessDotNet.Exceptions
{
    public class InvalidPgnMoveException : Exception
    {
        public InvalidPgnMoveException(string message) : base(message) { }
    }
}
