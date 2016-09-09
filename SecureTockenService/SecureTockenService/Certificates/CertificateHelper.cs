using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecureTockenService.Certificates
{
    public static class CertificateHelper
    {
        public static X509Certificate2 GetCertificate(StoreName storeName, StoreLocation storeLocation, string subjectDistinguishedName)
        {
            var store = new X509Store(storeName, storeLocation);
            X509Certificate2Collection certificates = null;
            store.Open(OpenFlags.ReadOnly);

            try
            {
                certificates = store.Certificates;
                var certs = certificates.OfType<X509Certificate2>().Where(x => x.SubjectName.Name.Equals(subjectDistinguishedName, StringComparison.OrdinalIgnoreCase)).ToList();

                if(certs.Count < 1)
                    throw new SecurityException($"No certificate found for subject '{subjectDistinguishedName}'");
                if(certs.Count > 1)
                    throw new SecurityException($"Multiple certificates found for subject '{subjectDistinguishedName}'");

                return new X509Certificate2(certs[0]);
            }
            finally
            {
                if (certificates != null)
                {
                    foreach (X509Certificate2 x509Certificate2 in certificates)
                    {
                        x509Certificate2.Reset();
                    }
                }

                store.Close();
            }
        }
    }
}
