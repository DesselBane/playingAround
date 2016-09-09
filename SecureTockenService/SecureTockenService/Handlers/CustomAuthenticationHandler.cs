using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SecureTockenService.Handlers
{
    public class CustomAuthenticationHandler : UserNameSecurityTokenHandler
    {

        #region Overrides of SecurityTokenHandler

        public override bool CanValidateToken => true;


        public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
        {
            var userToken = token as UserNameSecurityToken;

            if(token == null)
                throw new ArgumentNullException(nameof(token));

            if(userToken == null)
                throw new SecurityTokenException("Invalid Token");

            return new ReadOnlyCollection<ClaimsIdentity>(new ClaimsIdentity[] {AuthenticateUser(userToken.UserName,userToken.Password)});

        }

        protected virtual ClaimsIdentity AuthenticateUser(string username, string password)
        {
            //TODO add real Authentication here
            if(password != "pass")
                throw new FaultException("Unable to Authenticate User.");

            var ident = new ClaimsIdentity("CustomAuthentication");
            ident.AddClaim(new Claim(ClaimTypes.Name,username));
            ident.AddClaim(new Claim(ClaimTypes.Email,$"{username}@company.to"));

            return ident;
        }

        #endregion
    }
}
