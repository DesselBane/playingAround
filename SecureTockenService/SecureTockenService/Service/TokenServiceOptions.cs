using System.Security.Cryptography.X509Certificates;

namespace SecureTockenService.Service
{
    public class TokenServiceOptions : ITokenServiceOptions
    {
        #region Properties

        public string CertificateSubject { get; set; }

        public string CertificateIssuer { get; set; }

        public StoreName CertificateStoreName { get; set; }
        public StoreLocation CertificateStoreLocation { get; set; }

        #endregion
    }
}