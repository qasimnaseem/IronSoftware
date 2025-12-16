namespace IronSoftware.Utilities
{
    public static class AppConstants
    {
        public static class WarningMessage
        {
            public const string InvalidInput = "Invalid input format. Please use numbers, spaces for pauses, * for backspace, and finish with #";
            // More warning msg consts here
        }

        public static class ErrorMessage
        {
            public const string BadHappened = "Something went wrong while decoding. Please try again.";
            // More error msg consts here
        }

        public static class PromptMessage
        {
            public const string EnterKeypadInput = "Enter keypad input to decode (or enter X to exit): ";
            // More prompt message consts here
        }

        public static class InfoMessage
        {
            public const string DecodedResult = "Decoded Result:";
            // More info msg consts here
        }
    }
}
