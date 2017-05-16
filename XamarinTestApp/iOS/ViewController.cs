using System;

using UIKit;

namespace XamarinTestApp.iOS
{
	public partial class ViewController : UIViewController
	{
		int count = 1;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			btnHi.TouchUpInside += delegate {
				var title = string.Format("Hi for {0} time", count++);
				btnHi.SetTitle(title, UIControlState.Normal);
			};

			string translatedNumber = "";
			btnHello.TouchUpInside += (object sender, EventArgs e) =>
			{
				translatedNumber = NumberTranslator.ToNumber(tvNumber.Text);
				var title = string.Format("Call {0}", translatedNumber);
				btnHello.SetTitle(title, UIControlState.Normal);
				tvNumber.ResignFirstResponder();
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
