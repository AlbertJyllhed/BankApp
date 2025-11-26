namespace BankApp
{
    internal static class PrintUtilities
    {
        // Method to print a single message
        internal static void PrintMessage(string message)
        {
            Console.WriteLine(message);
            PrintEmptyLine();
        }

        // Method to print a colored message
        internal static void PrintColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            PrintEmptyLine();
        }

        // Method to print an input prompt
        internal static void PrintInputPrompt(string prompt)
        {
            Console.Write(prompt);
            PrintEmptyLine();
        }

        // Method to print a list of messages
        internal static void PrintMessages(string[] messages)
        {
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
            PrintEmptyLine();
        }

        // Method to print error messages in red color
        internal static void PrintError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
        }

        // Method to print a separator line
        internal static void PrintLine()
        {
            Console.WriteLine("--------------------------------------------------");
        }

        // Method to print an empty line
        internal static void PrintEmptyLine()
        {
            Console.WriteLine();
        }

        // Method to print reset message and wait for user input
        internal static void PrintResetMessage()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        // Method to print the bank logo
        internal static void PrintLogo()
        {
            Console.WriteLine("███████████████████████████████████████████████████████████████████████" +
                "███████████████████████████████████████\r\n█▌                                        " +
                "                                                                  ▐█\r\n█▌   $$\\    " +
                "   $$\\           $$\\                                 $$$$$$$\\                     " +
                " $$\\          ▐█\r\n█▌   $$ |      \\__|          $$ |                              " +
                "  $$  __$$\\                     $$ |         ▐█\r\n█▌   $$ |      $$\\  $$$$$$$\\ $$" +
                " |  $$\\  $$$$$$\\ $$\\    $$\\       $$ |  $$ | $$$$$$\\  $$$$$$$\\  $$ |  $$\\    ▐" +
                "█\r\n█▌   $$ |      $$ |$$  _____|$$ | $$  |$$  __$$\\\\$$\\  $$  |      $$$$$$$\\ | " +
                "\\____$$\\ $$  __$$\\ $$ | $$  |   ▐█\r\n█▌   $$ |      $$ |\\$$$$$$\\  $$$$$$  / $$ " +
                "/  $$ |\\$$\\$$  /       $$  __$$\\  $$$$$$$ |$$ |  $$ |$$$$$$  /    ▐█\r\n█▌   $$ | " +
                "     $$ | \\____$$\\ $$  _$$<  $$ |  $$ | \\$$$  /        $$ |  $$ |$$  __$$ |$$ |  $" +
                "$ |$$  _$$<     ▐█\r\n█▌   $$$$$$$$\\ $$ |$$$$$$$  |$$ | \\$$\\ \\$$$$$$  |  \\$  /  " +
                "       $$$$$$$  |\\$$$$$$$ |$$ |  $$ |$$ | \\$$\\    ▐█\r\n█▌   \\________|\\__|\\___" +
                "____/ \\__|  \\__| \\______/    \\_/          \\_______/  \\_______|\\__|  \\__|\\__|" +
                "  \\__|   ▐█\r\n█▌                                                                   " +
                "                                       ▐█\r\n████████████████████████████████████████" +
                "██████████████████████████████████████████████████████████████████████\n");
        }
    }
}
