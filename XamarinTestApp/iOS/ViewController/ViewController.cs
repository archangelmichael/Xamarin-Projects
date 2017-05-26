using System;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using CoreLocation;
using System.Threading.Tasks;

namespace XamarinTestApp.iOS
{
	public partial class ViewController : UIViewController
	{
		bool callsEnabled = false;
		private string translatedNumber = "";
		private const string CALLS_VC_SEGUE_ID = "showCallHistory";
		private const string LOCATIONS_VC_ID = "LocationsViewController";
		private List<PhoneCall> phoneCalls { get; set; }

		private LocationManager locationManager;

		public ViewController(IntPtr handle) : base(handle)
		{
			phoneCalls = new List<PhoneCall>();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			locationManager = new LocationManager();
			locationManager.StartLocationUpdates();

            setCallPossible(false);
			callsEnabled = false;
			switchDisableCalls.ValueChanged += SwitchDisableCalls_ValueChanged;

			btnTranslate.TouchUpInside += delegate {
				translatePhoneNumber(tvPhoneNumber.Text);
			};

			btnCall.TouchUpInside += (object sender, EventArgs e) => 
			{
				if (callsEnabled)
				{
					if (translatedNumber != null && translatedNumber.Trim() != "")
					{
						CLLocationCoordinate2D location = locationManager.GetLastUserLocation().Coordinate;
						PhoneCall newPhoneCall = new PhoneCall(translatedNumber,
															   DateTime.Now,
															   location.Latitude,
															   location.Longitude);
						phoneCalls.Add(newPhoneCall);
					}
				}
			};

			btnShowHistory.TouchUpInside += (object sender, EventArgs e) =>
			{
				PerformSegue(CALLS_VC_SEGUE_ID, this);
			};

			btnShowLocations.TouchUpInside += (object sender, EventArgs e) =>
			{
				LocationsViewController locationsVC = Storyboard.InstantiateViewController(LOCATIONS_VC_ID) as LocationsViewController;
				if (locationsVC != null)
				{
					locationsVC.phoneCalls = phoneCalls;
					NavigationController.PushViewController(locationsVC, true);
				}
			};

			DrawView circle = new DrawView(DrawPattern.Circle, 2, UIColor.Blue, UIColor.Yellow, CGPathDrawingMode.FillStroke);
			circle.Frame = new CGRect(0, 0, vDrawing.Frame.Size.Width, vDrawing.Frame.Size.Height);
			circle.BackgroundColor = UIColor.Clear;
			vDrawing.AddSubview(circle);
		}

		void SwitchDisableCalls_ValueChanged(object sender, EventArgs e)
		{
			if (switchDisableCalls.On)
			{
				callsEnabled = true;
				labelDisableCalls.Text = "Disable calls";
			}
			else
			{
				callsEnabled = false;
				labelDisableCalls.Text = "Enable calls";
			}
		}

		private void setCallPossible(bool possible)
		{
			if (possible)
			{
				btnCall.TintColor = UIColor.Green;
				btnCall.Enabled = true;
				ivAvatar.Alpha = 1;
			}
			else
			{
				lblPhoneNumber.Text = "";
				btnCall.TintColor = UIColor.Red;
				btnCall.Enabled = false;
				ivAvatar.Alpha = 0;
			}
		}

		async void translatePhoneNumber(string number)
		{

			lblPhoneNumber.Text = "";
			while (number != "")
			{
				await Task.Factory.StartNew(() =>
				{
					var letter = number.Substring(0, 1);
					var digit = NumberTranslator.ToNumber(letter);
					InvokeOnMainThread(() =>
					{
						lblPhoneNumber.Text = lblPhoneNumber.Text + digit;
						number = number.Substring(1);
						progressTranslation.Progress = (float)lblPhoneNumber.Text.Length/tvPhoneNumber.Text.Length;
					});
				});

				await Task.Delay(150);
			}

			translatedNumber = lblPhoneNumber.Text;
			if (translatedNumber != null && translatedNumber.Trim() != "")
			{
				var title = string.Format("Call {0}", translatedNumber);
				setCallPossible(true);
				tvPhoneNumber.ResignFirstResponder();
			}
			else
			{
				setCallPossible(false);
			}
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			if (segue.Identifier == CALLS_VC_SEGUE_ID)
			{
				HistoryViewController historyVC = segue.DestinationViewController as HistoryViewController;
				if (historyVC != null)
				{
					historyVC.phoneCalls = phoneCalls;
				}
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
