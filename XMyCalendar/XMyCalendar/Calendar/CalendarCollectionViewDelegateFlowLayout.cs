using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

namespace XMyCalendar
{
	public class CalendarCollectionViewDelegateFlowLayout : UICollectionViewDelegateFlowLayout
	{
		readonly UIEdgeInsets sectionInsets = new UIEdgeInsets(5, 5, 5, 5);
		readonly UIView view;
		const int DAYS_PER_WEEK = 7;

		readonly List<CalendarItem> collectionItems;
		readonly ICalendarDelegate dateHandler;
		CalendarItem selectedItem;
		NSIndexPath selectedItemPath;

		public CalendarCollectionViewDelegateFlowLayout(List<CalendarItem> items,
														ICalendarDelegate handler,
														UIView collectionView)
		{
			collectionItems = items;
			dateHandler = handler;
			view = collectionView;
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

		public override CGSize GetSizeForItem(UICollectionView collectionView,
											  UICollectionViewLayout layout,
											  NSIndexPath indexPath)
		{
			var paddingSpace = sectionInsets.Left * (DAYS_PER_WEEK + 1);
			var availableWidth = view.Bounds.Width - paddingSpace;
			var itemSize = availableWidth / DAYS_PER_WEEK - 5;
			return new CGSize(itemSize, itemSize);
		}

		public override UIEdgeInsets GetInsetForSection(UICollectionView collectionView,
														UICollectionViewLayout layout,
														nint section)
		{
			return sectionInsets;
		}

		public override nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView,
															   UICollectionViewLayout layout,
															   nint section)
		{
			return sectionInsets.Left;
		}
	}
}
