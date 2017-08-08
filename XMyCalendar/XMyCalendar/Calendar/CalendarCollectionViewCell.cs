using System;

using Foundation;
using UIKit;

namespace XMyCalendar
{
	public partial class CalendarCollectionViewCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString("CalendarCollectionViewCell");
		public static UINib Nib { get { return UINib.FromName(Key, NSBundle.MainBundle); } }

		protected CalendarCollectionViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

		}
	}
}
