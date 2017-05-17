using System;

using Foundation;
using UIKit;

namespace XamarinTestApp.iOS
{
	public partial class PhoneCallTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("PhoneCallTableViewCell");
		public static readonly UINib Nib;

		static PhoneCallTableViewCell()
		{
			Nib = UINib.FromName("PhoneCallTableViewCell", NSBundle.MainBundle);
		}

		public PhoneCallTableViewCell(IntPtr handle) : base(handle)
		{
		}

		public void SetCall(PhoneCall call)
		{
			lblNumber.Text = call.GetTitle();
			lblDate.Text = call.GetDateString();
		}
	}
}
