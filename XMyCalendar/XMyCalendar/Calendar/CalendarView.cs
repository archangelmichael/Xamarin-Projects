using Foundation;
using System;
using UIKit;

namespace XMyCalendar
{
    public partial class CalendarView : UIView
    {
        public CalendarView (IntPtr handle) : base (handle)
        {
        }

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
			btnNext.TouchUpInside += (sender, e) => { Console.WriteLine("Go to next month"); };
		}
    }
}