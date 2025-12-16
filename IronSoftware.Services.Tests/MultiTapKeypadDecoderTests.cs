using IronSoftware.Utilities;
using Xunit;

namespace IronSoftware.Services.Tests
{
    public class MultiTapKeypadDecoderTests
    {
        private readonly MultiTapKeypadDecoder _decoder;

        public MultiTapKeypadDecoderTests()
        {
            _decoder = new MultiTapKeypadDecoder();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("123")]        // missing #
        [InlineData("12A#")]       // invalid char
        [InlineData("12-3#")]      // invalid char
        public void Decode_InvalidInput_ShouldThrowArgumentException(string input)
        {
            var ex = Assert.Throws<ArgumentException>(() => _decoder.Decode(input));

            Assert.Equal(AppConstants.WarningMessage.InvalidInput, ex.Message);
        }

        [Fact]
        public void Decode_SingleKeyPress_ShouldDecodeCorrectly()
        {
            var result = _decoder.Decode("2#"); // 2 = A

            Assert.Equal("A", result);
        }

        [Fact]
        public void Decode_MultipleSameKeyPresses_ShouldCycleLetters()
        {
            var result = _decoder.Decode("222#"); // 222 = C

            Assert.Equal("C", result);
        }

        [Fact]
        public void Decode_KeyPressOverflow_ShouldWrapAround()
        {
            var result = _decoder.Decode("2222#"); // 2222 = A (wrap)

            Assert.Equal("A", result);
        }

        [Fact]
        public void Decode_Pause_ShouldAppendPreviousCharacter()
        {
            var result = _decoder.Decode("2 3#"); // 2 3 = AD

            Assert.Equal("AD", result);
        }

        [Fact]
        public void Decode_PauseAtBeginning_ShouldDoNothing()
        {
            var result = _decoder.Decode(" 2#"); // (space)2 = A

            Assert.Equal("A", result);
        }

        [Fact]
        public void Decode_Backspace_ShouldRemoveLastCharacter()
        {

            var result = _decoder.Decode("2*#"); // 2 = A, * = remove A, becomes Empty

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Decode_Backspace_WhenResultIsEmpty_ShouldNotThrow()
        {
            var result = _decoder.Decode("*#");

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Decode_Backspace_ShouldAppendBeforeRemoving()
        {
            var result = _decoder.Decode("22*#");// 22 = B, * removes B, becomes empty

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Decode_MultipleCharacters_ShouldDecodeCorrectly()
        {
            var result = _decoder.Decode("8 88777444666*664#");

            Assert.Equal("TURING", result);
        }

        [Fact]
        public void Decode_DifferentKeysWithoutPause_ShouldAutoAppend()
        {

            var result = _decoder.Decode("23#"); // 2 = A, 3 = D

            Assert.Equal("AD", result);
        }

        [Fact]
        public void Decode_ZeroKey_ShouldAddSpace()
        {
            var result = _decoder.Decode("0#"); // 0 = space

            Assert.Equal(" ", result);
        }

        [Fact]
        public void Decode_KeyOne_ShouldUseSpecialCharacters()
        {

            var result = _decoder.Decode("11#"); // 11 = ,

            Assert.Equal(",", result);
        }

        [Fact]
        public void Decode_EndCharacter_ShouldAppendLastCharacter()
        {
            var result = _decoder.Decode("777#"); // without pause before #

            Assert.Equal("R", result);
        }

        [Fact]
        public void Decode_EndImmediately_ShouldReturnEmptyString()
        {
            var result = _decoder.Decode("#");

            Assert.Equal(string.Empty, result);
        }
    }
}
