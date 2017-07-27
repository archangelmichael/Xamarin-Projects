using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace XOnboarding.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			btnLoad.TouchUpInside += (sender, e) => { SetRandomProgress(); };
		}

		void SetRandomProgress()
		{
			Random rnd = new Random();
			int days = rnd.Next(0, 180);
			// progress.SetProgress(days);

			lblProgress.Text = days.ToString();
			AddAnimatedRadialProgressLayer(lblProgress, 
			                               UIColor.Green, 
			                               180, 
			                               0, 
			                               days, 
			                               20, 
			                               ProgressCapStyle.Round, 
			                               90, 
			                               false, 
			                               1);
		}

		void AddAnimatedRadialProgressLayer(UIView view,
													UIColor strokeColor,
													float max = 1,
													float min = 0,
													float current = 0,
													float strokeWidth = 10.0f,
													ProgressCapStyle strokeCapStyle = ProgressCapStyle.Round,
													float startAngle = 90,
													bool clockwise = true,
													float animationDuration = 1.0f)
		{
			RemoveAllSublayers(view);
			var circleSize = (float)Math.Min(view.Bounds.Size.Width, view.Bounds.Size.Height) / 2;
			var circleLayer = new CAShapeLayer();
			var circleCenter = new CGPoint(view.Bounds.Width / 2, view.Bounds.Height / 2);
			var circleRadius = circleSize - strokeWidth;
			var progressPercentage = current / max;
			var progressInDegrees = 360 * progressPercentage;
			var endAngleInDegrees = clockwise ?
				startAngle + progressInDegrees :
				startAngle - progressInDegrees;

			var startAngleInRadians = startAngle * Math.PI / 180;
			var endAngleInRadians = endAngleInDegrees * Math.PI / 180;

			var circlePath = UIBezierPath.FromArc(circleCenter,
												  circleRadius,
												  (float)startAngleInRadians,
												  (float)endAngleInRadians,
												  clockwise);

			circleLayer.Path = circlePath.CGPath;
			circleLayer.FillColor = UIColor.Clear.CGColor;
			circleLayer.StrokeColor = strokeColor.CGColor;
			circleLayer.LineWidth = strokeWidth;
			circleLayer.LineCap = new NSString(strokeCapStyle.ToString().ToLower());

			var progressAnimation = CABasicAnimation.FromKeyPath("strokeEnd");
			progressAnimation.Duration = animationDuration;
			progressAnimation.RemovedOnCompletion = false;
			progressAnimation.From = NSNumber.FromFloat(0f);
			progressAnimation.To = NSNumber.FromFloat(1f);
			progressAnimation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseOut);

			circleLayer.AddAnimation(progressAnimation, "drawProgress");
			view.Layer.AddSublayer(circleLayer);
		}

		void RemoveAllSublayers(UIView view)
		{
			var sublayers = view.Layer.Sublayers;
			if (sublayers == null) { return; }
			foreach (var sublayer in view.Layer.Sublayers)
			{
				sublayer.RemoveFromSuperLayer();
			}
		}
	}
}
