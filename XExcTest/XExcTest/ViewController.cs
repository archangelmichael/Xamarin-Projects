using System;
using Microsoft.Exchange.WebServices.Data;
using UIKit;

namespace XExcTest
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            try
            {
                var service = GetExcService();
                SendEmail(service);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ExchangeService GetExcService()
        {
			var service = new ExchangeService();
			service.UseDefaultCredentials = true;
			service.Credentials = new WebCredentials("E607091", "*******");
			service.Url = new Uri("https://mail.ucb.com/ews/exchange.asmx");
            return service;
        }

        public void SendEmail(ExchangeService service)
        {
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Exchange connection notice";
            message.Body = "Exchange email sent from xamarin app";
            message.ToRecipients.Add("r.shikerov@theoryx.com");
            message.SendAndSaveCopy();
            Console.WriteLine("Email sent");
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
