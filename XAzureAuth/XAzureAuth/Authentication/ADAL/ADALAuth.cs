using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using UIKit;

namespace XAzureAuth.Authentication
{
    public static class ADALAuth
    {
        const string APP_ID = "a107980b-41ff-4438-8b71-c86e03e7cd6d";
        const string TENANT_ID = "UCB.onmicrosoft.com";
        const string RETURN_URI = "http://UConnectApp/";

        const string AZURE_GRAPH = "https://graph.windows.net";
        const string AZURE_PROFILE_ENDPOINT = "me?api-version=1.6";
        const string AZURE_USERS_ENDPOINT = "users?api-version=1.6";

		static string GetAzureGraphAuthorityUrl()
		{
			return string.Format("https://login.windows.net/{0}", TENANT_ID);
		}

		static string GetAzureGraphTenantUrl()
		{
            return string.Format("https://graph.windows.net/{0}/", TENANT_ID);
		}

        static string GetAzureGraphProfileUrl()
        {
            return string.Format("{0}{1}", GetAzureGraphTenantUrl(), AZURE_PROFILE_ENDPOINT);
        }

		static string GetAzureGraphUsersUrl()
		{
			return string.Format("{0}{1}", GetAzureGraphTenantUrl(), AZURE_USERS_ENDPOINT);
		}

		public static bool HasExistingAuthentication()
		{
            var authority = GetAzureGraphAuthorityUrl();
			var authContext = new AuthenticationContext(authority);
			return authContext.TokenCache.ReadItems().Any();
		}

		public static async Task<AuthenticationResult> GetToken(UIViewController vc)
		{
            if (vc == null) return null;

            var authority = GetAzureGraphAuthorityUrl();
            var returnUri = new Uri(RETURN_URI);
			var authContext = new AuthenticationContext(authority);
			var platformParams = new PlatformParameters(vc);
			try
			{
                var authResult = await authContext.AcquireTokenAsync(AZURE_GRAPH,
                                                                     APP_ID,
																	 returnUri,
																	 platformParams);
                PrintObjectAsJSON(authResult);
				return authResult;
			}
			catch (AdalException ex)
			{
				Console.WriteLine("Azure AD Authentication error : {0}", ex.Message);
				return null;
			}
		}

        public static async Task<ADALUser> GetProfile(AuthenticationResult authResult)
		{
            var profileUrl = GetAzureGraphProfileUrl();
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, profileUrl);
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
			var httpResponse = await client.SendAsync(request);
			try
			{
				if (httpResponse.Content != null)
				{
                    
					var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<ADALUser>(responseContent);
                    PrintObjectAsJSON(response);
					return response;
				}
				else { return null; }
			}
			catch (Exception ex)
			{
				Console.WriteLine("Azure profile error : {0}", ex.Message);
				return null;
			}
		}

		public static async Task<string> GetUsers(AuthenticationResult authResult)
		{
            var usersUrl = GetAzureGraphUsersUrl();
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, usersUrl);
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
			var httpResponse = await client.SendAsync(request);
			try
			{
				if (httpResponse.Content != null)
				{
                    PrintObjectAsJSON(httpResponse.Content);
					var responseContent = await httpResponse.Content.ReadAsStringAsync();
					return responseContent;
				}
				else { return null; }
			}
			catch (Exception ex)
			{
				Console.WriteLine("Azure users error : {0}", ex.Message);
				return null;
			}
		}

		public static void Logout()
		{
            var authority = GetAzureGraphAuthorityUrl();
			var authContext = new AuthenticationContext(authority);
			authContext.TokenCache.Clear();
		}

		public static void PrintObjectAsJSON(object obj)
		{
			var jsonObject = JsonConvert.SerializeObject(obj, Formatting.Indented);
			Console.WriteLine(jsonObject);
		}
    }
}
