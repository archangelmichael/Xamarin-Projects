using System;

using UIKit;

namespace XamarinSideMenu.iOS
{
	public partial class SubcontentViewController : UIViewController
	{
		public SubcontentViewController(IntPtr handle) : base(handle)
		{
		}

		public SubcontentViewController() : base("SubcontentViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			btnBack.TouchUpInside += (sender, e) => { DismissViewController(false, null); };
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

