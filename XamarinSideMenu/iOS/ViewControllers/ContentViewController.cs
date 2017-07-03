using System;

using UIKit;

namespace XamarinSideMenu.iOS
{
	public partial class ContentViewController : UIViewController
	{
		public ContentViewController(IntPtr handle) : base(handle)
		{
		}

public ContentViewController() : base("ContentViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//btnMenu1.TouchUpInside += (sender, e) => { SidebarController.ToggleMenu(); };

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

