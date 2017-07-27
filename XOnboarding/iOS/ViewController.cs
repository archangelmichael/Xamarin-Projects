using System;
using UIKit;

namespace XOnboarding.iOS
{
	public partial class ViewController : UIViewController
	{
		ProgressView progress;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//AddProgressView();


			btnLoad.TouchUpInside += (sender, e) => { SetRandomProgress(); };
		}

		void AddProgressView()
		{
			progress = new ProgressView()
			{
				MinValue = 0,
				MaxValue = 180,
				ClockwiseDirection = false,
				StrokeColor = UIColor.Green,
				StrokeWidth = 20.0f,
				AnimationDuration = 0.5f
			};

			Utils.AddResizableView(progress, vProgress);
		}

		void SetRandomProgress()
		{
			Random rnd = new Random();
			int days = rnd.Next(0, 180);
			// progress.SetProgress(days);

			lblProgress.Text = days.ToString();
			ProgressView.AddAnimatedRadialProgressLayer(lblProgress, UIColor.Green, 180, 0, days, 20, ProgressCapStyle.Round, 90, false, 1);

		}
	}
}
