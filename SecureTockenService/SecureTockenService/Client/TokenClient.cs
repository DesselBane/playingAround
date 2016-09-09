using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Web;
using System.Xml;
using SecureTockenService.Certificates;

namespace SecureTockenService.Client
{
    public class TokenClient
    {
        #region Vars

        private SessionSecurityToken _sessionToken;
        private ITokenClientOptions _tokenClientOptions;
        private TrustedIssuerNameRegistry _nameRegistry;

        #endregion

        #region Properties

        public SessionSecurityToken SessionToken
        {
            get { return _sessionToken; }
            private set { _sessionToken = value; }
        }

        #endregion

        #region Constructors

        public TokenClient(ITokenClientOptions options, TrustedIssuerNameRegistry nameRegistry)
        {
            _tokenClientOptions = options;
            _nameRegistry = nameRegistry;
        }

        #endregion

        public void SignIn(string username, string password)
        {
            ValidateConfiguration();
            GenericXmlSecurityToken genericToken = RequestTrustToken(username, password, _tokenClientOptions.AudienceUri);

            ClaimsPrincipal tokenClaimsPrincipal = ParseToken(genericToken);

            SessionToken = new SessionSecurityToken(tokenClaimsPrincipal);

        }

        private ClaimsPrincipal ParseToken(GenericXmlSecurityToken genericToken)
        {
            using (XmlReader samlReader = XmlReader.Create(new StringReader(genericToken.TokenXml.OuterXml)))
            {
                SecurityTokenHandlerCollection tokenHandlers = SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();

                SecurityTokenHandlerConfiguration config = tokenHandlers.Configuration;
                var securityTokens = new List<SecurityToken>()
                {
                    new X509SecurityToken(CertificateHelper.GetCertificate(_tokenClientOptions.StoreName,_tokenClientOptions.StoreLocation,_tokenClientOptions.SubjectDistinguishedName))
                };

                config.ServiceTokenResolver = SecurityTokenResolver.CreateDefaultSecurityTokenResolver(securityTokens.AsReadOnly(), false);
                config.CertificateValidator = X509CertificateValidator.PeerOrChainTrust;
                
                config.IssuerTokenResolver = new X509CertificateStoreTokenResolver(_tokenClientOptions.StoreName,_tokenClientOptions.StoreLocation);
                config.IssuerNameRegistry = _nameRegistry;

                config.AudienceRestriction.AllowedAudienceUris.Add(_tokenClientOptions.AudienceUri);
                SecurityToken samlToken = tokenHandlers.ReadToken(samlReader);

                ClaimsIdentity tokenIdentity = tokenHandlers.ValidateToken(samlToken).FirstOrDefault();
                return new ClaimsPrincipal(tokenIdentity);
            }
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(_tokenClientOptions?.AudienceUri))
                throw new ArgumentNullException(nameof(ITokenClientOptions.AudienceUri));
            if (string.IsNullOrWhiteSpace(_tokenClientOptions?.SigningCertificateName))
                throw new ArgumentNullException(nameof(ITokenClientOptions.SigningCertificateName));
        }

        private GenericXmlSecurityToken RequestTrustToken(string userName, string password, Uri appliesToUrl)
        {
            WSTrustChannel channel = null;

            try
            {
                channel = CreateWSTrustChannel(userName, password);

                var request = new RequestSecurityToken(RequestTypes.Issue) {AppliesTo = new EndpointReference(appliesToUrl.OriginalString)};
                RequestSecurityTokenResponse response;
                return channel.Issue(request, out response) as GenericXmlSecurityToken;
            }
            catch
            {
                if ((channel != null) && (channel.State == CommunicationState.Faulted))
                    channel.Abort();
                throw;
            }
            finally
            {
                if ((channel != null) && (channel.State != CommunicationState.Faulted))
                    channel.Close();
            }
        }

        private WSTrustChannel CreateWSTrustChannel(string username, string password)
        {
            var binding = new WS2007HttpBinding(SecurityMode.Message);
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            binding.Security.Message.NegotiateServiceCredential = true;
            binding.Security.Message.EstablishSecurityContext = false;

            var timeSpan = new TimeSpan(0, 5, 0);
            binding.SendTimeout = timeSpan;
            binding.ReceiveTimeout = timeSpan;
            binding.OpenTimeout = timeSpan;
            binding.CloseTimeout = timeSpan;

            EndpointIdentity identity = EndpointIdentity.CreateDnsIdentity(_tokenClientOptions.CertificateDnsIdentity);
            var address = new EndpointAddress(_tokenClientOptions.TokenServiceUri, identity);

            var factory = new WSTrustChannelFactory(binding, address);
            
            if(factory.Credentials == null)
                throw new NullReferenceException(nameof(WSTrustChannelFactory.Credentials));

            factory.TrustVersion = TrustVersion.WSTrust13;
            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;
            factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            factory.Credentials.SupportInteractive = false;
            factory.Credentials.UserName.UserName = username;
            factory.Credentials.UserName.Password = password;

            return (WSTrustChannel) factory.CreateChannel();
        }
    }
}