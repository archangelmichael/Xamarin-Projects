using MapKit;
using CoreLocation;

namespace XamarinTestApp.iOS
{
	public class PhoneCallAnnotation : MKAnnotation
	{
		CLLocationCoordinate2D coord;
		readonly string title;
		readonly string subtitle;

		public override string Title { get { return title; } }
		public override string Subtitle { get { return subtitle; } }

		public override CLLocationCoordinate2D Coordinate { get { return coord; } }
		public override void SetCoordinate(CLLocationCoordinate2D value) { coord = value; }

		public PhoneCallAnnotation(CLLocationCoordinate2D coordinate, string title, string subtitle)
		{
			coord = coordinate;
			this.title = title;
			this.subtitle = subtitle;
		}
	}
}
