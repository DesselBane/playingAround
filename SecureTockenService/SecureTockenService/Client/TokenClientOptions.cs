using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecureTockenService.Client
{
    public class TokenClientOptions : ITokenClientOptions
    {
        #region Implementation of ITokenClientOptions

        public Uri AudienceUri { get; set; }
        public string SigningCertificateName => SubjectDistinguishedName;
        public string CertificateDnsIdentity { get; set; }
        public Uri TokenServiceUri { get; set; }
        public StoreName StoreName { get; set; }
        public StoreLocation StoreLocation { get; set; }
        public string SubjectDistinguishedName { get; set; }

        #endregion
    }
}
