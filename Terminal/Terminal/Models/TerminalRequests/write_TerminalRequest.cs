using System;
using System.IO;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;
namespace Terminal.Models.TerminalRequests
{
    public class Write : TerminalRequest
    {
        public Write()
        {
            CommandName = "write";
            Description = "{url + name || name} ----> editor open to write";
        }
        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            try
            {
                if (commandBody.StartsWith("-"))
                {
                    char key = commandBody[1];

                    switch (key)
                    {
                        case 'r':
                            Console.WriteLine("Write (Replace)");
                            WriteToFile(handler, FileMode.Create);
                            break;
                        case 'a':
                            Console.WriteLine("Write (Append)");
                            WriteToFile(handler, FileMode.Append);
                            break;
                        default:
                            throw new Exception("Invalid command body");
                    }
                }
                else
                {
                    Console.WriteLine($"Executed: {CommandName} {commandBody}");
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
        private void WriteToFile(CommandHandler handler, FileMode fileMode)
        {
            Console.WriteLine("Enter file path:");
            string filePath = Console.ReadLine();
            try
            {
                using (FileStream fileStream = new FileStream(filePath, fileMode))
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    Console.WriteLine("Enter text (press ESC to finish):");

                    ConsoleKeyInfo key;
                    do
                    {
                        key = Console.ReadKey();

                        if (key.Key != ConsoleKey.Escape)
                        {
                            writer.Write(key.KeyChar);
                        }
                    }
                    while (key.Key != ConsoleKey.Escape);

                    Console.WriteLine();
                    ConsoleHelper.WriteColorLine("File writing completed.", ConsoleColor.Green);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }
}