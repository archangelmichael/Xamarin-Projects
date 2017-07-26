using System;
using UIKit;

namespace XOnboarding.iOS
{
	public static class Utils
	{
		public static void AddResizableView(UIView resizableView, UIView parentView)
		{
			resizableView.TranslatesAutoresizingMaskIntoConstraints = false;
			parentView.AddSubview(resizableView);
			NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[] {
				resizableView.LeadingAnchor.ConstraintEqualTo(parentView.LeadingAnchor),
				resizableView.TrailingAnchor.ConstraintEqualTo(parentView.TrailingAnchor),
				resizableView.TopAnchor.ConstraintEqualTo(parentView.TopAnchor),
				resizableView.BottomAnchor.ConstraintEqualTo(parentView.BottomAnchor)
			});

			parentView.LayoutIfNeeded();
		}
	}
}
