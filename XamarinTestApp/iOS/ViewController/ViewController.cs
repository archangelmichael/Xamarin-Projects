using System;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using CoreLocation;

namespace XamarinTestApp.iOS
{
	public partial class ViewController : UIViewController
	{
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
			btnTranslate.TouchUpInside += delegate {
				translatedNumber = NumberTranslator.ToNumber(tvPhoneNumber.Text);
				if (translatedNumber != null && translatedNumber.Trim() != "")
				{
					var title = string.Format("Call {0}", translatedNumber);
					lblPhoneNumber.Text = title;
					setCallPossible(true);
					tvPhoneNumber.ResignFirstResponder();
				}
				else
				{
					setCallPossible(false);
				}
			};

			btnCall.TouchUpInside += (object sender, EventArgs e) => 
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
