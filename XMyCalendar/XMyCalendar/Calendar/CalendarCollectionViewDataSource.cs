using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace XMyCalendar
{
	class CalendarCollectionViewDataSource : UICollectionViewDataSource
	{
		readonly List<CalendarItem> collectionItems;
		readonly UIColor dayColor = UIColor.DarkGray;
		readonly UIColor todayColor = UIColor.DarkGray;
		readonly UIColor activeDayColor = UIColor.Blue;

		public CalendarCollectionViewDataSource(List<CalendarItem> items) : this(items, UIColor.DarkGray, UIColor.DarkGray, UIColor.Blue) { }
		public CalendarCollectionViewDataSource(List<CalendarItem> items,
		                                        UIColor day,
		                                        UIColor today,
		                                        UIColor activeDay)
		{
			collectionItems = items;
			dayColor = day;
			todayColor = today;
			activeDayColor = activeDay;
		}

		public override nint NumberOfSections(UICollectionView collectionView)
		{
			return 1;
		}

		public override nint GetItemsCount(UICollectionView collectionView, nint section)
		{
			return collectionItems.Count;
		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = (CalendarCollectionViewCell)collectionView.DequeueReusableCell(CalendarCollectionViewCell.Key, indexPath);
			var item = collectionItems[indexPath.Row];
			if (item.IsGhost())
			{
				cell.SetupAsGhost();
			}
			else if (item.Selected)
			{
				cell.SetupWithItem(item, UIColor.White, activeDayColor);
			}
			else if (CalendarUtils.AreEqualDates(item.Date, DateTime.Now))
			{
				cell.SetupWithItem(item, UIColor.White, todayColor);
			}
			else
			{
				cell.SetupWithItem(item, dayColor, UIColor.Clear);
			}

			return cell;
		}
	}
}
