using System;
using System.Diagnostics;
using System.IO;
using Terminal.Handlers;
using Terminal.Helpers;
using Terminal.Models.TerminalRequests.Base;

namespace Terminal.Models.TerminalRequests
{
    public class Openweb : TerminalRequest
    {
        public Openweb()
        {
            CommandName = "openweb";
        }

        public  override void Execute(string[] arguments)
        {
            if (arguments.Length < 1)
            {
                Console.WriteLine("Please provide a URL.");
                return;
            }

            string url = arguments[0];

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                Console.WriteLine("Invalid URL. The URL must start with 'http://' or 'https://'.");
                return;
            }

            try
            {
                Process.Start(url);
                Console.WriteLine($"Opening URL: {url}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while opening the web page: {ex.Message}");
            }
        }
    }
}
