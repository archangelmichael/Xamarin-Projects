using System;
using UIKit;
using System.Collections.Generic;

namespace XMyCalendar
{
	public partial class CalendarView : UIView
	{
		public ICalendarDelegate Delegate;

		DateTime currentDate;
		int maxAllowedDatesSelected = 1;
		List<CalendarItem> items = new List<CalendarItem>();

		UIColor todayDateColor = UIColor.DarkGray;
		UIColor selectedDateColor = UIColor.Blue;

		public CalendarView(IntPtr handle) : base(handle)
		{
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
			btnNext.TouchUpInside += (sender, e) => { GoToNextMonth(); };
			btnPrev.TouchUpInside += (sender, e) => { GoToPrevMonth(); };
			cvDays.RegisterNibForCell(CalendarCollectionViewCell.Nib, CalendarCollectionViewCell.Key);
		}

		public void SetAppearance(UIColor titleColor,
		                          UIColor buttonColor,
		                          UIColor weekdayColor,
		                          UIColor todayColor,
		                          UIColor selectedColor)
		{
			lblMonth.TextColor = titleColor;
			lblMonday.TextColor = lblTuesday.TextColor = 
				lblWednesday.TextColor = lblThursday.TextColor =
					lblFriday.TextColor = lblSaturday.TextColor = lblSunday.TextColor = weekdayColor;
			btnNext.TintColor = btnPrev.TintColor = buttonColor;
			btnNext.SetTitleColor(buttonColor, UIControlState.Normal);
			btnPrev.SetTitleColor(buttonColor, UIControlState.Normal);
			todayDateColor = todayColor;
			selectedDateColor = selectedColor;
		}

		public void CreateFromDate(DateTime date)
		{
			currentDate = date;
			var monthName = CalendarUtils.GetMonthName(date);
			lblMonth.Text = monthName;
			var numberOfDays = CalendarUtils.GetDaysInMonth(date);
			var startDay = CalendarUtils.GetFirstDayOfMonthAsWeekDay(date);
			ReloadCalendarData(startDay, numberOfDays);
		}

		void ReloadCalendarData(int startDay, int numberOfDays)
		{
			var calendarEvents = Delegate != null ? Delegate.GetEventsForDate(currentDate) : new List<CalendarEvent>();
			var calendarItems = new List<CalendarItem>();
			var ghostDays = startDay - 1;
			for (int index = 0; index < ghostDays; index++) // Add ghost days from previous month
			{
				calendarItems.Add(new CalendarItem(null));
			}

			for (int currentDay = 1; currentDay <= numberOfDays; currentDay++) // Add days for current month
			{
				var day = new DateTime(currentDate.Year, currentDate.Month, currentDay);
				var calendarItem = GetCalendarItem(calendarEvents, day);
				calendarItems.Add(calendarItem);
			}

			Console.WriteLine(calendarItems);
			items = calendarItems;
			ReloadCalendar();
		}

		CalendarItem GetCalendarItem(List<CalendarEvent> calendarEvents, DateTime day)
		{
			foreach (var calendarEvent in calendarEvents)
			{
				if (calendarEvent.IsDateIncluded(day))
				{
					return new CalendarItem(day, true);
				}
			}

			return new CalendarItem(day);
		}

		void ReloadCalendar()
		{
			cvDays.Delegate = new CalendarCollectionViewDelegateFlowLayout(items, Delegate, cvDays);
			cvDays.DataSource = new CalendarCollectionViewDataSource(items);
			cvDays.ReloadData();
		}

		public void GoToNextMonth()
		{
			CreateFromDate(currentDate.AddMonths(1));
		}

		public void GoToPrevMonth()
		{
            CreateFromDate(currentDate.AddMonths(-1));
		}

		public void ReloadView()
		{
			cvDays.CollectionViewLayout.InvalidateLayout();
		}
    }
}