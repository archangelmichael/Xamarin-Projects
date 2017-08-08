using System;
using System.Collections.Generic;

namespace XMyCalendar
{
	public interface ICalendarDelegate
	{
		List<CalendarEvent> GetEventsForDate(DateTime date);
		void DateSelected(DateTime? date);
	}
}
