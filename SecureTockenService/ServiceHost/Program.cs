using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SecureTockenService.Service;

namespace ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Config Options
            var options = new TokenServiceOptions();
            options.CertificateStoreName = StoreName.Root;
            options.CertificateStoreLocation = StoreLocation.LocalMachine;
            options.CertificateIssuer = "CN=SecureTokenServiceTest";
            options.CertificateSubject = "CN=SecureTokenServiceTest";

            //Unity Config
            var unitContainer = new UnityContainer();
            unitContainer.RegisterType<TokenServiceFactory>(new ContainerControlledLifetimeManager());
            unitContainer.RegisterInstance(typeof(ITokenServiceOptions), options);

            try
            {
                Console.WriteLine("Resolving Factory");

                var serviceHost = unitContainer.Resolve<TokenServiceFactory>().CreateServiceHost("http://localhost:6690/STS");

                Console.WriteLine("Opening Host now...");


                serviceHost.Open(TimeSpan.FromMinutes(2));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }
}
