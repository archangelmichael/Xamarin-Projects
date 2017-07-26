using System;
using UIKit;
using CoreAnimation;
using CoreGraphics;
using Foundation;

namespace XOnboarding.iOS
{
	public enum ProgressCap
	{
		Round,
		Butt,
		Square   
	}

	public partial class ProgressView : UIView
	{
		public float AnimationDuration { get; set; } = 3;

		public float CurrentValue { get; private set; } = 0;
		public float MinValue { get; set; } = 0;
		public float MaxValue { get; set; } = 1;

		public float StartAngleInDegrees { get; set; } = 90;
		public bool ClockwiseDirection { get; set; } = true;

		public UIColor StrokeColor { get; set; } = UIColor.Black;
		public float StrokeWidth { get; set; } = 2;
		public ProgressCap StrokeCap { get; set; } = ProgressCap.Round;

		public ProgressView(IntPtr handle) : base(handle)
		{
		}

		public ProgressView()
		{
		}

		public void SetProgressTitle(UIFont font, UIColor textColor)
		{
			
		}

		public void SetProgress(float progress, bool animated = false)
		{
			ClearSublayers();
			if (progress < MinValue || progress > MaxValue)
			{
				Console.WriteLine("Invalid progress value : {0}", progress);
				return;
			}

			CurrentValue = progress;
			AddProgressLayer();
		}

		void AddProgressLayer()
		{
			var circleProgressLayer = new CAShapeLayer();
			var circleCenter = new CGPoint(Bounds.Width / 2, Bounds.Height / 2);
			var circleRadius = Bounds.Width / 2 - StrokeWidth;
			var progressPercentage = CurrentValue / MaxValue;

			var progressInDegrees = 360 * progressPercentage;
			var endAngleInDegrees = ClockwiseDirection ? 
				StartAngleInDegrees + progressInDegrees :
				StartAngleInDegrees - progressInDegrees;

			var startAngleInRadians =  StartAngleInDegrees * Math.PI / 180;
			var endAngleInRadians = endAngleInDegrees * Math.PI / 180;

			var circlePath = UIBezierPath.FromArc(circleCenter,
												  circleRadius,
												  (float)startAngleInRadians,
												  (float)endAngleInRadians,
												  ClockwiseDirection);

			circleProgressLayer.Path = circlePath.CGPath;
			circleProgressLayer.FillColor = UIColor.Clear.CGColor;
			circleProgressLayer.StrokeColor = StrokeColor.CGColor;
			circleProgressLayer.LineWidth = StrokeWidth;
			circleProgressLayer.LineCap = new NSString(StrokeCap.ToString().ToLower());

			var progressAnimation = CABasicAnimation.FromKeyPath("strokeEnd");
			progressAnimation.Duration = AnimationDuration;
			progressAnimation.RemovedOnCompletion = false;
			progressAnimation.From = NSNumber.FromFloat(0f);
			progressAnimation.To = NSNumber.FromFloat(1f);
			progressAnimation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseOut);

			circleProgressLayer.AddAnimation(progressAnimation, "drawProgress");
			Layer.AddSublayer(circleProgressLayer);
		}

		void ClearSublayers()
		{
			var sublayers = Layer.Sublayers;
			if (sublayers == null) { return; }
			foreach (var sublayer in Layer.Sublayers)
			{
				sublayer.RemoveFromSuperLayer();
			}
		}
	}
}