using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Print Test v1.0");
            do
            {
                Console.WriteLine("Please enter path to document or exit to quit.");
                var path = Console.ReadLine();

                if (path?.ToLower() == "exit")
                    return;

                PrintManager.DetectPrinters();

                Console.WriteLine("Select a Printer");
                var printerName = Console.ReadLine();

                PrintManager.Print(path, printerName);

            } while (true);
        }
    }
}
