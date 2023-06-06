using System.ComponentModel;
using System.Runtime.CompilerServices;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class mkfile_TerminalRequest : TerminalRequest
    {
        public mkfile_TerminalRequest()
        {
            CommandName = "mkfile";
            Description = "{filename}";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            commandBody = commandBody.Replace("\\\\", "\\");

            try
            {
                string fileName = Path.Combine(CommandHandler.CurrentDirectoryPath, commandBody);
                File.Create(fileName);
                Console.WriteLine($"File '{fileName}' created successfully.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error creating file: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }
}
