using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Handlers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class Sisinfo_TerminalRequest : TerminalRequest
    {
        public Sisinfo_TerminalRequest()
        {
            CommandName = "sisinfo";
        }
        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            Console.WriteLine("System Information:");
            Console.WriteLine("===================");

            Console.WriteLine($"Operating System: {Environment.OSVersion}");
            Console.WriteLine($"Processor Count: {Environment.ProcessorCount}");
            Console.WriteLine($"System Directory: {Environment.SystemDirectory}");
            Console.WriteLine($"Current Directory: {Environment.CurrentDirectory}");
            Console.WriteLine($"Username: {Environment.UserName}");
        }
    }
}
