using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceCore.Contracts;

namespace ClientHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var factory = new ChannelFactory<IEchoService>("EchoService"))
            {
                IEchoService channel = null;

                try
                {
                    channel = factory.CreateChannel();
                    Console.WriteLine(channel.Echo("test"));
                    Console.WriteLine(channel.Echo(null));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"Channel state: {((IClientChannel)channel).State}");
                }

                Console.WriteLine(channel.Echo("Still Active"));
                Console.ReadKey();
            }

        }
    }
}
