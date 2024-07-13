namespace ChessDotNet.Exceptions
{
    public class InvalidChessSquareException : Exception
    {
        public InvalidChessSquareException(string message) : base(message) { }
    }
}
