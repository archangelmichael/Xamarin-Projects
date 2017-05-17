﻿using System;
using System.Collections.Generic;
using UIKit;

namespace XamarinTestApp.iOS
{
	public partial class ViewController : UIViewController
	{
		private string translatedNumber = "";
		private const string CALLS_VC_SEGUE_ID = "showCallHistory";
		private const string LOCATIONS_VC_ID = "LocationsViewController";
		private List<PhoneCall> phoneCalls { get; set; }

		public ViewController(IntPtr handle) : base(handle)
		{
			phoneCalls = new List<PhoneCall>();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

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
					PhoneCall newPhoneCall = new PhoneCall(translatedNumber, DateTime.Now, 42.698334, 23.319941);
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
