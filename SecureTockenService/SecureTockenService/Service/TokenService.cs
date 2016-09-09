using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Configuration;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.SecurityTokenService;
using SecureTockenService.Certificates;

namespace SecureTockenService.Service
{
    public class TokenService : SecurityTokenService
    {
        #region Vars

        private ITokenServiceOptions _tokenServiceOptions;

        #endregion

        #region Constructors

        public TokenService(SecurityTokenServiceConfiguration securityTokenServiceConfiguration, ITokenServiceOptions options)
            : base(securityTokenServiceConfiguration)
        {
            _tokenServiceOptions = options;
        }

        #endregion

        #region Overrides of SecurityTokenService

        protected override Scope GetScope(IClaimsPrincipal principal, RequestSecurityToken request)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal), "principal cannot be null.");
            if (request == null)
                throw new ArgumentNullException(nameof(request), "request cannot be null");

            //Creating new Token Scope
            var scope = new Scope(request.AppliesTo.Uri.OriginalString, SecurityTokenServiceConfiguration.SigningCredentials);

            //Getting the Certificate from the Certificate Store
            //which certificate to get is configured by the TokenServiceOptions
            X509Certificate2 encryptingCertificate = CertificateHelper.GetCertificate(_tokenServiceOptions.CertificateStoreName, _tokenServiceOptions.CertificateStoreLocation, _tokenServiceOptions.CertificateSubject);
            var encryptingCredentials = new X509EncryptingCredentials(encryptingCertificate);

            //Setting the encryption credentials for our Scope
            scope.EncryptingCredentials = encryptingCredentials;

            //Enable encryption (true by default)
            scope.TokenEncryptionRequired = true;
            scope.SymmetricKeyEncryptionRequired = true;
            scope.ReplyToAddress = request.ReplyTo;

            return scope;
        }

        protected override IClaimsIdentity GetOutputClaimsIdentity(IClaimsPrincipal principal, RequestSecurityToken request, Scope scope)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal), "'principal' cannot be null.");
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "'request' cannot be null.");
            }

            if (scope == null)
            {
                throw new ArgumentNullException(nameof(scope), "'scope' cannot be null.");
            }

            // Retreiving ClaimsIdentity from Pricipal
            var authenticatedIdentity = principal.Identity as ClaimsIdentity;

            if (authenticatedIdentity == null)
                throw new ArgumentNullException(nameof(ClaimsPrincipal.Identity), "principal.Identity must be a ClaimsIdentity.");

            return authenticatedIdentity;
        }

        #endregion
    }
}