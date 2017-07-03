using System;

using UIKit;

namespace XamarinSideMenu.iOS
{
	public partial class NavViewController : UINavigationController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			NavigationBarHidden = true;
		}
	}
}

