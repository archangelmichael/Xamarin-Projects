using System;

using Foundation;
using UIKit;

namespace XamarinTestApp.iOS
{
	public partial class CallCollectionViewCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString("CallCollectionViewCell");
		public static readonly UINib Nib;
		public static readonly NSString ReuseId = new NSString("CallCollectionCell");

		static CallCollectionViewCell()
		{
			Nib = UINib.FromName("CallCollectionViewCell", NSBundle.MainBundle);
		}

		protected CallCollectionViewCell(IntPtr handle) : base(handle)
		{
			BackgroundColor = UIColor.Blue;
		}

		public void UpdateCell(PhoneCall call)
		{
			labelTitle.Text = call.GetTitle();
			labelSubtitle.Text = call.GetDateString();
		}
	}
}
