using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecureTockenService.Client
{
    public interface ITokenClientOptions
    {
        Uri AudienceUri { get; }
        string SigningCertificateName { get; }
        string CertificateDnsIdentity { get; }
        Uri TokenServiceUri { get; }
        StoreName StoreName { get; }
        StoreLocation StoreLocation { get; }
        string SubjectDistinguishedName { get; }
    }
}
