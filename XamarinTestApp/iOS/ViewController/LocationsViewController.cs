using System;
using UIKit;
using System.Collections.Generic;
using CoreLocation;

namespace XamarinTestApp.iOS
{
    public partial class LocationsViewController : UIViewController
    {
		public List<PhoneCall> phoneCalls { get; set; }
		private List<PhoneCallAnnotation> phoneCallAnnotations { get; set; }

        public LocationsViewController (IntPtr handle) : base (handle)
        {
			phoneCalls = new List<PhoneCall>();
			phoneCallAnnotations = new List<PhoneCallAnnotation>();
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			foreach (PhoneCall call in phoneCalls)
			{
				var callCoordinate = new CLLocationCoordinate2D(call.GetLatitude(), call.GetLongitude());
				PhoneCallAnnotation callAnnotation = new PhoneCallAnnotation(callCoordinate, call.GetTitle(), call.GetDateString());
				mvLocations.AddAnnotation(callAnnotation);
			}
		}
    }
}