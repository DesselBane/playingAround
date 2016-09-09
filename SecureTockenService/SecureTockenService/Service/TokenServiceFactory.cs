using System;
using System.IdentityModel.Configuration;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Security;
using Microsoft.Practices.Unity;
using SecureTockenService.Certificates;
using SecureTockenService.Handlers;
using SecureTockenService.Unity;

namespace SecureTockenService.Service
{
    public class TokenServiceFactory : ServiceHostFactory
    {
        #region Vars

        private ITokenServiceOptions _tokenServiceOptions;
        private IUnityContainer _unityContainer;

        #endregion

        #region Constructors

        public TokenServiceFactory(ITokenServiceOptions tokenServiceOptions, IUnityContainer unityContainer)
        {
            _tokenServiceOptions = tokenServiceOptions;
            _unityContainer = unityContainer ?? new UnityContainer();
        }

        #endregion

        private SecurityTokenServiceConfiguration GetSecureTokenServiceConfiguration()
        {
            var signingCredentials = new X509SigningCredentials(CertificateHelper.GetCertificate(_tokenServiceOptions.CertificateStoreName, _tokenServiceOptions.CertificateStoreLocation, _tokenServiceOptions.CertificateSubject));
            var config = new SecurityTokenServiceConfiguration(_tokenServiceOptions.CertificateIssuer, signingCredentials);
            config.SecurityTokenHandlers.AddOrReplace(_unityContainer.Resolve<CustomAuthenticationHandler>());
            config.SecurityTokenService = typeof(TokenService);
            config.CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;
            return config;
        }

        public ServiceHost CreateServiceHost(string hostUrl)
        {
            return CreateServiceHost(typeof(TokenService), new[] {new Uri(hostUrl)});
        }

        #region Overrides of ServiceHostFactory

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            SecurityTokenServiceConfiguration config = GetSecureTokenServiceConfiguration();

            var host = new UnityWSTrustServiceHost(_unityContainer, config, baseAddresses);
            return host;
        }

        #endregion
    }
}