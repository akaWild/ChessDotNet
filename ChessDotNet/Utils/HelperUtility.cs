using ChessDotNet.Internal;
using ChessDotNet.Public;
using System.Text.RegularExpressions;

namespace ChessDotNet.Utils
{
    internal static class HelperUtility
    {
        public static int Rank(int square) => square >> 4;

        public static int File(int square) => square & 0xf;

        public static ChessColor SwapColor(ChessColor color) => color == ChessColor.Black ? ChessColor.White : ChessColor.Black;

        public static ChessSquare Algebraic(int square)
        {
            var file = File(square);
            var rank = Rank(square);

            return new ChessSquare("abcdefgh"[file], "87654321"[rank]);
        }

        public static string StripSan(string move) => Regex.Replace(move.Replace("=", ""), @"[+#]?[?!]*$", @"");

        public static string TrimFen(string fen) => string.Join(' ', fen.Split(' ').Take(4));

        public static string GetDisambiguator(InternalMove move, InternalMove[] moves)
        {
            var from = move.From;
            var to = move.To;
            var piece = move.Piece;

            var ambiguities = 0;
            var sameRank = 0;
            var sameFile = 0;

            for (int i = 0, len = moves.Length; i < len; i++)
            {
                var ambigFrom = moves[i].From;
                var ambigTo = moves[i].To;
                var ambigPiece = moves[i].Piece;

                if (piece == ambigPiece && from != ambigFrom && to == ambigTo)
                {
                    ambiguities++;

                    if (Rank(from) == Rank(ambigFrom))
                        sameRank++;

                    if (File(from) == File(ambigFrom))
                        sameFile++;
                }
            }

            if (ambiguities > 0)
            {
                if (sameRank > 0 && sameFile > 0)
                    return Algebraic(from).ToString();

                if (sameFile > 0)
                    return $"{Algebraic(from).Rank}";

                return $"{Algebraic(from).File}";
            }

            return "";
        }

        public static void AddMove(List<InternalMove> moves, ChessColor color, int from, int to, ChessPieceType piece, ChessPieceType? captured = null, Bits flags = Bits.Normal)
        {
            var rank = Rank(to);

            if (piece == ChessPieceType.Pawn && (rank is InternalData.Rank1 or InternalData.Rank8))
            {
                foreach (var promotion in InternalData.Promotions)
                    moves.Add(new InternalMove(color, from, to, piece, captured, promotion, flags | Bits.Promotion));
            }
            else
                moves.Add(new InternalMove(color, from, to, piece, captured, null, flags));
        }

        public static ChessPieceType? InferPieceType(string san)
        {
            var pieceType = san[0];
            if (char.IsBetween(pieceType, 'a', 'h'))
            {
                if (Regex.IsMatch(san, @"[a-h]\d.*[a-h]\d"))
                    return null;

                return ChessPieceType.Pawn;
            }

            pieceType = char.ToLower(pieceType);
            if (pieceType == 'o')
                return ChessPieceType.King;

            return (ChessPieceType)pieceType;
        }
    }
}
