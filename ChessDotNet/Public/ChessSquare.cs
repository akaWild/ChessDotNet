﻿using ChessDotNet.Exceptions;
using System.Text.RegularExpressions;

namespace ChessDotNet.Public
{
    public readonly record struct ChessSquare
    {
        public char File { get; init; }
        public int Rank { get; init; }

        public ChessSquare(char file, int rank)
        {
            if (!char.IsBetween(file, 'a', 'h'))
                throw new InvalidChessSquareException("Square file has wrong format");

            if (rank is < 1 or > 8)
                throw new InvalidChessSquareException("Square rank has wrong format");

            File = file;
            Rank = rank;
        }

        public ChessSquare(string square)
        {
            var match = Regex.Match(square, @"^(?<file>[a-h])(?<rank>[[0-8])$");
            if (!match.Success)
                throw new InvalidChessSquareException("Square has wrong format");

            File = match.Groups["file"].Value[0];
            Rank = int.Parse(match.Groups["rank"].Value);
        }

        public static implicit operator ChessSquare(string square) => new(square);

        public override string ToString() => $"{File}{Rank}";
    }
}
