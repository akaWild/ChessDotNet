using ChessDotNet.Public;
using System.Reflection;

namespace ChessDotNet.Tests
{
    public class PositionCountFixture
    {
        public const string E4Fen = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq - 0 1";

        public Type ChessType { get; } = typeof(Chess);

        public MethodInfo? GetPositionCountMethod { get; }

        public FieldInfo? PositionCountDictionary { get; }

        public PositionCountFixture()
        {
            GetPositionCountMethod = ChessType.GetMethod("GetPositionCount", BindingFlags.NonPublic | BindingFlags.Instance);

            PositionCountDictionary = ChessType.GetField("_positionCount", BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }

    public class PositionCountTests : IClassFixture<PositionCountFixture>
    {
        private readonly PositionCountFixture _fixture;

        public PositionCountTests(PositionCountFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void PositionCount_CountsRepeatedPositions()
        {
            var chess = new Chess();

            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PublicData.DefaultChessPosition })));

            var fens = new List<string>() { PublicData.DefaultChessPosition };
            var moves = new string[] { "Nf3", "Nf6", "Ng1", "Ng8" };

            foreach (var move in moves)
            {
                foreach (var fen in fens)
                    Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { fen })));

                chess.Move(move);

                fens.Add(chess.GetFen());
            }

            Assert.Equal(2, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PublicData.DefaultChessPosition }) ?? -1));
            Assert.Equal(4, (int)(((Dictionary<string, int>)_fixture.PositionCountDictionary?.GetValue(chess)).Keys.Count));
        }

        [Fact]
        public void PositionCount_RemovesWhenUndo()
        {
            var chess = new Chess();

            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PublicData.DefaultChessPosition })));
            Assert.Equal(0, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PositionCountFixture.E4Fen })));

            chess.Move("e4");

            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PublicData.DefaultChessPosition })));
            Assert.Equal(PositionCountFixture.E4Fen, chess.GetFen());
            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PositionCountFixture.E4Fen })));

            chess.Undo();

            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PublicData.DefaultChessPosition })));
            Assert.Equal(0, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PositionCountFixture.E4Fen })));
            Assert.Equal(1, (int)(((Dictionary<string, int>)_fixture.PositionCountDictionary?.GetValue(chess)).Keys.Count));
        }

        [Fact]
        public void PositionCount_ResetsWhenCleared()
        {
            var chess = new Chess();

            chess.Move("e4");
            chess.ClearBoard();

            Assert.Equal(0, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PublicData.DefaultChessPosition })));
            Assert.Equal(0, (int)(((Dictionary<string, int>)_fixture.PositionCountDictionary?.GetValue(chess)).Keys.Count));
        }

        [Fact]
        public void PositionCount_ResetsWhenLoadingFen()
        {
            var chess = new Chess();

            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PublicData.DefaultChessPosition })));

            chess.Move("e4");

            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PublicData.DefaultChessPosition })));
            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PositionCountFixture.E4Fen })));

            var newFen = "rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2";

            chess.LoadFen(newFen);

            Assert.Equal(0, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PublicData.DefaultChessPosition })));
            Assert.Equal(0, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PositionCountFixture.E4Fen })));
            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { newFen })));
            Assert.Equal(1, (int)(((Dictionary<string, int>)_fixture.PositionCountDictionary?.GetValue(chess)).Keys.Count));
        }

        [Fact]
        public void PositionCount_ResetsWhenLoadingPgn()
        {
            var chess = new Chess();

            chess.Move("e4");
            chess.LoadPgn("1. d4 d5");

            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PublicData.DefaultChessPosition })));
            Assert.Equal(0, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { PositionCountFixture.E4Fen })));

            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { "rnbqkbnr/pppppppp/8/8/3P4/8/PPP1PPPP/RNBQKBNR b KQkq - 0 1" })));
            Assert.Equal(1, (int)(_fixture.GetPositionCountMethod?.Invoke(chess, new object[] { "rnbqkbnr/ppp1pppp/8/3p4/3P4/8/PPP1PPPP/RNBQKBNR w KQkq - 0 2" })));
            Assert.Equal(3, (int)(((Dictionary<string, int>)_fixture.PositionCountDictionary?.GetValue(chess)).Keys.Count));
        }
    }
}
