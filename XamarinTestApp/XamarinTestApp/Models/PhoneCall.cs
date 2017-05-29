using System;
namespace XamarinTestApp
{
	public class PhoneCall
	{
		public string title { get; set; }
		public DateTime date { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }

		public PhoneCall(string title, DateTime date, double latitude, double longitude)
		{
			this.title = title;
			this.date = date;
			this.latitude = latitude;
			this.longitude = longitude;
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
