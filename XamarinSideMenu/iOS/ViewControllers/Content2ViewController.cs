using System;

using UIKit;

namespace XamarinSideMenu.iOS
{
	public partial class Content2ViewController : UIViewController
	{
		public Content2ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			btnMenu2.TouchUpInside += (sender, e) => { };
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

