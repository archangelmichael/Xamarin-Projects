using System;
using CoreLocation;

namespace XamarinTestApp.iOS
{
	public class LocationManager
	{
		private CLLocationManager locationManager;
		private CLLocation lastUserLocation = new CLLocation(42.698334, 23.319941);

		public LocationManager()
		{
            locationManager = new CLLocationManager();
            locationManager.PausesLocationUpdatesAutomatically = false;

			// When receiving location update
			locationManager.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
			{
				lastUserLocation = e.Locations[e.Locations.Length - 1] as CLLocation;
				Console.WriteLine("Location updated! {0} {1}", lastUserLocation.Coordinate.Latitude, lastUserLocation.Coordinate.Longitude);
			};

			if (CLLocationManager.LocationServicesEnabled)
			{
				if (CLLocationManager.Status == CLAuthorizationStatus.Denied)
				{
					Console.WriteLine("Location services denied!");
				}

				locationManager.RequestWhenInUseAuthorization();
			}
			else
			{
				Console.WriteLine("Location services disabled!");
			}

			//if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
			//{
			//	locationManager.AllowsBackgroundLocationUpdates = true;
			//}
		}

		public void StartLocationUpdates()
		{
			locationManager.StartUpdatingLocation();
		}

		public CLLocation GetLastUserLocation()
		{
			return lastUserLocation;
		}
	}
}
