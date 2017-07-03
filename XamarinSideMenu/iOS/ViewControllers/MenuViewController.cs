using System;

using UIKit;

namespace XamarinSideMenu.iOS
{
	public partial class MenuViewController : UIViewController
	{
		protected SidebarNavigation.SidebarController sidebarVC
		{
			get
			{
				return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.sidebarVC;
			}
		}

		protected NavViewController navVC
		{
			get
			{
				return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.navVC;
			}
		}

		public MenuViewController(IntPtr handle) : base(handle)
		{
		}

		public MenuViewController() : base("MenuViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			segment.ValueChanged += (sender, e) => {
    			var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;
				if (selectedSegmentId == 0)
				{
					navVC.PopToRootViewController(false);
					sidebarVC.CloseMenu();
				}
				else
				{
					var storyboard = UIStoryboard.FromName("Main", null);
					var content2VC = storyboard.InstantiateViewController("Content2ViewController");
					navVC.PushViewController(content2VC, false);
					sidebarVC.CloseMenu();
				}
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

