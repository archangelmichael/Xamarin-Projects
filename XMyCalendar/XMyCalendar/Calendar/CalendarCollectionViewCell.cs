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

			lblDay.Text = "";
			lblDay.Layer.CornerRadius = 5.0f;
			lblDay.ClipsToBounds = true;
			vMark.BackgroundColor = UIColor.Clear;
			vMark.Layer.CornerRadius = vMark.Bounds.Width / 2;
			vMark.ClipsToBounds = true;
		}



		public void SetupAsGhost()
		{
			lblDay.Text = "";
			lblDay.Hidden = true;
			vMark.BackgroundColor = UIColor.Clear;
		}

		public void SetupWithItem(CalendarItem item, UIColor textColor, UIColor backgroundColor)
		{
			lblDay.Text = item.GetDay();
			lblDay.TextColor = textColor;
			lblDay.BackgroundColor = backgroundColor;
			lblDay.Hidden = false;
			vMark.BackgroundColor = item.Marked ? UIColor.DarkGray : UIColor.Clear;
		}
	}
}
