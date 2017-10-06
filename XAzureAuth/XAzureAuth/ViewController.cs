using System;
using System.Threading.Tasks;
using CoreGraphics;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using UIKit;
using XAzureAuth.Authentication;

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

        UIButton btnLogin;
        UIButton btnGetProfile;
        UIButton btnGetUsers;
        UIButton btnLogout;

        AuthenticationResult adalAuthenticationResult;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			AddAuthButtons();

            btnGetProfile.Enabled = false;
            btnGetUsers.Enabled = false;
		}

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (ADALAuth.HasExistingAuthentication())
			{
				OnLogin();
			}
        }

		void OnLogin()
		{
			Task.Run(async () =>
			{
                adalAuthenticationResult = await ADALAuth.GetToken(this);
                if (adalAuthenticationResult != null)
                {
                    InvokeOnMainThread(() =>
                    {
                        Console.WriteLine("Fetch token complete");
						btnGetProfile.Enabled = true;
						btnGetUsers.Enabled = true;
                    });
                }
                else
                {
                    Console.WriteLine("ADAL authentication failed");
                }
			});
		}

        async void OnGetProfile() 
        {
            var profile = await ADALAuth.GetProfile(adalAuthenticationResult);
			InvokeOnMainThread(() =>
            {
				Console.WriteLine("Fetch profile complete");
            });
        }

        async void OnGetUsers()
        {
            var users = await ADALAuth.GetUsers(adalAuthenticationResult);
			InvokeOnMainThread(() =>
            {
                Console.WriteLine("Fetch users complete");
            });
        }

		void OnLogout()
		{
            ADALAuth.Logout();
			btnGetProfile.Enabled = false;
			btnGetUsers.Enabled = false;
		}

		void AddAuthButtons()
		{
			var btnHeight = 60;
            var verticalOffset = 10;
			var center = this.View.Center;
            center.Y = 60;
			var size = new CGSize(this.View.Bounds.Width - 40, btnHeight);
            btnLogin = new UIButton(UIButtonType.Custom)
            {
                Frame = new CGRect(center, size),
                Center = center
            };
            btnLogin.SetTitle("Login", UIControlState.Normal);
            btnLogin.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnLogin.BackgroundColor = UIColor.Cyan;
            btnLogin.TouchUpInside += (sender, e) => { OnLogin(); };
            this.View.AddSubview(btnLogin);

			center.Y += btnHeight + verticalOffset;
			btnGetProfile = new UIButton(UIButtonType.Custom)
			{
				Frame = new CGRect(center, size),
				Center = center
			};
			btnGetProfile.SetTitle("Get Profile", UIControlState.Normal);
			btnGetProfile.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnGetProfile.BackgroundColor = UIColor.Cyan;
			btnGetProfile.TouchUpInside += (sender, e) => { OnGetProfile(); };
			this.View.AddSubview(btnGetProfile);

			center.Y += btnHeight + verticalOffset;
			btnGetUsers = new UIButton(UIButtonType.Custom)
			{
				Frame = new CGRect(center, size),
				Center = center
			};
			btnGetUsers.SetTitle("Get Users", UIControlState.Normal);
			btnGetUsers.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnGetUsers.BackgroundColor = UIColor.Cyan;
			btnGetUsers.TouchUpInside += (sender, e) => { OnGetUsers(); };
			this.View.AddSubview(btnGetUsers);

            center.Y += btnHeight + verticalOffset;
			btnLogout = new UIButton(UIButtonType.Custom)
			{
				Frame = new CGRect(center, size),
                Center = center
			};
			btnLogout.SetTitle("Logout", UIControlState.Normal);
			btnLogout.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnLogout.BackgroundColor = UIColor.Cyan;
			btnLogout.TouchUpInside += (sender, e) => { OnLogout(); };
			this.View.AddSubview(btnLogout);
		}
	}
}
