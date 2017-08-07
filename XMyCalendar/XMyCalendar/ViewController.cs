using System;
using UIKit;

namespace XMyCalendar
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle) { }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			ShowCalendar();
		}

		void ShowCalendar()
		{
			
		}
	}
}
