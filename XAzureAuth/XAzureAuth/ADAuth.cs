using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;

namespace XAzureAuth
{
	public static class ADAuth
	{
		public static async Task<AuthenticationResult> Authenticate(string authority, 
		                                                            string resource, 
		                                                            string clientId,
		                                                            string returnUri,
		                                                            UIViewController vc)
		{
			var authContext = new AuthenticationContext(authority);
			//if (authContext.TokenCache.ReadItems().Any())
			//{
			//	authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
			//}

			var uri = new Uri(returnUri);
			var platformParams = new PlatformParameters(vc);

			try
			{
				var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
				return authResult;
			}
			catch (AdalException e)
			{
				return null;
			}
		}
	}
}
