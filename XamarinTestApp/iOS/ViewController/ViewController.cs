using System;
using System.Collections.Generic;
using UIKit;
using CoreGraphics;
using CoreLocation;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace XamarinTestApp.iOS
{
	public partial class ViewController : UIViewController
	{
		bool callsEnabled;
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

			btnTranslate.TouchUpInside += delegate
			{
				TranslatePhoneNumber(tvPhoneNumber.Text);
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
						SaveLastPhoneCall(newPhoneCall);
					}
				}
			};

			btnShowLastCall.TouchUpInside += (object sender, EventArgs e) =>
			{
				var call = GetLastPhoneCall();
				if (call != null)
				{
					var okAlertController = UIAlertController.Create("Last call", string.Format("To {0} at {1}", call.GetTitle(), call.GetDateString()), UIAlertControllerStyle.Alert);
					okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
					PresentViewController(okAlertController, true, null);
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

			//FetchData();
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

		async void TranslatePhoneNumber(string number)
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

		void SaveLastPhoneCall(PhoneCall call)
		{
			string jsonCall = Newtonsoft.Json.JsonConvert.SerializeObject(call);
			Console.WriteLine (jsonCall);
			File.WriteAllText(GetLastPhoneCallFilePath(), jsonCall);
		}

		PhoneCall GetLastPhoneCall()
		{
			var filePath = GetLastPhoneCallFilePath();
			if (File.Exists(filePath))
			{
				string jsonCall = File.ReadAllText(filePath);
				PhoneCall call = Newtonsoft.Json.JsonConvert.DeserializeObject<PhoneCall>(jsonCall);
				Console.WriteLine("Last Call to: {0} at {1}", call.GetTitle(), call.GetDateString());
				return call;
			}

			return null;
		}

		string GetLastPhoneCallFilePath()
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var filePath = Path.Combine(documentsPath, "last_phone_call.txt");
			return filePath;
		}

		void FetchData()
		{
			var request = HttpWebRequest.Create("http://www.mobile.bg/pcgi/mobile.cgi?topmenu=1&act=4&adv=11495725980994090&slink=3fr41y");
			request.ContentType = "application/json";
			request.Method = "GET";

			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if (response.StatusCode != HttpStatusCode.OK)
				{
					Console.WriteLine("Request failed");
				}
				else
				{
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						if (string.IsNullOrWhiteSpace(content))
						{
							Console.WriteLine("Response is empty");
						}
						else
						{
							Console.WriteLine(content);
						}
					}
				}
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
