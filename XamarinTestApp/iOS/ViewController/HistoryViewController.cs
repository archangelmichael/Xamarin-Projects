using System;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace XamarinTestApp.iOS
{
	public partial class HistoryViewController : UITableViewController
	{
		public List<PhoneCall> phoneCalls { get; set; }

		public HistoryViewController(IntPtr handle) : base(handle)
		{
			UINib cellNib = UINib.FromName(PhoneCallTableViewCell.Key, NSBundle.MainBundle);
			TableView.RegisterNibForCellReuse(cellNib, PhoneCallTableViewCell.Key);
			TableView.Source = new HistoryDataSource(this);

			TableView.RowHeight = UITableView.AutomaticDimension;
			TableView.EstimatedRowHeight = 60.0f;

			phoneCalls = new List<PhoneCall>();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var rightBarButton = new UIBarButtonItem(
				"Switch",
				UIBarButtonItemStyle.Done,
				(object sender, EventArgs e) =>
			{
				PerformSegue(CallsCollectionViewController.SegueId, this);
			});

			NavigationItem.RightBarButtonItem = rightBarButton;
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			var segueId = segue.Identifier;
			if (segueId == CallsCollectionViewController.SegueId)
			{
				var callsCollectionVC = segue.DestinationViewController as CallsCollectionViewController;
				callsCollectionVC.calls = phoneCalls;
			}
		}
	
		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		private class HistoryDataSource : UITableViewSource
		{
			readonly HistoryViewController controller;

			public HistoryDataSource(HistoryViewController controller)
			{
				this.controller = controller;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return controller.phoneCalls.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				PhoneCallTableViewCell cell = tableView.DequeueReusableCell(PhoneCallTableViewCell.Key, indexPath) as PhoneCallTableViewCell;
				if (cell != null)
				{
					int row = indexPath.Row;
					PhoneCall rowPhoneCall = controller.phoneCalls[row];
					cell.SetCall(rowPhoneCall);
				}

				return cell;
			}
		}
	}
}

