using ChessDotNet.Tests.TestData;

namespace ChessDotNet.Tests
{
    public class FenValidatorTest
    {
        [Fact]
        public void ValidateTest_FenWithNoKings_ReturnsError()
        {
            string fen = "8/8/8/8/8/8/8/8 w - - 0 1";

            var validationResult = FenValidator.ValidateFen(fen);

            Assert.False(validationResult.Ok);
            Assert.Equal("Missing white king", validationResult.Error);
        }

        [Fact]
        public void ValidateTest_MissingWhiteKing_ReturnsError()
        {
            string fen = "k7/8/8/8/8/8/8/8 w - - 0 1";

            var validationResult = FenValidator.ValidateFen(fen);

            Assert.False(validationResult.Ok);
            Assert.Equal("Missing white king", validationResult.Error);
        }

        [Fact]
        public void ValidateTest_MissingBlackKing_ReturnsError()
        {
            string fen = "8/8/8/8/8/8/8/7K w - - 0 1";

            var validationResult = FenValidator.ValidateFen(fen);

            Assert.False(validationResult.Ok);
            Assert.Equal("Missing black king", validationResult.Error);
        }

        [Fact]
        public void ValidateTest_TooManyWhiteKings_ReturnsError()
        {
            string fen = "k7/8/8/8/8/8/8/6KK w - - 0 1";

            var validationResult = FenValidator.ValidateFen(fen);

            Assert.False(validationResult.Ok);
            Assert.Equal("Too many white kings", validationResult.Error);
        }

        [Fact]
        public void ValidateTest_TooManyBlackKings_ReturnsError()
        {
            string fen = "kk6/8/8/8/8/8/8/7K w - - 0 1";

            var validationResult = FenValidator.ValidateFen(fen);

            Assert.False(validationResult.Ok);
            Assert.Equal("Too many black kings", validationResult.Error);
        }

        [Fact]
        public void ValidateTest_WhitePawnOn8thRow_ReturnsError()
        {
            string fen = "3P4/1k2K3/8/8/8/8/8/8 w - - 0 1";

            var validationResult = FenValidator.ValidateFen(fen);

            Assert.False(validationResult.Ok);
            Assert.Equal("Some pawns are on the edge rows", validationResult.Error);
        }

        [Fact]
        public void ValidateTest_BlackPawnOn1stRow_ReturnsError()
        {
            string fen = "8/8/8/8/8/8/1k3K2/3p4 w - - 0 1";

            var validationResult = FenValidator.ValidateFen(fen);

            Assert.False(validationResult.Ok);
            Assert.Equal("Some pawns are on the edge rows", validationResult.Error);
        }

        [Theory]
        [ClassData(typeof(FenValidatorFailedTestData))]
        public void ValidateTest_FailedInputData_ReturnsError(string fen)
        {
            var validationResult = FenValidator.ValidateFen(fen);

            Assert.False(validationResult.Ok);
            Assert.NotNull(validationResult.Error);
        }

        [Theory]
        [ClassData(typeof(FenValidatorCorrectTestData))]
        public void ValidateTest_CorrectInputData_ReturnsError(string fen)
        {
            var validationResult = FenValidator.ValidateFen(fen);

            Assert.Equal(true, validationResult.Ok);
            Assert.Null(validationResult.Error);
        }
    }
}