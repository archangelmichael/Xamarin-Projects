using System;
using System.Collections.Generic;
using UIKit;

namespace XamarinTestApp.iOS
{
	public partial class CallsCollectionViewController : UIViewController
	{
		public static string SegueId = "showCalls";
		public List<PhoneCall> calls { get; set; }

		public CallsCollectionViewController() : base("CallsCollectionViewController", null)
		{
			
		}

		public CallsCollectionViewController(IntPtr handler) : base(handler)
		{
			
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			AutomaticallyAdjustsScrollViewInsets = true;
			collectionView.ContentInset = new UIEdgeInsets(0, 0, 0, 0);

			collectionView.RegisterNibForCell(CallCollectionViewCell.Nib, CallCollectionViewCell.ReuseId);
			collectionView.Source = new CallsCollectionDataSource(this);
			collectionView.Delegate = new CallsCollectionDelegate(this);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		class CallsCollectionDelegate : UICollectionViewDelegateFlowLayout
		{
			readonly CallsCollectionViewController controller;

			List<PhoneCall> selectedCalls = new List<PhoneCall>();

			public CallsCollectionDelegate(CallsCollectionViewController controller)
			{
				this.controller = controller;
			}

			public override CoreGraphics.CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, Foundation.NSIndexPath indexPath)
			{
				return new CoreGraphics.CGSize(150.0f, 80.0f);
			}

			public override bool ShouldHighlightItem(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
			{
				return true;
			}

			public override void ItemHighlighted(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
			{
				var cell = (CallCollectionViewCell)collectionView.CellForItem(indexPath);
				cell.BackgroundColor = UIColor.Blue;
			}

			public override void ItemUnhighlighted(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
			{
				var cell = (CallCollectionViewCell)collectionView.CellForItem(indexPath);
				cell.BackgroundColor = UIColor.White;
			}

			public override bool ShouldSelectItem(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
			{
				return true;
			}

			public override void ItemSelected(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
			{
				var call = controller.calls[indexPath.Row];
				selectedCalls.Add(call);
				Console.WriteLine("{0} calls", selectedCalls.Count);
			}
		}

		class CallsCollectionDataSource : UICollectionViewSource
		{
			readonly CallsCollectionViewController controller;

			public CallsCollectionDataSource(CallsCollectionViewController controller)
			{
				this.controller = controller;
			}

			public override nint NumberOfSections(UICollectionView collectionView)
			{
				return 1;
			}

			public override nint GetItemsCount(UICollectionView collectionView, nint section)
			{
				return controller.calls.Count;
			}

			public override UICollectionViewCell GetCell(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
			{
				var cell = collectionView.DequeueReusableCell(CallCollectionViewCell.ReuseId, indexPath) as CallCollectionViewCell;
				var call = controller.calls[indexPath.Row];
				cell.UpdateCell(call);
				return cell;
			}
		}
	}
}

