using System.Collections.Generic;
using Foundation;
using UIKit;

namespace XMyCalendar
{
	class CalendarCollectionViewDelegate : UICollectionViewDelegate
	{
		readonly List<CalendarItem> collectionItems;
		ICalendarDelegate dateHandler;
		CalendarItem selectedItem;
		NSIndexPath selectedItemPath;

		public CalendarCollectionViewDelegate(List<CalendarItem> items,
		                                      ICalendarDelegate handler)
		{
			collectionItems = items;
			dateHandler = handler;
		}

		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			var item = collectionItems[indexPath.Row];
			if (item.IsGhost()) { return; }

			if (selectedItem == null) // Select the item
			{
				selectedItem = item;
				selectedItem.Selected = true;
				selectedItemPath = indexPath;
				collectionView.ReloadItems(new NSIndexPath[] { selectedItemPath });
				if (dateHandler != null) { dateHandler.DateSelected(selectedItem.Date); }
			}
			else
			{
				if (selectedItem == item) // Deselect the item
				{
					selectedItem.Selected = false;
					selectedItem = null;
					collectionView.ReloadItems(new NSIndexPath[] { selectedItemPath });
				}
				else // Deselect old item and select new item
				{
					selectedItem.Selected = false;
					selectedItem = item;
					selectedItem.Selected = true;
					collectionView.ReloadItems(new NSIndexPath[] { selectedItemPath, indexPath });
					selectedItemPath = indexPath;
					if (dateHandler != null) { dateHandler.DateSelected(selectedItem.Date); }
				}
			}
		}
	}
}
