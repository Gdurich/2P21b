using System;
using System.Diagnostics;
using System.IO;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    public class Append_TerminalRequest : TerminalRequest
    {
        public Append_TerminalRequest()
        {
            CommandName = "append";
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
                        case 'b':
                            Console.WriteLine("Edit file at the beginning");
                            EditFile(handler, EditPosition.Beginning);
                            break;
                        case 'c':
                            Console.WriteLine("Edit file at the center");
                            EditFile(handler, EditPosition.Center);
                            break;
                        case 'e':
                            Console.WriteLine("Edit file at the end");
                            EditFile(handler, EditPosition.End);
                            break;
                        case 'r':
                            Console.WriteLine("Replace content in the file");
                            EditFile(handler, EditPosition.Replace);
                            break;
                        case 'd':
                            Console.WriteLine("Delete content from the file");
                            EditFile(handler, EditPosition.Delete);
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

        private void EditFile(CommandHandler handler, EditPosition position)
        {
            Console.WriteLine("Enter file path:");
            string filePath = Console.ReadLine();
            try
            {
                string fileContent = File.ReadAllText(filePath);
                Console.WriteLine($"File content: {fileContent}");
                string content = string.Empty;
                switch (position)
                {
                    case EditPosition.Beginning:
                    case EditPosition.Center:
                    case EditPosition.End:
                        Console.WriteLine("Enter content to append:");
                        content = Console.ReadLine();
                        break;
                    case EditPosition.Replace:
                        Console.WriteLine("Enter the text to replace:");
                        string replaceText = Console.ReadLine();
                        Console.WriteLine("Enter the new text:");
                        string newText = Console.ReadLine();
                        fileContent = fileContent.Replace(replaceText, newText);
                        break;
                    case EditPosition.Delete:
                        Console.WriteLine("Enter the text to delete:");
                        string deleteText = Console.ReadLine();
                        fileContent = fileContent.Replace(deleteText, string.Empty);
                        break;
                }
                if (position != EditPosition.Replace && position != EditPosition.Delete)
                {
                    switch (position)
                    {
                        case EditPosition.Beginning:
                            fileContent = content + fileContent;
                            break;
                        case EditPosition.Center:
                            int insertIndex = fileContent.Length / 2;
                            fileContent = fileContent.Insert(insertIndex, content);
                            break;
                        case EditPosition.End:
                            fileContent = fileContent + content;
                            break;
                    }
                }
                File.WriteAllText(filePath, fileContent);
                Console.WriteLine($"Content successfully edited in file '{filePath}' at {position}.");

                // Check if the file path is not empty
                if (!string.IsNullOrEmpty(filePath))
                {
                    // Open the file in the default associated application
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error editing file: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }

    public enum EditPosition
    {
        Beginning,
        Center,
        End,
        Replace,
        Delete
    }
}
