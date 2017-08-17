using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotAuth.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace BotAuth.AADv2
{
    [Serializable]
    public class MSALAuthProvider : IAuthProvider
    {
        public string Name
        {
            get { return "MSALAuthProvider"; }
        }

        public async Task<AuthResult> GetAccessToken(AuthenticationOptions authOptions, IDialogContext context)
        {
            AuthResult authResult;
            string validated = null;
            if (context.UserData.TryGetValue($"{this.Name}{ContextConstants.AuthResultKey}", out authResult) &&
                context.UserData.TryGetValue($"{this.Name}{ContextConstants.MagicNumberValidated}", out validated) &&
                validated == "true")
            {

                try
                {
                    InMemoryTokenCacheMSAL tokenCache = new InMemoryTokenCacheMSAL(authResult.TokenCache);
                    ConfidentialClientApplication client = new ConfidentialClientApplication(authOptions.ClientId, 
                        authOptions.RedirectUrl, new ClientCredential(authOptions.ClientSecret), tokenCache);
                    var result = await client.AcquireTokenSilentAsync(authOptions.Scopes, authResult.UserUniqueId);
                    authResult = result.FromMSALAuthenticationResult(tokenCache);
                    context.StoreAuthResult(authResult, this);
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Failed to renew token: " + ex.Message);
                    await context.PostAsync("Your credentials expired and could not be renewed automatically!");
                    await Logout(authOptions, context);
                    return null;
                }
                return authResult;
            }

            return null;
        }

        public async Task<string> GetAuthUrlAsync(AuthenticationOptions authOptions, string state)
        {
            Uri redirectUri = new Uri(authOptions.RedirectUrl);
            InMemoryTokenCacheMSAL tokenCache = new InMemoryTokenCacheMSAL();
            ConfidentialClientApplication client = new ConfidentialClientApplication(authOptions.ClientId, redirectUri.ToString(),
                new ClientCredential(authOptions.ClientSecret),
                tokenCache);
            var uri = await client.GetAuthorizationRequestUrlAsync(authOptions.Scopes, null, $"state={state}");
            return uri.ToString();
        }

        public async Task<AuthResult> GetTokenByAuthCodeAsync(AuthenticationOptions authOptions, string authorizationCode)
        {
            InMemoryTokenCacheMSAL tokenCache = new InMemoryTokenCacheMSAL();
            ConfidentialClientApplication client = new ConfidentialClientApplication(authOptions.ClientId, authOptions.RedirectUrl,
                new ClientCredential(authOptions.ClientSecret), tokenCache);
            Uri redirectUri = new Uri(authOptions.RedirectUrl);
            var result = await client.AcquireTokenByAuthorizationCodeAsync(authOptions.Scopes, authorizationCode);
            AuthResult authResult = result.FromMSALAuthenticationResult(tokenCache);
            return authResult;
        }

        public async Task Logout(AuthenticationOptions authOptions, IDialogContext context)
        {
            context.UserData.RemoveValue($"{this.Name}{ContextConstants.AuthResultKey}");
            context.UserData.RemoveValue($"{this.Name}{ContextConstants.MagicNumberKey}");
            context.UserData.RemoveValue($"{this.Name}{ContextConstants.MagicNumberValidated}");
            string signoutURl = "https://login.microsoftonline.com/common/oauth2/logout?post_logout_redirect_uri=" + System.Net.WebUtility.UrlEncode(authOptions.RedirectUrl);
            await context.PostAsync($"In order to finish the sign out, please click at this [link]({signoutURl}).");
        }
    }
}
