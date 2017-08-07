using System;
using Foundation;
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
			var nibs = NSBundle.MainBundle.LoadNib("CalendarView", this, null);
			var calendar = nibs.GetItem<CalendarView>(0);
			calendar.Frame = new CoreGraphics.CGRect(0, 20, View.Bounds.Width, View.Bounds.Height);
			View.AddSubview(calendar);
		}
	}
}
