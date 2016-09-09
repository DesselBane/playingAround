using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Configuration;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.SecurityTokenService;

namespace SecureTockenService.Service
{
    public class TokenService : SecurityTokenService
    {
        public TokenService(SecurityTokenServiceConfiguration securityTokenServiceConfiguration)
            : base(securityTokenServiceConfiguration)
        {
        }

        #region Overrides of SecurityTokenService

        protected override Scope GetScope(IClaimsPrincipal principal, RequestSecurityToken request)
        {
            if(principal == null)
                throw new ArgumentNullException(nameof(principal),"principal cannot be null.");
            if(request == null)
                throw new ArgumentNullException(nameof(request),"request cannot be null");

            var scope = new Scope(request.AppliesTo.Uri.OriginalString,SecurityTokenServiceConfiguration.SigningCredentials);



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

            var authenticatedIdentity = principal.Identity as ClaimsIdentity;

            if(authenticatedIdentity == null)
                throw new ArgumentNullException(nameof(ClaimsPrincipal.Identity),"principal.Identity must be a ClaimsIdentity.");

            return authenticatedIdentity;
        }

        #endregion
    }
}
