using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecureTockenService.Service
{
    public interface ITokenServiceOptions
    {
        string CertificateName { get; }
        StoreName CertificateStoreName { get; }
        StoreLocation CertificateStoreLocation { get; }
    }
}
