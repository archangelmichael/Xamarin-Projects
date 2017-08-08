using System;
namespace XMyCalendar
{
	public class CalendarItem
	{
		public DateTime? Date { get; private set; }
		public bool Marked { get; private set; }
		public bool Selected;

		public CalendarItem(DateTime? date, bool marked = false)
		{
			Date = date;
			Marked = marked;
		}

		public bool IsGhost()
		{
			return Date == null;
		}

		public string GetDay()
		{
			if (Date == null) return "";
			else return Date?.Day.ToString();
		}
	}
}
