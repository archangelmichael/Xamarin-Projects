using System;
namespace XMyCalendar
{
	public class CalendarEvent
	{
		public DateTime StartDate { get; private set; }
		public DateTime EndDate { get; private set; }
		public string Info { get; private set; }

		public CalendarEvent(DateTime startDate, DateTime endDate, string info = null)
		{
			if (startDate > endDate) { throw new Exception("Invalid calendar event end date."); }
			StartDate = startDate;
			EndDate = endDate;
			Info = info;
		}

		public CalendarEvent(DateTime startDate, string info = null) : this(startDate, startDate, info) { }

		public bool IsDateIncluded(DateTime date)
		{
			var included = date >= StartDate && date <= EndDate;
			return included;
		}
	}
}
