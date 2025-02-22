﻿using ChessDotNet.Exceptions;
using ChessDotNet.Internal;
using ChessDotNet.Public;
using ChessDotNet.Utils;
using System.Text.RegularExpressions;

namespace ChessDotNet
{
    public class Chess
    {
        private ChessColor _turn = ChessColor.White;
        private int _epSquare = -1;
        private int _halfMoves = 0;
        private int _moveNumber = 0;

        private ChessPiece?[] _board = new ChessPiece?[128];

        private readonly List<PgnHeader> _headers = new();

        private readonly Stack<ChessHistory> _history = new();

        private Dictionary<ChessColor, int> _kings = new()
        {
            { ChessColor.White, InternalData.Empty},
            { ChessColor.Black, InternalData.Empty},
        };
        private Dictionary<ChessColor, int> _castling = new()
        {
            { ChessColor.White, 0},
            { ChessColor.Black, 0},
        };
        private Dictionary<string, string> _comments = new();
        private readonly Dictionary<string, int> _positionCount = new();

        public Chess(string fen = PublicData.DefaultChessPosition)
        {
            LoadFen(fen);
        }

        public void LoadFen(string fen, bool skipValidation = false, bool preserveHeaders = false)
        {
            var tokens = fen.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length is >= 2 and < 6)
            {
                var adjustments = new[] { "-", "-", "0", "1" };

                fen = string.Join(" ", tokens.Concat(adjustments[^(6 - tokens.Length)..]));
            }

            tokens = fen.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (!skipValidation)
            {
                var fenValidationResult = FenValidator.ValidateFen(fen);
                if (!fenValidationResult.Ok)
                    throw new FenValidationException(fenValidationResult.Error!);
            }

            var position = tokens[0];
            var square = 0;

            ClearBoard(preserveHeaders);

            foreach (var piece in position)
            {
                if (piece == '/')
                    square += 8;

                else if (char.IsDigit(piece))
                    square += int.Parse($"{piece}");
                else
                {
                    var color = piece < 'a' ? ChessColor.White : ChessColor.Black;

                    Put((ChessPieceType)char.ToLower(piece), color, HelperUtility.Algebraic(square));

                    square++;
                }
            }

            _turn = (ChessColor)tokens[1][0];

            if (tokens[2].IndexOf('K') > -1)
                _castling[ChessColor.White] |= (int)Bits.KSideCastle;

            if (tokens[2].IndexOf('Q') > -1)
                _castling[ChessColor.White] |= (int)Bits.QSideCastle;

            if (tokens[2].IndexOf('k') > -1)
                _castling[ChessColor.Black] |= (int)Bits.KSideCastle;

            if (tokens[2].IndexOf('q') > -1)
                _castling[ChessColor.Black] |= (int)Bits.QSideCastle;

            _epSquare = tokens[3] == "-" ? InternalData.Empty : InternalData.Ox88[new ChessSquare(tokens[3])];
            _halfMoves = int.Parse(tokens[4]);
            _moveNumber = int.Parse(tokens[5]);

            UpdateSetup(fen);
            IncPositionCount(fen);
        }

        public void ClearBoard(bool preserveHeaders = false)
        {
            _turn = ChessColor.White;
            _epSquare = InternalData.Empty;
            _halfMoves = 0;
            _moveNumber = 1;

            _board = new ChessPiece?[128];

            _history.Clear();

            _kings = new()
            {
                { ChessColor.White, InternalData.Empty},
                { ChessColor.Black, InternalData.Empty},
            };
            _castling = new()
            {
                { ChessColor.White, 0},
                { ChessColor.Black, 0},
            };
            _comments.Clear();
            _positionCount.Clear();

            if (!preserveHeaders)
                _headers.Clear();

            _headers.RemoveAll(h => h.Key == "SetUp");
            _headers.RemoveAll(h => h.Key == "FEN");
        }

        public string GetFen()
        {
            var empty = 0;
            var fen = string.Empty;

            for (var i = InternalData.Ox88[new ChessSquare("a8")]; i <= InternalData.Ox88[new ChessSquare("h1")]; i++)
            {
                var piece = _board[i];

                if (piece != null)
                {
                    if (empty > 0)
                    {
                        fen += empty;
                        empty = 0;
                    }

                    fen += piece.Color == ChessColor.White ? char.ToUpper((char)piece.PieceType) : char.ToLower((char)piece.PieceType);
                }
                else
                    empty++;

                if (((i + 1) & 0x88) != 0)
                {
                    if (empty > 0)
                        fen += empty;

                    if (i != InternalData.Ox88[new ChessSquare("h1")])
                        fen += '/';

                    empty = 0;
                    i += 8;
                }
            }

            var castling = string.Empty;

            if ((_castling[ChessColor.White] & (int)Bits.KSideCastle) != 0)
                castling += 'K';

            if ((_castling[ChessColor.White] & (int)Bits.QSideCastle) != 0)
                castling += 'Q';

            if ((_castling[ChessColor.Black] & (int)Bits.KSideCastle) != 0)
                castling += 'k';

            if ((_castling[ChessColor.Black] & (int)Bits.QSideCastle) != 0)
                castling += 'q';

            if (castling == string.Empty)
                castling = "-";

            var epSquare = "-";

            if (_epSquare != InternalData.Empty)
            {
                var bigPawnSquare = _epSquare + (_turn == ChessColor.White ? 16 : -16);
                var squares = new[] { bigPawnSquare + 1, bigPawnSquare - 1 };

                foreach (var square in squares)
                {
                    if ((square & 0x88) != 0)
                        continue;

                    var color = _turn;

                    if (_board[square]?.Color == color && _board[square]?.PieceType == ChessPieceType.Pawn)
                    {
                        MakeMove(new InternalMove(color, square, _epSquare, ChessPieceType.Pawn, ChessPieceType.Pawn, null, Bits.EpCapture));

                        var isLegal = !IsKingAttacked(color);
                        UndoMove();

                        if (isLegal)
                        {
                            epSquare = HelperUtility.Algebraic(_epSquare).ToString();

                            break;
                        }
                    }
                }
            }

            return $"{fen} {(char)_turn} {castling} {epSquare} {_halfMoves} {_moveNumber}";
        }

        public string GetPgn(string newLine = "\r\n", int maxWidth = 0)
        {
            List<string> result = new List<string>();

            var headerExists = false;

            foreach (var header in _headers)
            {
                result.Add($"[{header.Key} \"{header.Value}\"]{newLine}");

                headerExists = true;
            }

            if (headerExists && _history.Count > 0)
                result.Add(newLine);

            var reversedHistory = new Stack<InternalMove?>();
            while (_history.Count > 0)
                reversedHistory.Push(UndoMove());

            var moves = new List<string>();
            var moveString = string.Empty;

            if (reversedHistory.Count == 0)
                moves.Add(AppendComment(""));

            while (reversedHistory.Count > 0)
            {
                moveString = AppendComment(moveString);

                var move = reversedHistory.Pop();

                if (move == null)
                    break;

                if (_history.Count == 0 && move.Color == ChessColor.Black)
                {
                    var prefix = $"{_moveNumber}. ...";

                    moveString = moveString != string.Empty ? $"{moveString} {prefix}" : prefix;
                }
                else if (move.Color == ChessColor.White)
                {
                    if (moveString.Length > 0)
                        moves.Add(moveString);

                    moveString = $"{_moveNumber}.";
                }

                moveString += $" {MoveToSan(move, Moves(true).ToArray())}";

                MakeMove(move);
            }

            if (moveString.Length > 0)
                moves.Add(AppendComment(moveString));

            var resultHeader = _headers.FirstOrDefault(h => h.Key == "Result");
            if (resultHeader != null)
                moves.Add(resultHeader.Value);

            if (maxWidth == 0)
                return string.Join("", result) + string.Join(" ", moves);

            var currentWidth = 0;

            for (var i = 0; i < moves.Count; i++)
            {
                if (currentWidth + moves[i].Length > maxWidth)
                {
                    if (moves[i].Contains('{'))
                    {
                        currentWidth = WrapComment(currentWidth, moves[i]);

                        continue;
                    }
                }

                if (currentWidth + moves[i].Length > maxWidth && i != 0)
                {
                    if (result[^1] == " ")
                        result.RemoveAt(result.Count - 1);

                    result.Add(newLine);

                    currentWidth = 0;
                }
                else if (i != 0)
                {
                    result.Add(" ");

                    currentWidth++;
                }

                result.Add(moves[i]);

                currentWidth += moves[i].Length;
            }

            return string.Join("", result);

            string AppendComment(string moveString)
            {
                _comments.TryGetValue(GetFen(), out var comment);

                if (comment != null)
                {
                    var delimiter = moveString.Length > 0 ? " " : "";
                    moveString = $"{moveString}{delimiter}{{{comment}}}";
                }

                return moveString;
            }

            bool Strip()
            {
                if (result.Count > 0 && result[^1] == " ")
                {
                    result.RemoveAt(result.Count - 1);

                    return true;
                }

                return false;
            }

            int WrapComment(int width, string move)
            {
                foreach (var token in move.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                {
                    if (width + token.Length > maxWidth)
                    {
                        while (Strip())
                            width--;

                        result.Add(newLine);

                        width = 0;
                    }

                    result.Add(token);

                    width += token.Length;

                    result.Add(" ");

                    width++;
                }

                if (Strip())
                    width--;

                return width;
            }
        }

        public ChessMove Move(string move, bool strict = false)
        {
            var moveObj = MoveFromSan(move, strict);

            if (moveObj == null)
                throw new InvalidMoveException($"Invalid move: {move}");

            var prettyMove = MakePretty(moveObj);

            MakeMove(moveObj);
            IncPositionCount(prettyMove.After);

            return prettyMove;
        }

        public ChessMove Move(MoveInfo move)
        {
            InternalMove? moveObj = null;

            var from = move.From;
            var to = move.To;
            var promotion = move.Promotion;

            var moves = Moves();

            for (int i = 0, len = moves.Count; i < len; i++)
            {
                if
                (
                    from == HelperUtility.Algebraic(moves[i].From).ToString() &&
                    to == HelperUtility.Algebraic(moves[i].To).ToString() &&
                    (moves[i].Promotion == null || promotion == $"{(char)moves[i].Promotion!}")
                )
                {
                    moveObj = moves[i];

                    break;
                }
            }

            if (moveObj == null)
            {
                var errorMessage = $"{{from: \"{from}\", to: \"{to}\", promotion: \"{promotion}\"}}";

                throw new InvalidMoveException($"Invalid move: {errorMessage}");
            }

            var prettyMove = MakePretty(moveObj);

            MakeMove(moveObj);
            IncPositionCount(prettyMove.After);

            return prettyMove;
        }

        public ChessMove? Undo()
        {
            var move = UndoMove();
            if (move != null)
            {
                var prettyMove = MakePretty(move);

                DecPositionCount(prettyMove.After);

                return prettyMove;
            }

            return null;
        }

        public void SetHeader(PgnHeader pgnHeader)
        {
            var matchedHeader = _headers.FirstOrDefault(h => h == pgnHeader);
            if (matchedHeader == null)
                _headers.Add(pgnHeader);
            else
                matchedHeader.Value = pgnHeader.Value;
        }

        public PgnHeader[] GetHeaders() => _headers.Select(kv => new PgnHeader(kv.Key, kv.Value)).ToArray();

        public bool RemoveHeader(string key)
        {
            var matchedHeader = _headers.FirstOrDefault(h => h.Key == key);
            if (matchedHeader == null)
                return false;

            return _headers.Remove(matchedHeader);
        }

        public BoardItem?[][] GetBoard()
        {
            var output = new List<BoardItem?[]>();
            var row = new List<BoardItem?>();

            for (var i = InternalData.Ox88[new ChessSquare("a8")]; i <= InternalData.Ox88[new ChessSquare("h1")]; i++)
            {
                var boardItem = _board[i];

                row.Add(boardItem == null ? null : new BoardItem(HelperUtility.Algebraic(i), boardItem.PieceType, boardItem.Color));

                if (((i + 1) & 0x88) != 0)
                {
                    output.Add(row.ToArray());

                    row.Clear();

                    i += 8;
                }
            }

            return output.ToArray();
        }

        public string[] GetHistory()
        {
            var reversedHistory = new Stack<InternalMove?>();
            var moveHistory = new List<string>();

            while (_history.Count > 0)
                reversedHistory.Push(UndoMove());

            while (true)
            {
                if (reversedHistory.Count == 0)
                    break;

                var move = reversedHistory.Pop();
                if (move == null)
                    break;

                moveHistory.Add(MoveToSan(move, Moves().ToArray()));

                MakeMove(move);
            }

            return moveHistory.ToArray();
        }

        public ChessMove[] GetHistoryVerbose()
        {
            var reversedHistory = new Stack<InternalMove?>();
            var moveHistory = new List<ChessMove>();

            while (_history.Count > 0)
                reversedHistory.Push(UndoMove());

            while (true)
            {
                if (reversedHistory.Count == 0)
                    break;

                var move = reversedHistory.Pop();
                if (move == null)
                    break;

                moveHistory.Add(MakePretty(move));

                MakeMove(move);
            }

            return moveHistory.ToArray();
        }

        public string ToAscii()
        {
            var s = "   +------------------------+\n";

            for (var i = InternalData.Ox88[new ChessSquare("a8")]; i <= InternalData.Ox88[new ChessSquare("h1")]; i++)
            {
                if (HelperUtility.File(i) == 0)
                    s += " " + "87654321"[HelperUtility.Rank(i)] + " |";

                var boardPiece = _board[i];
                if (boardPiece != null)
                {
                    var piece = boardPiece.PieceType;
                    var color = boardPiece.Color;
                    var symbol = color == ChessColor.White ? char.ToUpper((char)piece) : char.ToLower((char)piece);

                    s += " " + symbol + " ";
                }
                else
                    s += " . ";

                if (((i + 1) & 0x88) != 0)
                {
                    s += "|\n";
                    i += 8;
                }
            }

            s += "   +------------------------+\n";
            s += "     a  b  c  d  e  f  g  h";

            return s;
        }

        public ChessSquare[] GetAttackers(ChessSquare square, ChessColor? attackedBy) => Attacked(attackedBy ?? _turn, InternalData.Ox88[square]);

        public bool SetCastlingRights(ChessColor color, CastlingRights castlingRights)
        {
            if (castlingRights.KingCastlingRights != null)
            {
                if (castlingRights.KingCastlingRights.Value)
                    _castling[color] |= (int)InternalData.Sides[ChessPieceType.King];
                else
                    _castling[color] &= ~(int)InternalData.Sides[ChessPieceType.King];
            }

            if (castlingRights.QueenCastlingRights != null)
            {
                if (castlingRights.QueenCastlingRights.Value)
                    _castling[color] |= (int)InternalData.Sides[ChessPieceType.Queen];
                else
                    _castling[color] &= ~(int)InternalData.Sides[ChessPieceType.Queen];
            }

            UpdateCastlingRights();

            var result = GetCastlingRights(color);

            return (castlingRights.KingCastlingRights == null || castlingRights.KingCastlingRights == result.KingCastlingRights) &&
                   (castlingRights.QueenCastlingRights == null || castlingRights.QueenCastlingRights == result.QueenCastlingRights);
        }

        public CastlingRights GetCastlingRights(ChessColor color)
        {
            return new CastlingRights((_castling[color] & (int)InternalData.Sides[ChessPieceType.King]) != 0,
                (_castling[color] & (int)InternalData.Sides[ChessPieceType.Queen]) != 0);
        }

        public ChessPiece? GetPiece(ChessSquare square) => _board[InternalData.Ox88[square]] ?? null;

        public void LoadPgn(string pgn, bool strict = false)
        {
            pgn = pgn.Trim();

            var matches = Regex.Match(pgn, @"^(\[((?:\r?\n)|.)*\])((?:\s*\r?\n){2}|(?:\s*\r?\n)*$)");
            var headerString = matches.Groups.Count >= 2 ? matches.Groups[1].Value : string.Empty;

            Reset();

            var headers = ParsePgnHeader(headerString);
            var fen = string.Empty;

            foreach (var key in headers.Keys)
            {
                if (key.ToLower() == "fen")
                    fen = headers[key];

                SetHeader(new PgnHeader(key, headers[key]));
            }

            if (!strict)
            {
                if (fen != string.Empty)
                    LoadFen(fen, preserveHeaders: true);
            }
            else
            {
                if (headers.TryGetValue("SetUp", out var setUpValue) && setUpValue == "1")
                {
                    if (!headers.ContainsKey("FEN"))
                        throw new InvalidPgnException("Invalid PGN: FEN tag must be supplied with SetUp tag");

                    LoadFen(headers["FEN"], preserveHeaders: true);
                }
            }

            var ms = headerString != string.Empty ? pgn.Replace(headerString, string.Empty) : pgn;

            ms = Regex.Replace(ms, @"(?<bracket>{[^}]*})+?|;(?<semicolon>([^\r?\n]*))", (match) => match.Groups["bracket"].Success
                ? EncodeComment(match.Groups["bracket"].Value)
                : ' ' + (match.Groups["semicolon"].Value == string.Empty ? "{}" : EncodeComment($"{{{match.Groups["semicolon"].Value[1..]}}}")));

            ms = Regex.Replace(ms, @"\r?\n", " ");

            var ravRegex = @"(\([^()]+\))+?";

            while (Regex.IsMatch(ms, ravRegex))
                ms = Regex.Replace(ms, ravRegex, "");

            ms = Regex.Replace(ms, @"\d+\.(\.\.)?", "");
            ms = Regex.Replace(ms, @"\.\.\.", "");
            ms = Regex.Replace(ms, @"\$\d+", "");

            var moves = Regex.Split(ms.Trim(), @"\s+");
            moves = moves.Where((move) => move != "").ToArray();

            var result = string.Empty;

            foreach (var m in moves)
            {
                var comment = DecodeComment(m);

                if (comment != null)
                {
                    _comments[GetFen()] = comment;

                    continue;
                }

                var move = MoveFromSan(m, strict);

                if (move == null)
                {
                    if (Array.IndexOf(InternalData.TerminationMarkers, m) != -1)
                        result = m;
                    else
                        throw new InvalidPgnMoveException($"Invalid move in PGN: {m}");
                }
                else
                {
                    result = string.Empty;

                    MakeMove(move);
                    IncPositionCount(GetFen());
                }
            }

            if (result != string.Empty && _headers.Count > 0 && _headers.Count(h => h.Key == "Result") == 0)
                SetHeader(new Public.PgnHeader("Result", result));
            Dictionary<string, string> ParsePgnHeader(string header)
            {
                var headerObj = new Dictionary<string, string>();
                var headers = Regex.Split(header, "\r?\n");

                foreach (var h in headers)
                {
                    var match = Regex.Match(h, @"^\s*\[\s*(?<key>[A-Za-z]+)\s*""(?<value>.*)""\s*\]\s*$");

                    if (match.Success)
                    {
                        var key = match.Groups["key"].Value;
                        var value = match.Groups["value"].Value;

                        if (key.Trim().Length > 0)
                            headerObj[key] = value;
                    }
                }

                return headerObj;
            }

            string ToHex(string s)
            {
                var output = string.Concat(s.Select(c => c < 256 ? Uri.HexEscape(c) : Uri.EscapeDataString($"{c}")));

                return output;
            }

            string FromHex(string s)
            {
                if (s.Length == 0)
                    return string.Empty;

                var matches = Regex.Matches(s, @".{1,2}");
                var input = matches.Count > 0 ? string.Join("", matches.Select(m => m.Value)) : string.Empty;

                return Uri.UnescapeDataString($"{input}");
            }

            string EncodeComment(string s)
            {
                s = Regex.Replace(s, @"\r?\n", " ");

                return $"{{{ToHex(s[1..^1])}}}";
            }

            string? DecodeComment(string s)
            {
                if (s.StartsWith("{") && s.EndsWith("}"))
                    return FromHex(s[1..^1]);

                return null;
            }
        }

        public bool PutPiece(ChessPiece piece, ChessSquare square)
        {
            if (Put(piece.PieceType, piece.Color, square))
            {
                UpdateCastlingRights();
                UpdateEnPassantSquare();
                UpdateSetup(GetFen());

                return true;
            }

            return false;
        }

        public string[] GetMoves(ChessPieceType? piece = null, ChessSquare? square = null)
        {
            var moves = Moves(piece: piece, square: square);

            return moves.Select(move => MoveToSan(move, moves.ToArray())).ToArray();
        }

        public ChessMove[] GetMovesVerbose(ChessPieceType? piece = null, ChessSquare? square = null)
        {
            var moves = Moves(piece: piece, square: square);

            return moves.Select(MakePretty).ToArray();
        }

        public ChessPiece? RemovePiece(ChessSquare square)
        {
            var piece = GetPiece(square);

            _board[InternalData.Ox88[square]] = null;

            if (piece is { PieceType: ChessPieceType.King })
                _kings[piece.Color] = InternalData.Empty;

            UpdateCastlingRights();
            UpdateEnPassantSquare();
            UpdateSetup(GetFen());

            return piece;
        }

        public string? GetComment()
        {
            var fen = GetFen();

            if (_comments.TryGetValue(fen, out var comment))
                return comment;

            return null;
        }

        public void SetComment(string comment) => _comments[GetFen()] = comment.Replace('{', '[').Replace('}', ']');

        public bool DeleteComment()
        {
            var fen = GetFen();

            return _comments.Remove(fen);
        }

        public CommentInfo[] GetComments()
        {
            PruneComments();

            return _comments.Select(kv => new CommentInfo(kv.Key, kv.Value)).ToArray();
        }

        public void DeleteComments()
        {
            PruneComments();

            _comments.Clear();
        }

        public int Perft(int depth)
        {
            var moves = Moves(false);

            var nodes = 0;
            var color = _turn;

            for (int i = 0, len = moves.Count; i < len; i++)
            {
                MakeMove(moves[i]);

                if (!IsKingAttacked(color))
                {
                    if (depth - 1 > 0)
                        nodes += Perft(depth - 1);
                    else
                        nodes++;
                }

                UndoMove();
            }

            return nodes;
        }

        public void Reset() => LoadFen(PublicData.DefaultChessPosition);

        public bool IsAttacked(ChessSquare square, ChessColor attackedBy) => Attacked(attackedBy, InternalData.Ox88[square]).Length > 0;

        public bool IsCheck() => IsKingAttacked(_turn);

        public bool IsCheckmate() => IsCheck() && Moves().Count == 0;

        public bool IsStalemate() => !IsCheck() && Moves().Count == 0;

        public bool IsThreefoldRepetition() => GetPositionCount(GetFen()) >= 3;

        public bool IsInsufficientMaterial()
        {
            var pieces = new Dictionary<ChessPieceType, int>()
            {
                { ChessPieceType.Bishop, 0},
                { ChessPieceType.Knight, 0},
                { ChessPieceType.Rook, 0},
                { ChessPieceType.Queen, 0},
                { ChessPieceType.King, 0},
                { ChessPieceType.Pawn, 0},
            };

            var bishops = new List<int>();

            var numPieces = 0;
            var squareColor = 0;

            for (var i = InternalData.Ox88[new ChessSquare("a8")]; i <= InternalData.Ox88[new ChessSquare("h1")]; i++)
            {
                squareColor = (squareColor + 1) % 2;

                if ((i & 0x88) != 0)
                {
                    i += 7;

                    continue;
                }

                var piece = _board[i];

                if (piece != null)
                {
                    pieces[piece.PieceType]++;

                    if (piece.PieceType == ChessPieceType.Bishop)
                        bishops.Add(squareColor);

                    numPieces++;
                }
            }

            if (numPieces == 2)
                return true;

            if (numPieces == 3 && (pieces[ChessPieceType.Bishop] == 1 || pieces[ChessPieceType.Knight] == 1))
                return true;

            if (numPieces == pieces[ChessPieceType.Bishop] + 2)
            {
                var sum = 0;
                var len = bishops.Count;

                for (var i = 0; i < len; i++)
                    sum += bishops[i];

                if (sum == 0 || sum == len)
                    return true;
            }

            return false;
        }

        public bool IsDraw() => _halfMoves >= 100 || IsStalemate() || IsInsufficientMaterial() || IsThreefoldRepetition();

        public bool IsGameOver() => IsCheckmate() || IsStalemate() || IsDraw();

        public ChessColor GetTurn() => _turn;

        public ChessColor GetSquareColor(ChessSquare square)
        {
            var sq = InternalData.Ox88[square];

            return (HelperUtility.Rank(sq) + HelperUtility.File(sq)) % 2 == 0 ? ChessColor.White : ChessColor.Black;
        }

        public int GetMoveNumber() => _moveNumber;

        public static FenValidationResult ValidateFen(string fen)
        {
            return FenValidator.ValidateFen(fen);
        }

        #region Private methods

        private bool Put(ChessPieceType type, ChessColor color, ChessSquare square)
        {
            if (!InternalData.Ox88.ContainsKey(square))
                return false;

            var sq = InternalData.Ox88[square];

            if (type == ChessPieceType.King && !(_kings[color] == InternalData.Empty || _kings[color] == sq))
                return false;

            var currentPieceOnSquare = _board[sq];

            if (currentPieceOnSquare is { PieceType: ChessPieceType.King })
                _kings[currentPieceOnSquare.Color] = InternalData.Empty;

            _board[sq] = new ChessPiece(color, type);

            if (type == ChessPieceType.King)
                _kings[color] = sq;

            return true;
        }

        private void UpdateSetup(string fen)
        {
            if (_history.Count > 0)
                return;

            if (fen != PublicData.DefaultChessPosition)
            {
                var setupHeader = _headers.FirstOrDefault(h => h.Key == "SetUp");
                if (setupHeader == null)
                    _headers.Add(new PgnHeader("SetUp", "1"));
                else
                    setupHeader.Value = "1";

                var fenHeader = _headers.FirstOrDefault(h => h.Key == "FEN");
                if (fenHeader == null)
                    _headers.Add(new PgnHeader("FEN", fen));
                else
                    fenHeader.Value = fen;
            }
            else
            {
                _headers.RemoveAll(h => h.Key == "SetUp");
                _headers.RemoveAll(h => h.Key == "FEN");
            }
        }

        private void IncPositionCount(string fen)
        {
            var trimmedFen = HelperUtility.TrimFen(fen);

            _positionCount.TryAdd(trimmedFen, 0);

            _positionCount[trimmedFen]++;
        }

        private void DecPositionCount(string fen)
        {
            var trimmedFen = HelperUtility.TrimFen(fen);

            if (_positionCount[trimmedFen] == 1)
                _positionCount.Remove(trimmedFen);
            else
                _positionCount[trimmedFen] -= 1;
        }

        private int GetPositionCount(string fen)
        {
            var trimmedFen = HelperUtility.TrimFen(fen);

            return _positionCount.TryGetValue(trimmedFen, out var value) ? value : 0;
        }

        private void MakeMove(InternalMove move)
        {
            var us = _turn;
            var them = HelperUtility.SwapColor(us);

            Push(move);

            _board[move.To] = _board[move.From];
            _board[move.From] = null;

            if ((move.Flags & Bits.EpCapture) != 0)
            {
                if (_turn == ChessColor.Black)
                    _board[move.To - 16] = null;
                else
                    _board[move.To + 16] = null;
            }

            if (move.Promotion != null)
                _board[move.To] = new ChessPiece(us, move.Promotion.Value);

            if (_board[move.To]!.PieceType == ChessPieceType.King)
            {
                _kings[us] = move.To;

                if ((move.Flags & Bits.KSideCastle) != 0)
                {
                    var castlingTo = move.To - 1;
                    var castlingFrom = move.To + 1;

                    _board[castlingTo] = _board[castlingFrom];
                    _board[castlingFrom] = null;
                }
                else if ((move.Flags & Bits.QSideCastle) != 0)
                {
                    var castlingTo = move.To + 1;
                    var castlingFrom = move.To - 2;

                    _board[castlingTo] = _board[castlingFrom];
                    _board[castlingFrom] = null;
                }

                _castling[us] = 0;
            }

            if (_castling[us] != 0)
            {
                foreach (var kv in InternalData.Rooks[us])
                {
                    if (move.From == kv.Key && (_castling[us] & kv.Value) != 0)
                    {
                        _castling[us] ^= kv.Value;

                        break;
                    }
                }
            }

            if (_castling[them] != 0)
            {
                foreach (var kv in InternalData.Rooks[them])
                {
                    if (move.To == kv.Key && (_castling[them] & kv.Value) != 0)
                    {
                        _castling[them] ^= kv.Value;

                        break;
                    }
                }
            }

            if ((move.Flags & Bits.BigPawn) != 0)
            {
                if (us == ChessColor.Black)
                    _epSquare = move.To - 16;
                else
                    _epSquare = move.To + 16;
            }
            else
                _epSquare = InternalData.Empty;

            if (move.Piece == ChessPieceType.Pawn)
                _halfMoves = 0;
            else if ((move.Flags & (Bits.Capture | Bits.EpCapture)) != 0)
                _halfMoves = 0;
            else
                _halfMoves++;

            if (us == ChessColor.Black)
                _moveNumber++;

            _turn = them;
        }

        private InternalMove? UndoMove()
        {
            if (!_history.Any())
                return null;

            var old = _history.Pop();

            var move = old.Move;

            _kings = old.Kings;
            _turn = old.Turn;
            _castling = old.Castling;
            _epSquare = old.EpSquare;
            _halfMoves = old.HalfMoves;
            _moveNumber = old.MoveNumber;

            var us = _turn;
            var them = HelperUtility.SwapColor(us);

            _board[move.From] = _board[move.To];
            _board[move.From]!.PieceType = move.Piece;

            _board[move.To] = null;

            if (move.Captured != null)
            {
                if ((move.Flags & Bits.EpCapture) != 0)
                {
                    int index;

                    if (us == ChessColor.Black)
                        index = move.To - 16;
                    else
                        index = move.To + 16;

                    _board[index] = new ChessPiece(them, ChessPieceType.Pawn);
                }
                else
                    _board[move.To] = new ChessPiece(them, move.Captured.Value);
            }

            if ((move.Flags & (Bits.KSideCastle | Bits.QSideCastle)) != 0)
            {
                int castlingTo, castlingFrom;

                if ((move.Flags & Bits.KSideCastle) != 0)
                {
                    castlingTo = move.To + 1;
                    castlingFrom = move.To - 1;
                }
                else
                {
                    castlingTo = move.To - 2;
                    castlingFrom = move.To + 1;
                }

                _board[castlingTo] = _board[castlingFrom];
                _board[castlingFrom] = null;
            }

            return move;
        }

        private void Push(InternalMove move)
        {
            _history.Push(new ChessHistory(
                move,
                new Dictionary<ChessColor, int> { { ChessColor.Black, _kings[ChessColor.Black] }, { ChessColor.White, _kings[ChessColor.White] } },
                _turn,
                new Dictionary<ChessColor, int> { { ChessColor.Black, _castling[ChessColor.Black] }, { ChessColor.White, _castling[ChessColor.White] } },
                _epSquare,
                _halfMoves,
                _moveNumber
            ));
        }

        private bool IsKingAttacked(ChessColor color)
        {
            var square = _kings[color];

            return square != -1 && Attacked(HelperUtility.SwapColor(color), square).Length > 0;
        }

        private ChessSquare[] Attacked(ChessColor color, int square)
        {
            var attackers = new List<ChessSquare>();

            for (var i = InternalData.Ox88[new ChessSquare("a8")]; i <= InternalData.Ox88[new ChessSquare("h1")]; i++)
            {
                if ((i & 0x88) != 0)
                {
                    i += 7;

                    continue;
                }

                var piece = _board[i];
                if (piece == null || piece.Color != color)
                    continue;

                var difference = i - square;

                if (difference == 0)
                    continue;

                var index = difference + 119;

                if ((InternalData.Attacks[index] & InternalData.PieceMasks[piece.PieceType]) != 0)
                {
                    if (piece.PieceType == ChessPieceType.Pawn)
                    {
                        if ((difference > 0 && piece.Color == ChessColor.White) || (difference <= 0 && piece.Color == ChessColor.Black))
                            attackers.Add(HelperUtility.Algebraic(i));

                        continue;
                    }

                    if (piece.PieceType == ChessPieceType.Knight || piece.PieceType == ChessPieceType.King)
                    {
                        attackers.Add(HelperUtility.Algebraic(i));

                        continue;
                    }

                    var offset = InternalData.Rays[index];
                    var j = i + offset;
                    var blocked = false;

                    while (j != square)
                    {
                        if (_board[j] != null)
                        {
                            blocked = true;

                            break;
                        }

                        j += offset;
                    }

                    if (!blocked)
                    {
                        attackers.Add(HelperUtility.Algebraic(i));

                        continue;
                    }
                }
            }

            return attackers.ToArray();
        }

        private string MoveToSan(InternalMove move, InternalMove[] moves)
        {
            var output = string.Empty;

            if ((move.Flags & Bits.KSideCastle) != 0)
                output = "O-O";
            else if ((move.Flags & Bits.QSideCastle) != 0)
                output = "O-O-O";
            else
            {
                if (move.Piece != ChessPieceType.Pawn)
                {
                    var disambiguator = HelperUtility.GetDisambiguator(move, moves);
                    output += char.ToUpper((char)move.Piece) + disambiguator;
                }

                if ((move.Flags & (Bits.Capture | Bits.EpCapture)) != 0)
                {
                    if (move.Piece == ChessPieceType.Pawn)
                        output += HelperUtility.Algebraic(move.From).File;

                    output += 'x';
                }

                output += HelperUtility.Algebraic(move.To);

                var promotion = move.Promotion;
                if (promotion != null)
                    output += "=" + char.ToUpper((char)promotion);
            }

            MakeMove(move);

            if (IsCheck())
            {
                if (IsCheckmate())
                    output += '#';
                else
                    output += '+';
            }

            UndoMove();

            return output;
        }

        private InternalMove? MoveFromSan(string move, bool strict = false)
        {
            var cleanMove = HelperUtility.StripSan(move);

            var pieceType = HelperUtility.InferPieceType(cleanMove);
            var moves = Moves(true, pieceType);

            for (int i = 0, len = moves.Count; i < len; i++)
            {
                if (cleanMove == HelperUtility.StripSan(MoveToSan(moves[i], moves.ToArray())))
                    return moves[i];
            }

            if (strict)
                return null;

            ChessPieceType? piece = null;
            string? from = null;
            ChessSquare? to = null;
            ChessPieceType? promotion = null;

            var overlyDisambiguated = false;

            var match = Regex.Match(cleanMove, @"(?<piece>[pnbrqkPNBRQK])?(?<from>[a-h][1-8])x?-?(?<to>[a-h][1-8])(?<promotion>[qrbnQRBN])?");

            if (match.Success)
            {
                piece = match.Groups["piece"].Success ? (ChessPieceType)char.ToLower(match.Groups["piece"].Value[0]) : null;
                from = match.Groups["from"].Value;
                to = new ChessSquare(match.Groups["to"].Value);
                promotion = match.Groups["promotion"].Success ? (ChessPieceType)char.ToLower(match.Groups["promotion"].Value[0]) : null;

                if (from?.Length == 1)
                    overlyDisambiguated = true;
            }
            else
            {
                match = Regex.Match(cleanMove, @"(?<piece>[pnbrqkPNBRQK])?(?<from>[a-h]?[1-8]?)x?-?(?<to>[a-h][1-8])(?<promotion>[qrbnQRBN])?");

                if (match.Success)
                {
                    piece = match.Groups["piece"].Success ? (ChessPieceType)char.ToLower(match.Groups["piece"].Value[0]) : null;
                    from = match.Groups["from"].Value;
                    to = new ChessSquare(match.Groups["to"].Value);
                    promotion = match.Groups["promotion"].Success ? (ChessPieceType)char.ToLower(match.Groups["piece"].Value[0]) : null;

                    if (from?.Length == 1)
                        overlyDisambiguated = true;
                }
            }

            moves = Moves(true, piece ?? pieceType);


            if (to == null)
                return null;

            for (int i = 0, len = moves.Count; i < len; i++)
            {
                if (string.IsNullOrEmpty(from))
                {
                    if (cleanMove == HelperUtility.StripSan(MoveToSan(moves[i], moves.ToArray())).Replace("x", string.Empty))
                        return moves[i];
                }
                else if (overlyDisambiguated)
                {
                    var square = HelperUtility.Algebraic(moves[i].From);

                    if (
                        (piece == null || piece == moves[i].Piece) &&
                        InternalData.Ox88[to.Value] == moves[i].To &&
                        (from[0] == square.File || from[0] == square.Rank) &&
                        (promotion == null || promotion == moves[i].Promotion)
                    )
                        return moves[i];
                }
                else if (
                  (piece == null || piece == moves[i].Piece) &&
                  InternalData.Ox88[new ChessSquare(from)] == moves[i].From &&
                  InternalData.Ox88[to.Value] == moves[i].To &&
                  (promotion == null || promotion == moves[i].Promotion)
                )
                    return moves[i];
            }

            return null;
        }

        private ChessMove MakePretty(InternalMove uglyMove)
        {
            var color = uglyMove.Color;
            var piece = uglyMove.Piece;
            var from = uglyMove.From;
            var to = uglyMove.To;
            var flags = uglyMove.Flags;
            var captured = uglyMove.Captured;
            var promotion = uglyMove.Promotion;

            var prettyFlags = string.Empty;

            foreach (var flag in Enum.GetValues<Bits>())
            {
                if ((flag & flags) != 0)
                    prettyFlags += (char)Enum.Parse<Flags>(flag.ToString());
            }

            var fromAlgebraic = HelperUtility.Algebraic(from);
            var toAlgebraic = HelperUtility.Algebraic(to);

            var move = new ChessMove(
                color,
                fromAlgebraic,
                toAlgebraic,
                piece,
                null,
                null,
                prettyFlags,
                MoveToSan(uglyMove, Moves().ToArray()),
                fromAlgebraic.ToString() + toAlgebraic,
                GetFen(),
                string.Empty);

            MakeMove(uglyMove);

            move.After = GetFen();

            UndoMove();

            if (captured != null)
                move.Captured = captured;

            if (promotion != null)
            {
                move.Promotion = promotion;
                move.Lan += (char)promotion;
            }

            return move;
        }

        private List<InternalMove> Moves(bool legal = true, ChessPieceType? piece = null, ChessSquare? square = null)
        {
            ChessSquare? forSquare = square;

            var forPiece = piece;

            var moves = new List<InternalMove>();

            var us = _turn;
            var them = HelperUtility.SwapColor(us);

            var firstSquare = InternalData.Ox88[new ChessSquare("a8")];
            var lastSquare = InternalData.Ox88[new ChessSquare("h1")];

            var singleSquare = false;

            if (forSquare != null)
            {
                if (!InternalData.Ox88.ContainsKey(forSquare.Value))
                    return new List<InternalMove>();

                firstSquare = lastSquare = InternalData.Ox88[forSquare.Value];

                singleSquare = true;
            }

            for (var from = firstSquare; from <= lastSquare; from++)
            {
                if ((from & 0x88) != 0)
                {
                    from += 7;

                    continue;
                }

                var fromPiece = _board[from];
                if (fromPiece == null || fromPiece.Color == them)
                    continue;

                var type = fromPiece.PieceType;

                int to;

                if (type == ChessPieceType.Pawn)
                {
                    if (forPiece != null && forPiece != type)
                        continue;

                    to = from + InternalData.PawnOffsets[us][0];
                    if (_board[to] == null)
                    {
                        HelperUtility.AddMove(moves, us, from, to, ChessPieceType.Pawn);

                        to = from + InternalData.PawnOffsets[us][1];

                        if (InternalData.SecondRank[us] == HelperUtility.Rank(from) && _board[to] == null)
                            HelperUtility.AddMove(moves, us, from, to, ChessPieceType.Pawn, null, Bits.BigPawn);
                    }

                    for (var j = 2; j < 4; j++)
                    {
                        to = from + InternalData.PawnOffsets[us][j];
                        if ((to & 0x88) != 0)
                            continue;

                        if (_board[to]?.Color == them)
                            HelperUtility.AddMove(moves, us, from, to, ChessPieceType.Pawn, _board[to]?.PieceType, Bits.Capture);
                        else if (to == _epSquare)
                            HelperUtility.AddMove(moves, us, from, to, ChessPieceType.Pawn, ChessPieceType.Pawn, Bits.EpCapture);
                    }
                }
                else
                {
                    if (forPiece != null && forPiece != type)
                        continue;

                    for (int j = 0, len = InternalData.PieceOffsets[type].Length; j < len; j++)
                    {
                        var offset = InternalData.PieceOffsets[type][j];
                        to = from;

                        while (true)
                        {
                            to += offset;
                            if ((to & 0x88) != 0)
                                break;

                            if (_board[to] == null)
                                HelperUtility.AddMove(moves, us, from, to, type);
                            else
                            {
                                if (_board[to]?.Color == us)
                                    break;

                                HelperUtility.AddMove(
                                    moves,
                                    us,
                                    from,
                                    to,
                                    type,
                                    _board[to]?.PieceType,
                                    Bits.Capture
                                );

                                break;
                            }

                            if (type is ChessPieceType.Knight or ChessPieceType.King)
                                break;
                        }
                    }
                }
            }

            if (forPiece is null or ChessPieceType.King)
            {
                if (!singleSquare || lastSquare == _kings[us])
                {
                    if ((_castling[us] & (int)Bits.KSideCastle) != 0)
                    {
                        var castlingFrom = _kings[us];
                        var castlingTo = castlingFrom + 2;

                        if (
                        _board[castlingFrom + 1] == null &&
                        _board[castlingTo] == null &&
                        Attacked(them, _kings[us]).Length == 0 &&
                        Attacked(them, castlingFrom + 1).Length == 0 &&
                        Attacked(them, castlingTo).Length == 0
                        )
                        {
                            HelperUtility.AddMove(
                                moves,
                                us,
                                _kings[us],
                                castlingTo,
                                ChessPieceType.King,
                                null,
                                Bits.KSideCastle
                            );
                        }
                    }

                    if ((_castling[us] & (int)Bits.QSideCastle) != 0)
                    {
                        var castlingFrom = _kings[us];
                        var castlingTo = castlingFrom - 2;

                        if (
                        _board[castlingFrom - 1] == null &&
                        _board[castlingFrom - 2] == null &&
                        _board[castlingFrom - 3] == null &&
                        Attacked(them, this._kings[us]).Length == 0 &&
                        Attacked(them, castlingFrom - 1).Length == 0 &&
                        Attacked(them, castlingTo).Length == 0
                        )
                        {
                            HelperUtility.AddMove(
                                moves,
                                us,
                                _kings[us],
                                castlingTo,
                                ChessPieceType.King,
                                null,
                                Bits.QSideCastle
                            );
                        }
                    }
                }
            }

            if (!legal || _kings[us] == -1)
                return moves;

            var legalMoves = new List<InternalMove>();

            for (int i = 0, len = moves.Count; i < len; i++)
            {
                MakeMove(moves[i]);

                if (!IsKingAttacked(us))
                    legalMoves.Add(moves[i]);

                UndoMove();
            }

            return legalMoves;
        }

        private void PruneComments()
        {
            var reversedHistory = new Stack<InternalMove?>();
            var currentComments = new Dictionary<string, string>();

            while (_history.Count > 0)
                reversedHistory.Push(UndoMove());

            CopyComment(GetFen());

            while (true)
            {
                if (reversedHistory.Count == 0)
                    break;

                var move = reversedHistory.Pop();
                if (move == null)
                    break;

                MakeMove(move);
                CopyComment(GetFen());
            }

            _comments = currentComments;
            void CopyComment(string fen)
            {
                if (_comments.TryGetValue(fen, out var comment))
                    currentComments[fen] = comment;
            }
        }

        private void UpdateCastlingRights()
        {
            var whiteKingInPlace = _board[InternalData.Ox88[new ChessSquare("e1")]]?.PieceType == ChessPieceType.King &&
                                   _board[InternalData.Ox88[new ChessSquare("e1")]]?.Color == ChessColor.White;

            var blackKingInPlace = _board[InternalData.Ox88[new ChessSquare("e8")]]?.PieceType == ChessPieceType.King &&
                                   _board[InternalData.Ox88[new ChessSquare("e8")]]?.Color == ChessColor.Black;

            if (!whiteKingInPlace ||
                _board[InternalData.Ox88[new ChessSquare("a1")]]?.PieceType != ChessPieceType.Rook
                || _board[InternalData.Ox88[new ChessSquare("a1")]]?.Color != ChessColor.White
               )
                _castling[ChessColor.White] &= ~(int)Bits.QSideCastle;

            if (!whiteKingInPlace ||
                _board[InternalData.Ox88[new ChessSquare("h1")]]?.PieceType != ChessPieceType.Rook ||
                _board[InternalData.Ox88[new ChessSquare("h1")]]?.Color != ChessColor.White
               )
                _castling[ChessColor.White] &= ~(int)Bits.KSideCastle;

            if (!blackKingInPlace ||
                _board[InternalData.Ox88[new ChessSquare("a8")]]?.PieceType != ChessPieceType.Rook ||
                _board[InternalData.Ox88[new ChessSquare("a8")]]?.Color != ChessColor.Black
               )
                _castling[ChessColor.Black] &= ~(int)Bits.QSideCastle;

            if (!blackKingInPlace ||
                _board[InternalData.Ox88[new ChessSquare("h8")]]?.PieceType != ChessPieceType.Rook ||
                _board[InternalData.Ox88[new ChessSquare("h8")]]?.Color != ChessColor.Black
               )
                _castling[ChessColor.Black] &= ~(int)Bits.KSideCastle;
        }

        private void UpdateEnPassantSquare()
        {
            if (_epSquare == InternalData.Empty)
                return;

            var startSquare = _epSquare + (_turn == ChessColor.White ? -16 : 16);
            var currentSquare = _epSquare + (_turn == ChessColor.White ? 16 : -16);
            var attackers = new[] { currentSquare + 1, currentSquare - 1 };

            if (_board[startSquare] != null || _board[_epSquare] != null ||
                _board[currentSquare]?.Color != HelperUtility.SwapColor(_turn) || _board[currentSquare]?.PieceType != ChessPieceType.Pawn
            )
            {
                _epSquare = InternalData.Empty;

                return;
            }

            var canCapture = (int square) => (square & 0x88) == 0 && _board[square]?.Color == _turn && _board[square]?.PieceType == ChessPieceType.Pawn;

            if (!attackers.Any(canCapture))
                _epSquare = InternalData.Empty;
        }

        #endregion
    }
}
