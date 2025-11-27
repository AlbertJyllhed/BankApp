namespace BankApp
{
    internal static class InputUtilities
    {
        //Method to get string input from user
        internal static string GetString()
        {
            string? input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                PrintUtilities.PrintError("Inmatning kan inte vara tomt, försök igen.");
                input = Console.ReadLine();
            }
            return input;
        }

        //Method to get decimal input from the user
        internal static decimal GetDecimal()
        {
            decimal input;
            while (!decimal.TryParse(Console.ReadLine(), out input))
            {
                PrintUtilities.PrintError("Felaktigt värde, försök igen.");
            }
            return Math.Round(input, 2);
        }

        //Method to get positive decimal input from the user
        internal static decimal GetPositiveDecimal()
        {
            decimal input = GetDecimal();
            while (input <= 0)
            {
                input = GetDecimal();
                PrintUtilities.PrintError("Felaktigt värde, värdet måste vara positivt.");
            }
            return input;
        }

        //Method to get int input from the user
        internal static int GetInt()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                PrintUtilities.PrintError("Felaktigt värde, försök igen.");
            }
            return input;

        }

        //Method to get int input within a specific range
        internal static int GetIndex(int maxIndex)
        {
            int index = GetInt() - 1;
            while (index < 0 || index >= maxIndex)
            {
                PrintUtilities.PrintError("Felaktigt index värde, försök igen.");
                index = GetInt() - 1;
            }
            return index;
        }

        internal static bool GetYesOrNo()
        {
            string input = GetString().ToLower();
            while (input != "y" && input != "n")
            {
                PrintUtilities.PrintError("Felaktigt inmatning, vänligen skriv 'y' för ja eller 'n' för nej.");
                input = GetString().ToLower();
            }
            return input == "y";
        }
    }
}
