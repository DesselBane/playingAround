using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Threading;
using Ghostscript.NET.Processor;

namespace PrintTest
{
    public static class PrintManager
    {
        public static void Print(string printDocPath, string printerName,bool showWindow = false)
        {
            using (var processor = new GhostscriptProcessor())
            {
                List<string> switches = new List<string>();
                switches.Add("-empty");
                switches.Add("-dPrinted");
                switches.Add("-dBATCH");
                switches.Add("-dNOPAUSE");
                switches.Add("-dNOSAFER");
                switches.Add("-dNumCopies=1");
                switches.Add("-sDEVICE=mswinpr2");
                switches.Add("-sOutputFile=%printer%" + printerName);
                switches.Add("-f");
                switches.Add(printDocPath);

                processor.StartProcessing(switches.ToArray(), null);
            }

        }

        public static void DetectPrinters()
        {
            var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");
            foreach (var printer in printerQuery.Get())
            {
                var name = printer.GetPropertyValue("Name");
                var status = printer.GetPropertyValue("Status");
                var isDefault = printer.GetPropertyValue("Default");
                var isNetworkPrinter = printer.GetPropertyValue("Network");

                Console.WriteLine("{0} (Status: {1}, Default: {2}, Network: {3}",
                            name, status, isDefault, isNetworkPrinter);
            }
        }
    }
}