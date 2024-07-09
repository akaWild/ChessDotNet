namespace ChessDotNet.Internal
{
    [Flags]
    internal enum Bits
    {
        Normal = 1,
        Capture = 2,
        BigPawn = 4,
        EpCapture = 8,
        Promotion = 16,
        KSideCastle = 32,
        QSideCastle = 64
    }
}