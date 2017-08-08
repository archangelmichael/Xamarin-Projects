using System;
using System.Globalization;

namespace XMyCalendar
{
	public static class CalendarUtils
	{
		public static string GetMonthName(DateTime date)
		{
			return date.ToString("Y", CultureInfo.InvariantCulture);
		}

		public static int GetDaysInMonth(DateTime date)
		{
			var month = date.Month;
			var year = date.Year;
			return DateTime.DaysInMonth(year, month);
		}

		public static int GetFirstDayOfMonthAsWeekDay(DateTime date)
		{
			var month = date.Month;
			var year = date.Year;
			var firstDay = new DateTime(year, month, 1);
			return (firstDay.DayOfWeek == 0) ? 7 : (int)firstDay.DayOfWeek;
		}

		public static bool AreEqualDates(DateTime? date1, DateTime? date2)
		{
			if (date1 == null || date2 == null) { return false; } 
			return date1?.Year == date2?.Year &&
						date1?.Month == date2?.Month &&
						date1?.Day == date2?.Day;
		}
	}
}
