using Microsoft.Win32;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    internal class Ccleaner_TerminalRequest : TerminalRequest
    {
        public Ccleaner_TerminalRequest()
        {
            CommandName = "ccleaner";
            Description = "{ url + name || name }";
        }
        public override void Execute(CommandHandler handler, string commandBody = "")
        {
            try
            {
                switch (commandBody)
                {
                    case "":
                        Console.WriteLine(Registry.ClassesRoot);
                        Console.WriteLine(Registry.CurrentConfig);
                        Console.WriteLine(Registry.CurrentUser);
                        Console.WriteLine(Registry.Users);
                        Console.WriteLine(Registry.PerformanceData);

                        break;

                    case "-d":
                        Console.WriteLine("Delete not using values");
                        DeleteNotUsingValues(handler);

                        break;

                    default:
                        if (!handler.SetCurrentDirectory(commandBody)) throw new Exception("Directory not exists");

                        break;

                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"Error: {ex.Message}", ConsoleColor.DarkRed);
            }
        }

        private void DeleteNotUsingValues(CommandHandler handler)
        {
            Console.WriteLine("Chose values path :");
            string valuePath = Console.ReadLine();
            RegistryKey valueName = Registry.ClassesRoot.OpenSubKey(@"");
            
            try
            {
                if (valuePath.ToLower() == "classesroot")
                {
                    Console.WriteLine("Enter values path: ");
                    valuePath = Console.ReadLine().ToLower();
                    valuePath = valuePath.Replace("\\\\", "\\");
                    valueName = Registry.ClassesRoot.OpenSubKey(valuePath, true);
                }

                if (valuePath.ToLower() == "currentuser")
                {
                    Console.WriteLine("Enter values path: ");
                    valuePath = Console.ReadLine().ToLower();
                    valuePath = valuePath.Replace("\\\\", "\\");
                    valueName = Registry.CurrentUser.OpenSubKey(valuePath, true);
                }

                if (valuePath.ToLower() == "localmachine")
                {
                    Console.WriteLine("Enter values path: ");
                    valuePath = Console.ReadLine().ToLower();
                    valuePath = valuePath.Replace("\\\\", "\\");
                    valueName = Registry.LocalMachine.OpenSubKey(valuePath, true);
                }

                if (valuePath.ToLower() == "users")
                {
                    Console.WriteLine("Enter values path: ");
                    valuePath = Console.ReadLine().ToLower();
                    valuePath = valuePath.Replace("\\\\", "\\");
                    valueName = Registry.Users.OpenSubKey(valuePath, true);
                }

                
                if (valueName != null) 
                {
                    string[] allkeysNames = valueName.GetValueNames();
                    
                    List<object> allEmptyvalues = new List<object>();
                    
                    foreach (string key in allkeysNames) { if (valueName.GetValue(key) == "") { allEmptyvalues.Add(key); } }

                    Console.WriteLine($"Are you sure you want to delete this values: ");

                    foreach (var item in allEmptyvalues) { Console.WriteLine(item); }

                    Console.Write("?");
                    Console.Write(" (yes/no)");
                    string confirmation = Console.ReadLine();

                    if (confirmation.Trim().ToLower() == "yes")
                    {
                        foreach (string key in allEmptyvalues)
                        {
                            valueName.DeleteValue(key);
                        }
                        valueName.Close();
                    }

                    else
                    {
                        ConsoleHelper.WriteColorLine("Operation canceled.", ConsoleColor.Yellow);
                    }
                }
                else
                {
                    ConsoleHelper.WriteColorLine($"An error occurred while deleting the file", ConsoleColor.DarkRed);
                }
                
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteColorLine($"An error occurred while deleting the file: {ex.Message}", ConsoleColor.DarkRed);
            }
        }
    }
}
