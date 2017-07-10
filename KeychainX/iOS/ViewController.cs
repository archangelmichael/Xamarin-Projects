using System;
using Square.Valet;
using UIKit;

namespace KeychainX.iOS
{
	public partial class ViewController : UIViewController
	{
		public const string CERTIFICATE_KEY = "certificate-key";
		public const string CERTIFICATE_GROUP = "P4JVA28GY3.com.oryx.shared.keychain";
		public const string CERTIFICATE_SERVICE = "UConnect";
		bool userComponent = true;

		string inputValue
		{
			get
			{
				return tfInput.Text;
			}
		}

		Keychain Keychain = new Keychain(Keychain.CERTIFICATE_SERVICE, Keychain.CERTIFICATE_GROUP);
		Valet valet;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			tfInput.Delegate = new TFDelegate();

			if (userComponent)
			{
				valet = new Valet(CERTIFICATE_GROUP, Accessibility.WhenUnlocked, false);

				btnSave.TouchUpInside += (sender, e) =>
				{
					var result = valet.SetString("original data", CERTIFICATE_KEY);
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
			else
			{
				btnSave.TouchUpInside += (object sender, EventArgs e) =>
				{
					var result = Keychain.AddData(Keychain.CERTIFICATE_KEY, "new-test");
					if (!result)
					{
						var subresult = Keychain.UpdateData(Keychain.CERTIFICATE_KEY, "new-test");
						Log(string.Format(" Update {0}", subresult));
					}
					else
					{
						Log(string.Format(" Add {0}", result));
					}
				};

				btnLoad.TouchUpInside += (object sender, EventArgs e) =>
				{
					var result = Keychain.GetData(Keychain.CERTIFICATE_KEY);
					Console.WriteLine(result);
				};

				btnDelete.TouchUpInside += (object sender, EventArgs e) =>
				{
					var result = Keychain.RemoveData(Keychain.CERTIFICATE_KEY);
					Console.WriteLine(result);
				};
			}
		}

		void Log(string text)
		{
			lblResult.Text = text;
		}

		class TFDelegate : UITextFieldDelegate
		{
			public override bool ShouldReturn(UITextField textField)
			{
				textField.ResignFirstResponder();
				return true;
			}
		}
	}
}
