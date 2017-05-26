using System;
using System.Collections.Generic;
using UIKit;

namespace XamarinTestApp.iOS
{
	public partial class CallsCollectionViewController : UICollectionViewController
	{
		public static string SegueId = "showCollectionView";
		public List<PhoneCall> calls { get; set; }

		public CallsCollectionViewController() : base("CallsCollectionViewController", null)
		{
			
		}

		public CallsCollectionViewController(IntPtr handler) : base(handler)
		{
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

