using System.Security.Cryptography.X509Certificates;

namespace SecureTockenService.Service
{
    public interface ITokenServiceOptions
    {
        #region Properties

        string CertificateSubject { get; }
        string CertificateIssuer { get; }
        StoreName CertificateStoreName { get; }
        StoreLocation CertificateStoreLocation { get; }

        #endregion
    }
}