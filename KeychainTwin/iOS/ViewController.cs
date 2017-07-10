using System;
using Square.Valet;
using UIKit;


namespace KeychainTwin.iOS
{
	public partial class ViewController : UIViewController
	{
public const string CERTIFICATE_KEY = "certificate-key";
public const string CERTIFICATE_GROUP = "P4JVA28GY3.com.oryx.shared.keychain";
public const string CERTIFICATE_SERVICE = "UConnect";

		Valet valet;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			valet = new Valet(CERTIFICATE_GROUP, Accessibility.WhenUnlocked, false);

			btnSave.TouchUpInside += (sender, e) =>
			{
				var result = valet.SetString("twin data", CERTIFICATE_KEY);
				Console.WriteLine(result.ToString());
			};

			btnLoad.TouchUpInside += (sender, e) =>
			{
				var result = valet.GetString(CERTIFICATE_KEY);
				Console.WriteLine(result);
			};

			btnDelete.TouchUpInside += (sender, e) =>
			{
				var result = valet.RemoveObject(CERTIFICATE_KEY);
				Console.WriteLine(result.ToString());
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
