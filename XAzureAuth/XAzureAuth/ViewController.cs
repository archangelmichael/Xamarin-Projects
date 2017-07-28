using System;
using System.Linq;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;

namespace XAzureAuth
{
	public partial class ViewController : UIViewController
	{
		//Client ID from from step 1. point 6
		public static string clientId = "25927d3c-.....-63f2304b90de";
		public static string commonAuthority = "https://login.windows.net/common";
//Redirect URI from step 1. point 5<br />
		public static Uri returnUri = new Uri("http://xam-demo-redirect");
		//Graph URI if you've given permission to Azure Active Directory in step 1. point 6
		const string graphResourceUri = "https://graph.windows.net";
		public static string graphApiVersion = "2013-11-08";
		//AuthenticationResult will hold the result after authentication completes
		AuthenticationResult authResult = null;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();



		}

		async void OpenADAuth()
		{
var authContext = new AuthenticationContext(commonAuthority);
  if (authContext.TokenCache.ReadItems().Count() > 0)
    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
 
			authResult = await authContext.AcquireTokenAsync(graphResourceUri, clientId, returnUri, new PlatformParameters(this));
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
