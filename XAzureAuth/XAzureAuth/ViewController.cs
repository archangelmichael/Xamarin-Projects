using System;
using System.Threading.Tasks;
using CoreGraphics;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UIKit;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace XAzureAuth
{
	public partial class ViewController : UIViewController
	{
		const string GraphResourseUri = "https://graph.windows.net";
		const string ManagementResourseUri = "https://management.core.windows.net";
		const string ReturnUriStr = "http://UConnectApp/";
		const string AuthorityFormat = "https://login.windows.net/{0}";
		const string CommonAuthority = "https://login.windows.net/common";
		const string Tenant = "UCB.onmicrosoft.com";
		const string AppId = "a107980b-41ff-4438-8b71-c86e03e7cd6d";

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
				var authority = string.Format(AuthorityFormat, Tenant);
				var authResult = await ADAuth.Authenticate(authority, GraphResourseUri, AppId, ReturnUriStr, this);
				InvokeOnMainThread(() => { PrintToken(authResult); });
				var users = await GetAzureADProfile(Tenant, authResult);
				InvokeOnMainThread(() => { Console.WriteLine(users); });
			});
		}

		void OnTestAuth()
		{
			var auth = string.Format(AuthorityFormat, Tenant);
			ADAuth.LogoutAsync(auth);
		}

		async Task<string> GetAzureADProfile(string tenant, AuthenticationResult authResult)
		{
			var getUsersUrlStr = string.Format("https://graph.windows.net/{0}/me?api-version=1.6", tenant);
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, getUsersUrlStr);
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
			var response = await client.SendAsync(request);
			var content = await response.Content.ReadAsStringAsync();
			return content;
		}

		async Task<string> GetAzureADUsers(string tenant, AuthenticationResult authResult)
		{
			var getUsersUrlStr = string.Format("https://graph.windows.net/{0}/users?api-version=1.6", tenant);
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, getUsersUrlStr);
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
			var response = await client.SendAsync(request);
			var content = await response.Content.ReadAsStringAsync();
			return content;
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
			buttonTestOpenAuth.SetTitle("Logout", UIControlState.Normal);
			buttonTestOpenAuth.SetTitleColor(UIColor.White, UIControlState.Normal);
			buttonTestOpenAuth.BackgroundColor = UIColor.Cyan;
			buttonTestOpenAuth.TouchUpInside += (sender, e) => { OnTestAuth(); };
			this.View.AddSubview(buttonTestOpenAuth);
		}
	}
}
