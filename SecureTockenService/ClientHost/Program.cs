using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SecureTockenService.Client;

namespace ClientHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new TokenClientOptions();
            options.AudienceUri = new Uri("http://localhost");
            options.StoreName = StoreName.Root;
            options.StoreLocation = StoreLocation.LocalMachine;
            options.CertificateDnsIdentity = "SecureTokenServiceTest";
            options.SubjectDistinguishedName = "CN=SecureTokenServiceTest";
            options.TokenServiceUri = new Uri("http://localhost:6690/STS");


            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<TrustedIssuerNameRegistry>();
            unityContainer.RegisterInstance(typeof(ITokenClientOptions), options);
            unityContainer.RegisterType<TokenClient>();

            Console.WriteLine("Login test");

            try
            {
                Console.WriteLine("Valid test");
                var client = unityContainer.Resolve<TokenClient>();
                client.SignIn("Dessel Bane","pass");

                foreach (Claim claimsPrincipalClaim in client.SessionToken.ClaimsPrincipal.Claims)
                {
                    Console.WriteLine($"Claim type: {claimsPrincipalClaim.Type}, Claim Value: {claimsPrincipalClaim.Value}");
                }

                client.SignOut();

                client.SignIn("Dessel Bane", "pas123s");

                Console.WriteLine(client?.SessionToken?.ClaimsPrincipal?.Identity?.Name ?? "Not Authenticated");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }
    }
}
