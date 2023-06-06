using Terminal.Handlers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class Help_TerminalRequest : TerminalRequest
    {
        public Help_TerminalRequest()
        {
            CommandName = "help";
            Description = "{Information about commands}";
        }

        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            Console.WriteLine("Available commands:");
            foreach (TerminalRequest terminalRequest in handler.GetCommandList())
            {
                Console.WriteLine($"{terminalRequest.CommandName}: {terminalRequest.Description}");
            }
        }
    }
}
