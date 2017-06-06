using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinForms.iOS;

[assembly: Dependency(typeof(PhoneDialer))]
namespace XamarinForms.iOS
{
	public class PhoneDialer : IDialer
	{
		public bool Dial(string number)
		{
			return UIApplication.SharedApplication.OpenUrl(
				new NSUrl("tel:" + number));
		}
	}
}
