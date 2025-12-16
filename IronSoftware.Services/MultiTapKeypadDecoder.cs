using IronSoftware.Interfaces;
using IronSoftware.Utilities;
using System.Text;

namespace IronSoftware.Services
{
    public sealed class MultiTapKeypadDecoder : IKeypadInputDecoder
    {
        /// <summary>
        /// Translates a sequence of old mobile phone keypad inputs into a readable text message.
        /// </summary>
        /// <param name="input">
        /// The string representing key presses (digits, spaces for pauses, '*' for backspace, and ending with '#').
        /// </param>
        /// <returns>
        /// A decoded text message corresponding to the keypad input.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the input is null, empty, invalid, or does not end with a '#' character.
        /// </exception>
        /// <remarks>
        /// This method simulates the multi-tap text entry system used in early mobile phones (e.g., Nokia 3310).
        /// </remarks>
        public string Decode(string input)
        {
            if (!IsValidInput(input))
            {
                throw new ArgumentException(AppConstants.WarningMessage.InvalidInput);
            }

            var keys = new Dictionary<char, string>
            {
                { '0', " " },
                { '1', "&,(" },
                { '2', "ABC" },
                { '3', "DEF" },
                { '4', "GHI" },
                { '5', "JKL" },
                { '6', "MNO" },
                { '7', "PQRS" },
                { '8', "TUV" },
                { '9', "WXYZ" }
            };

            var result = new StringBuilder();
            var lastKey = '\0';
            var count = 0;

            void AppendCurrentCharacter()
            {
                if (lastKey == '\0' || !keys.TryGetValue(lastKey, out string? letters))
                {
                    return;
                }

                int index = count - 1;

                // Wrap around if key presses exceed the number of available letters
                if (index >= letters.Length)
                {
                    index -= letters.Length;
                }

                result.Append(letters[index]);

                lastKey = '\0';
                count = 0;
            }

            foreach (char c in input)
            {
                bool isEnd = c == '#';
                bool isPause = c == ' ';
                bool isBackspace = c == '*';

                if (isEnd)
                {
                    AppendCurrentCharacter();
                    break;
                }
                else if (isPause || isBackspace)
                {
                    AppendCurrentCharacter();

                    // Removes the previously entered character
                    if (isBackspace && result.Length > 0)
                    {
                        result.Length--;
                    }
                }
                else if (c == lastKey)
                {
                    count++;
                }
                else
                {
                    AppendCurrentCharacter();
                    lastKey = c;
                    count = 1;
                }
            }

            return result.ToString();
        }

        private bool IsAllowedChar(char c) => char.IsDigit(c) || c == ' ' || c == '*';

        private bool IsValidInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || !input.EndsWith('#'))
                return false;

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (!IsAllowedChar(input[i]))
                    return false;
            }

            return true;
        }
    }
}
