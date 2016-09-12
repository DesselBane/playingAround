using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCore.Service;

namespace ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new System.ServiceModel.ServiceHost(typeof(EchoService)))
            {
                try
                {
                    Console.WriteLine("Opening Host");
                    host.Open();
                    Console.WriteLine("Host Open");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                 Console.WriteLine(e);   
                }
            }
        }
    }
}
