using System;

using UIKit;
using SidebarNavigation;

namespace XamarinSideMenu.iOS
{
	public partial class ViewController : UIViewController
	{
		// the sidebar controller for the app
		public SidebarNavigation.SidebarController sidebarVC { get; private set; }

		// the navigation controller
		public NavViewController navVC { get; private set; }

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var storyboard = UIStoryboard.FromName("Main", null);
			var contentVC = storyboard.InstantiateViewController("ContentViewController");
			var menuVC = storyboard.InstantiateViewController("MenuViewController");


			navVC = new NavViewController();
			navVC.PushViewController(contentVC, false);
			sidebarVC = new SidebarNavigation.SidebarController(this, navVC, menuVC);
			sidebarVC.MenuWidth = 220;
			sidebarVC.ReopenOnRotate = false;
			sidebarVC.HasShadowing = true;
			sidebarVC.MenuLocation = SidebarController.MenuLocations.Left;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
