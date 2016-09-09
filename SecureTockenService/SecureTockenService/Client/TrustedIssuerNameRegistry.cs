using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureTockenService.Client
{
    public class TrustedIssuerNameRegistry : IssuerNameRegistry
    {
        private ITokenClientOptions _tokenClientOptions;

        public TrustedIssuerNameRegistry(ITokenClientOptions tokenClientOptions)
        {
            _tokenClientOptions = tokenClientOptions;
        }

        #region Overrides of IssuerNameRegistry

        public override string GetIssuerName(SecurityToken securityToken)
        {
            using (var x509Token = (X509SecurityToken)securityToken)
            {
                string name = x509Token.Certificate.SubjectName.Name;
                string trustedIssuer = _tokenClientOptions.SubjectDistinguishedName;

                return name == trustedIssuer ? name : "";
            }
        }

        #endregion
    }
}
