using System;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace XamarinTestApp.iOS
{
	public partial class HistoryViewController : UITableViewController
	{
		public List<string> PhoneNumbers { get; set; }

		static NSString callHistoryCellId = new NSString("CallHistoryCell");

		public HistoryViewController(IntPtr handle) : base(handle)
		{
			TableView.RegisterClassForCellReuse(typeof(UITableViewCell), callHistoryCellId);
			TableView.Source = new HistoryDataSource(this);
			PhoneNumbers = new List<string>();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
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
				return controller.PhoneNumbers.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(callHistoryCellId);

				int row = indexPath.Row;
				cell.TextLabel.Text = controller.PhoneNumbers[row];
				return cell;
			}
		}
	}
}

