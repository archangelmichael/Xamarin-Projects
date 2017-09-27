using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace MicrosoftGraph.iOS.Helpers
{
    public class Authentication
    {
		public static string TokenForUser = null;
		public static DateTimeOffset expiration;
		private static GraphServiceClient graphClient = null;

        // Get an access token for the given context and resourceId. An attempt is first made to 
        // acquire the token silently. If that fails, then we try to acquire the token by prompting the user.
        public static GraphServiceClient GetAuthenticatedClient()
        {
            if (graphClient == null)
            {
                try
                {
                    graphClient = new GraphServiceClient(
                        "https://graph.microsoft.com/v1.0",
                        new DelegateAuthenticationProvider(
                            async (requestMessage) =>
                            {
                                var token = await GetTokenForUserAsync();
                                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                            }));
                    return graphClient;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Could not create a graph client: " + ex.Message);
                }
            }

            return graphClient;
        }

		public static async Task<string> GetTokenForUserAsync()
		{
			if (TokenForUser == null || expiration <= DateTimeOffset.UtcNow.AddMinutes(5))
			{
                AuthenticationResult authResult = await AppDelegate.IdentityClientApp.AcquireTokenAsync(AppDelegate.Scopes, AppDelegate.UiParent).ConfigureAwait(false);
				TokenForUser = authResult.AccessToken;
				expiration = authResult.ExpiresOn;
			}

			return TokenForUser;
		}

		public static async Task<User> GetUserAsync()
		{
            var client = GetAuthenticatedClient();
            if (client != null)
            {
                var user = await client.Me.Request().GetAsync();
                return user;
            }
            else 
            {
                return null;
            }
		}

		public static void SignOut()
		{
			foreach (var user in AppDelegate.IdentityClientApp.Users)
			{
				AppDelegate.IdentityClientApp.Remove(user);
			}
			graphClient = null;
			TokenForUser = null;
		}
    }
}
