using IronSoftware.Interfaces;
using IronSoftware.Services;
using IronSoftware.Utilities;

internal class Program
{
    private static void Main(string[] args)
    {
        IKeypadInputDecoder decoder = new MultiTapKeypadDecoder();

        while (true)
        {
            Console.Write(AppConstants.PromptMessage.EnterKeypadInput);
            var input = Console.ReadLine();

            if (string.Equals(input, "x", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            try
            {
                var result = decoder.Decode(input);
                Console.WriteLine($"{AppConstants.InfoMessage.DecodedResult} {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                // TODO: log exception
                Console.WriteLine(AppConstants.ErrorMessage.BadHappened);
            }

            Console.WriteLine();
        }
    }
}