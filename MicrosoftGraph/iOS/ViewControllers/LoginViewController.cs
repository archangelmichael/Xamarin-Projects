using Foundation;
using System;
using UIKit;
using Microsoft.Graph;
using MicrosoftGraph.iOS.Helpers;

namespace MicrosoftGraph.iOS
{
    public partial class LoginViewController : UIViewController
    {
        string connect = "connect";
        string disconnect = "disconnect";

        string connectionPossible = "Connection is possible";
        string connectionNotPossible = "Connection is not possible. No client id";

        string testMailTitle = "Test microsoft graph";
        string testMailContent = "Test microsoft content";
        string testEmail = "r.shikerov@theoryx.com";
        string testSuccessful = "Email sent to {0}";
        string testFailed = "Email was not sent";

		private static GraphServiceClient graphClient = null;
		private Mail mailHelper = new Mail();

        public LoginViewController (IntPtr handle) : base (handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            btnConnect.SetTitle(connect, UIControlState.Normal);
            btnMail.Hidden = true;
            btnMail.TouchUpInside += (sender, e) => { OnMail(); };
            btnConnect.TouchUpInside += (sender, e) => { OnConnect(); };

			// Developer code - if you haven't registered the app yet, we warn you. 
            if (AppDelegate.ClientID == "")
			{
                lblInfo.Text = connectionNotPossible;
                btnConnect.Enabled = false;
			}
			else
			{
                lblInfo.Text = connectionPossible;
				btnConnect.Enabled = true;
			}
        }

        void OnConnect()
        {
            if (btnConnect.TitleLabel.Text == connect)
            {
                Connect();
            }
            else
            {
                Disconnect();
            }
        }

        async void Connect() 
        {
            try
            {
				graphClient = Authentication.GetAuthenticatedClient();
				var currentUser = await graphClient.Me.Request().GetAsync();
				AppDelegate.Username = currentUser.DisplayName;
				AppDelegate.UserEmail = currentUser.UserPrincipalName;

				lblInfo.Text = string.Format("Hello, {0}", AppDelegate.Username);
				btnMail.Hidden = false;
				btnConnect.SetTitle(disconnect, UIControlState.Normal);
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        void Disconnect()
        {
			Authentication.SignOut();
			lblInfo.Text = connectionPossible;
			btnMail.Hidden = true;
            btnConnect.SetTitle(connect, UIControlState.Normal);
        }

        async void OnMail() 
        {
			try
			{
                await mailHelper.ComposeAndSendMailAsync(testMailTitle, testMailContent, testEmail);
                lblInfo.Text = string.Format(testSuccessful, testEmail);
			}
			catch (ServiceException exception)
			{
                lblInfo.Text = testFailed;
				throw new Exception("We could not send the message: " + exception.Error == null ? "No error message returned." : exception.Error.Message);

			}
        }
    }
}