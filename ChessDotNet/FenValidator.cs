using System.Text.RegularExpressions;

namespace ChessDotNet
{
    internal static class FenValidator
    {
        public static FenValidationResult ValidateFen(string fen)
        {
            var tokens = fen.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length != 6)
                return new FenValidationResult(false, "Must contain six space-delimited fields");

            if (!int.TryParse(tokens[5], out var moveNumber) || moveNumber <= 0)
                return new FenValidationResult(false, "Move number must be a positive integer");

            if (!int.TryParse(tokens[4], out var halfMoves) || halfMoves < 0)
                return new FenValidationResult(false, "Half move counter number must be a non-negative integer");

            if (!Regex.IsMatch(tokens[3], "^(-|[abcdefgh][36])$"))
                return new FenValidationResult(false, "En-passant square is invalid");

            if (!Regex.IsMatch(tokens[2], "^(-|K?Q?k?q?)$"))
                return new FenValidationResult(false, "Castling availability is invalid");

            if (!Regex.IsMatch(tokens[1], "^(w|b)$"))
                return new FenValidationResult(false, "Side-to-move is invalid");

            var rows = tokens[0].Split("/");
            if (rows.Length != 8)
                return new FenValidationResult(false, "Piece data does not contain 8 '/'-delimited rows");

            foreach (var row in rows)
            {
                int sumFields = 0;
                bool previousWasNumber = false;

                for (int j = 0; j < row.Length; j++)
                {
                    if (char.IsDigit(row[j]))
                    {
                        if (previousWasNumber)
                            return new FenValidationResult(false, "Piece data is invalid (consecutive number)");

                        sumFields += int.Parse($"{row[j]}");
                        previousWasNumber = true;
                    }
                    else
                    {
                        if (!Regex.IsMatch($"{row[j]}", "^[prnbqkPRNBQK]$"))
                            return new FenValidationResult(false, "Piece data is invalid (invalid piece)");

                        sumFields++;
                        previousWasNumber = false;
                    }
                }

                if (sumFields != 8)
                    return new FenValidationResult(false, "Piece data is invalid (too many squares in rank)");
            }

            if (tokens[3][0] != '-')
            {
                if ((tokens[3][1] == '3' && tokens[1] == "w") || (tokens[3][1] == '6' && tokens[1] == "b"))
                    return new FenValidationResult(false, "Illegal en-passant square");
            }

            var whiteKingsCount = tokens[0].Count(c => c == 'K');
            if (whiteKingsCount == 0)
                return new FenValidationResult(false, "Missing white king");

            if (whiteKingsCount > 1)
                return new FenValidationResult(false, "Too many white kings");

            var blackKingsCount = tokens[0].Count(c => c == 'k');
            if (blackKingsCount == 0)
                return new FenValidationResult(false, "Missing black king");

            if (blackKingsCount > 1)
                return new FenValidationResult(false, "Too many black kings");

            if (rows[0].Concat(rows[7]).Any(c => c is 'p' or 'P'))
                return new FenValidationResult(false, "Some pawns are on the edge rows");

            return new FenValidationResult(true);
        }
    }
}
