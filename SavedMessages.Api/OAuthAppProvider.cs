
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SavedMessages.Api
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var userRepository = new UserRepository();
            var User = userRepository.Get(context.UserName, context.Password);

            if (User == null)
            {
                context.SetError("invalie_grant", "Wrong user");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            if (User.PermissionId == 1)
                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
            else
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

            context.Validated(identity);
        }
    }
}