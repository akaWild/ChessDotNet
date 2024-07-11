namespace ChessDotNet.Public
{
    public record struct MoveInfo(string From, string To, string? Promotion = null);
}
