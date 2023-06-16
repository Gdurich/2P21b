using System.ComponentModel;
using Terminal.Handlers;
using Terminal.Models.TerminalRequests;

CommandHandler commandHandler = new CommandHandler(); 
Init();

while (true)
{
    Console.Write($"_{CommandHandler.CurrentDirectoryPath}>");
    string commandString = Console.ReadLine();
    if (commandString != null && commandString != "")
        commandHandler.ExecuteCommand(commandString);
}

void Init()
{
    commandHandler
        .AddCommandObject(new CD_TerminalRequest())
        .AddCommandObject(new Printbin_TerminalRequest())
        .AddCommandObject(new Sisinfo_TerminalRequest())
        .AddCommandObject(new mkfile_TerminalRequest())
        .AddCommandObject(new mkdir_TerminalRequest())
        .AddCommandObject(new Help_TerminalRequest())
        .AddCommandObject(new Delete())
        .AddCommandObject(new Write())
        .AddCommandObject(new Ccleaner_TerminalRequest())
        .AddCommandObject(new Openweb())
        .AddCommandObject(new Append_TerminalRequest());
}
