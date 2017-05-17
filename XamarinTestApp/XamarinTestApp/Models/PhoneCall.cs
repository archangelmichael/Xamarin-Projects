using System;
namespace XamarinTestApp
{
	public class PhoneCall
	{
		private string title;
		private DateTime date;
		private double latitude;
		private double longitude;

		public PhoneCall(string title, DateTime date, double latitude, double longitude)
		{
			this.title = title;
			this.date = date;

			var latitudeSign = MathUtils.GetRandomSign();
			double latitudeDisplacement = MathUtils.GetRandomNumber(0, 5);
			this.latitude = latitude + latitudeSign * latitudeDisplacement;

			var longitudeSign = MathUtils.GetRandomSign();
			double longitudeDisplacement = MathUtils.GetRandomNumber(0, 5);
            this.longitude = longitude + longitudeSign * longitudeDisplacement;
		}

		public string GetTitle()
		{
			return this.title;
		}

		public DateTime GetDate()
		{
			return this.date;
		}

		public string GetDateString()
		{
			return this.date.ToString();
		}

		public double GetLatitude()
		{
			return this.latitude;
		}

		public double GetLongitude()
		{
			return this.longitude;
		}
	}
}
