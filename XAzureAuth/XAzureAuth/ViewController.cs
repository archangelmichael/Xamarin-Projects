using System;
using System.Threading.Tasks;
using CoreGraphics;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;

namespace XAzureAuth
{
	public partial class ViewController : UIViewController
	{
		////Client ID from from step 1. point 6
		//public static string clientId = "25927d3c-.....-63f2304b90de";
		//public static string commonAuthority = "https://login.windows.net/common";
		////Redirect URI from step 1. point 5<br />
		//public static Uri returnUri = new Uri("azureauth://");
		////Graph URI if you've given permission to Azure Active Directory in step 1. point 6
		//const string graphResourceUri = "https://graph.windows.net";
		//public static string graphApiVersion = "2013-11-08";
		////AuthenticationResult will hold the result after authentication completes
		//AuthenticationResult authResult = null;
		// https://management.core.windows.net/

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			AddAuthButtons();
		}

		void OnAuth()
		{
			Task.Run(async () =>
			{
				var tokenResult = await GetAuthToken();
				InvokeOnMainThread(() => { PrintToken(tokenResult); });
			});
		}


		void OnTestAuth()
		{
			Task.Run(async () =>
			{
				var tokenResult = await GetTestAuthToken();
				InvokeOnMainThread(() => { PrintToken(tokenResult); });
			});
		}

		async Task<AuthenticationResult> GetTestAuthToken()
		{
			Console.WriteLine("TEST AD AUTH");
			var redirectUri = new Uri("http://UConnectApp/");
			var appID = "625ce3e5-a75e-48cc-b199-1d368329af58"; // clientID
			var tenant = "emanueldejanu.onmicrosoft.com";
			var authority = string.Format("https://login.windows.net/{0}", tenant);
			var graphResourseUri = "https://graph.windows.net";

			var authContext = new AuthenticationContext(authority);
			//if (authContext.TokenCache.ReadItems().Count() > 0)
			//{
			//	authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
			//}

			var result = await authContext.AcquireTokenAsync(graphResourseUri, appID, redirectUri, new PlatformParameters(this));
			return result;
		}

		async Task<AuthenticationResult> GetAuthToken()
		{
			Console.WriteLine("AD AUTH");
			var redirectUri = new Uri("http://UConnectApp/");
			var appID = "a107980b-41ff-4438-8b71-c86e03e7cd6d"; // clientID
			var tenant = "UCB.onmicrosoft.com";
			var authority = string.Format("https://login.windows.net/{0}", tenant);
			var graphResourseUri = "https://graph.windows.net";

			var authContext = new AuthenticationContext(authority);
			//if (authContext.TokenCache.ReadItems().Count() > 0)
			//{
			//	authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
			//}
    
			var result = await authContext.AcquireTokenAsync(graphResourseUri, appID, redirectUri, new PlatformParameters(this));
			return result;
		}

		void PrintToken(AuthenticationResult authResult)
		{
			if (authResult != null)
			{
				Console.WriteLine("AD auth token : {0}", authResult.AccessToken);
			}
			else
			{
				Console.WriteLine("AD auth failed");
			}
		}

		//async void TestAuth()
		//{
		//	var resourseUcbTenant = "https://login.windows.net/UCB.onmicrosoft.com";


		//	var redirectUri = new Uri("http://UConnectApp/");
		//	var appID = "625ce3e5-a75e-48cc-b199-1d368329af58"; // clientID
		//	var resourseTestTenant = "https://login.windows.net/emanueldejanu.onmicrosoft.com";
		//	var graphID = "https://graph.windows.net";

		//	var context = new AuthenticationContext(resourseTestTenant);
		//	var result = await context.AcquireTokenAsync(graphID,
		//												 appID,
		//												 redirectUri,
		//												 new PlatformParameters(this));
		//	Console.WriteLine("Result : {0}", result.AccessToken);
		//	InvokeOnMainThread(() => {
		//		var token = result.AccessToken;
		//		int a = 5;
		//	});

		//	//if (!string.IsNullOrEmpty(token))
		//	//{
		//	//	//var client = new HttpClient();
		//	//	//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationHelper.Token);
		//	//	//var queryString = HttpUtility.ParseQueryString(string.Empty);
		//	//	//queryString["api-version"] = "1.6";
		//	//	//string segmentPath = string.IsNullOrEmpty(segment) ? string.Empty : $"/{segment}";
		//	//	//var uri = $"https://graph.windows.net/me{segmentPath}?" + queryString;
		//	//	//var response = client.GetAsync(uri).Result;
		//	//	//return response.Content;
		//	//}


		//}
		// http://ParkingApp/
		//<add key = "ida:Tenant" value="UCB.onmicrosoft.com" />
		//  <add key = "ida:TenantId" value="237582ad-3eab-4d44-8688-06ca9f2e613b" />
		//  <add key = "ida:AADInstance" value="https://login.windows.net/" /> 



		void AddAuthButtons()
		{
			var btnHeight = 60;
			var center = this.View.Center;
			var size = new CGSize(this.View.Bounds.Width - 40, btnHeight);
			var buttonOpenAuth = new UIButton(UIButtonType.Custom);
			buttonOpenAuth.Frame = new CGRect(center, size);
			buttonOpenAuth.Center = center;
			buttonOpenAuth.SetTitle("Authenticate", UIControlState.Normal);
			buttonOpenAuth.SetTitleColor(UIColor.White, UIControlState.Normal);
			buttonOpenAuth.BackgroundColor = UIColor.Cyan;
			buttonOpenAuth.TouchUpInside += (sender, e) => { OnAuth(); };
			this.View.AddSubview(buttonOpenAuth);

			var buttonTestOpenAuth = new UIButton(UIButtonType.Custom);
			buttonTestOpenAuth.Frame = new CGRect(center, size);
			buttonTestOpenAuth.Center = new CGPoint(center.X, center.Y + btnHeight + 10);
			buttonTestOpenAuth.SetTitle("Test Authenticate", UIControlState.Normal);
			buttonTestOpenAuth.SetTitleColor(UIColor.White, UIControlState.Normal);
			buttonTestOpenAuth.BackgroundColor = UIColor.Cyan;
			buttonTestOpenAuth.TouchUpInside += (sender, e) => { OnTestAuth(); };
			this.View.AddSubview(buttonTestOpenAuth);
		}
	}
}
