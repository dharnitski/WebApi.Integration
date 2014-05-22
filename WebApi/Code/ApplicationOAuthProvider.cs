using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace WebApi.Code
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //using (TfUserManager userManager = _userManagerFactory())
            //{
            //    var user = await userManager.FindAsync(context.UserName, context.Password);
            //    if (user == null)
            //    {
            //        context.SetError("invalid_grant", "The user name or password is incorrect.");
            //        return;
            //    }

            //    ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync(user,
            //        context.Options.AuthenticationType);
            //    ClaimsIdentity cookiesIdentity = await userManager.CreateIdentityAsync(user,
            //        CookieAuthenticationDefaults.AuthenticationType);
            //    AuthenticationProperties properties = CreateProperties(user.UserName);
            //    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            //    context.Validated(ticket);
            //    context.Request.Context.Authentication.SignIn(cookiesIdentity);
            //}
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //string clientId;
            //string clientSecret;
            //if (context.TryGetBasicCredentials(out clientId, out clientSecret) ||
            //    context.TryGetFormCredentials(out clientId, out clientSecret))
            //{
            //    if (clientId == _settings.ApiKey && clientSecret == _settings.ApiSecret)
            //    {
            //        context.Validated(context.ClientId);
            //    }
            //    else
            //    {
            //        context.Rejected();
            //    }
            //}
            //else
            //{
            //    context.Rejected();
            //}
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            //if (context.ClientId == _publicClientId)
            //{
            //    Uri expectedRootUri = new Uri(context.Request.Uri, "/");

            //    if (expectedRootUri.AbsoluteUri == context.RedirectUri)
            //    {
            //        context.Validated();
            //    }
            //}

            return Task.FromResult<object>(null);
        }
    }
}