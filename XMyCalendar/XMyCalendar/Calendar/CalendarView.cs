using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Globalization;

namespace XMyCalendar
{
	public partial class CalendarView : UIView
	{
		UIEdgeInsets sectionInsets = new UIEdgeInsets(5, 5, 5, 5);
		DateTime currentDate;
		List<CalendarItem> items = new List<CalendarItem>();

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

		public void OpenAtDate(DateTime date)
		{
			currentDate = date;
			var monthName = GetMonthName(date);
			lblMonth.Text = monthName;
			var daysInMonth = GetDaysInMonth(date);
			var startDay = GetFirstDayOfMonthAsWeekDay(date);

			Console.WriteLine("Month name : {0}", monthName);
			Console.WriteLine("Days in month : {0}", daysInMonth);
			Console.WriteLine("Start day : {0}", startDay);
		}

		string GetMonthName(DateTime date)
		{
			return date.ToString("Y", CultureInfo.InvariantCulture);
		}

		int GetDaysInMonth(DateTime date)
		{
			var month = date.Month;
			var year = date.Year;
			return DateTime.DaysInMonth(year, month);
		}

		int GetFirstDayOfMonthAsWeekDay(DateTime date)
		{
			var month = date.Month;
			var year = date.Year;
			var firstDay = new DateTime(year, month, 1);
			return ((int)firstDay.DayOfWeek == 0) ? 7 : (int)firstDay.DayOfWeek;
		}

		void SetItems()
		{
			cvDays.DataSource = new CalendarCollectionViewDataSource(items);
			cvDays.Delegate = new CalendarCollectionViewDelegate();
		}

		List<CalendarItem> GetItemsForDate(DateTime date)
		{
			return new List<CalendarItem>
			{ 
				new CalendarItem { Date = new DateTime(), Free = false },
				new CalendarItem { Date = new DateTime(), Free = true },
				new CalendarItem { Date = new DateTime(), Free = false },
				new CalendarItem { Date = new DateTime(), Free = true },
				new CalendarItem { Date = new DateTime(), Free = false },
				new CalendarItem { Date = new DateTime(), Free = true },
				new CalendarItem { Date = new DateTime(), Free = false },
				new CalendarItem { Date = new DateTime(), Free = true },
				new CalendarItem { Date = new DateTime(), Free = false },
				new CalendarItem { Date = new DateTime(), Free = true },
				new CalendarItem { Date = new DateTime(), Free = false },
				new CalendarItem { Date = new DateTime(), Free = true },
				new CalendarItem { Date = new DateTime(), Free = false },
				new CalendarItem { Date = new DateTime(), Free = true },
				new CalendarItem { Date = new DateTime(), Free = false },
				new CalendarItem { Date = new DateTime(), Free = true },
				new CalendarItem { Date = new DateTime(), Free = false },
				new CalendarItem { Date = new DateTime(), Free = true }
			};
		}


		public void GoToNextMonth()
		{
			OpenAtDate(currentDate.AddMonths(1));
		}

		public void GoToPrevMonth()
		{
            OpenAtDate(currentDate.AddMonths(-1));
		}

		class CalendarItem
		{
			public DateTime Date;
			public bool Free;

			public string GetDay()
			{
				return Date.Day.ToString();
			}
		}

		class CalendarCollectionViewDataSource : UICollectionViewDataSource
		{
			List<CalendarItem> collectionItems;

			public CalendarCollectionViewDataSource(List<CalendarItem> items)
			{
				collectionItems = items;
			}

			public override nint GetItemsCount(UICollectionView collectionView, nint section)
			{
				return collectionItems.Count;
			}

			public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
			{
				var cell = (CalendarCollectionViewCell)collectionView.DequeueReusableCell(CalendarCollectionViewCell.Key, indexPath);
				return cell;
			}
		}

		class CalendarCollectionViewDelegate : UICollectionViewDelegate
		{
			
		}

		class CalendarCollectionViewFlowLayout : UICollectionViewFlowLayout
		{
			
		}
    }
}