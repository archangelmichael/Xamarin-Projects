using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace XMyCalendar
{
	public partial class ViewController : UIViewController, ICalendarDelegate
	{
		CalendarView calendar;

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
			calendar = nibs.GetItem<CalendarView>(0);
			calendar.Frame = new CoreGraphics.CGRect(0, 20, View.Bounds.Width, 400);
			calendar.Delegate = this;
			calendar.CreateFromDate(DateTime.Now);
			View.AddSubview(calendar);
		}

		public List<CalendarEvent> GetEventsForDate(DateTime date)
		{
			var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

			return new List<CalendarEvent>
			{
				new CalendarEvent(currentDate),
				new CalendarEvent(currentDate.AddDays(10)),
				new CalendarEvent(currentDate.AddDays(8)),
				new CalendarEvent(currentDate.AddDays(5)),
				new CalendarEvent(currentDate.AddDays(-2)),
				new CalendarEvent(currentDate.AddDays(5)),
				new CalendarEvent(currentDate.AddDays(5), currentDate.AddDays(7))
			};
		}

		public void DateSelected(DateTime? date)
		{
			if (date == null) {  Console.WriteLine("Invalid date selected."); return; }
			Console.WriteLine("Selected date : {0}", date.Value.ToString("yy-MMM-dd ddd"));
		}

		public override void ViewWillTransitionToSize(CoreGraphics.CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
		{
			base.ViewWillTransitionToSize(toSize, coordinator);
			if (calendar != null) { calendar.ReloadView(); } 
		}
	}
}
