using System;
using System.Collections.Generic;

using UIKit;

namespace XamarinTestApp.iOS
{
	public partial class ViewController : UIViewController
	{
		private string translatedNumber = "";

		public List<string> PhoneNumbers { get; set; }

		public ViewController(IntPtr handle) : base(handle)
		{
			PhoneNumbers = new List<string>();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			BtnTranslate.TouchUpInside += delegate {
				translatedNumber = NumberTranslator.ToNumber(TvPhoneNumber.Text);
				var title = string.Format("Call {0}", translatedNumber);
				BtnCall.SetTitle(title, UIControlState.Normal);
				TvPhoneNumber.ResignFirstResponder();
			};

			BtnCall.TouchUpInside += (object sender, EventArgs e) => {
				if (translatedNumber != null && translatedNumber.Trim() != "")
				{
					PhoneNumbers.Add(translatedNumber);
				}
			};

			BtnShowHistory.TouchUpInside += (object sender, EventArgs e) =>
			{
				PerformSegue("showCallHistory", this);
			};
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			if (segue.Identifier == "showCallHistory")
			{
				HistoryViewController historyVC = segue.DestinationViewController as HistoryViewController;
				if (historyVC != null)
				{
					historyVC.PhoneNumbers = this.PhoneNumbers;
				}
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
