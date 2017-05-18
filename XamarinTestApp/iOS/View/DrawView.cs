using System;
using CoreGraphics;
using UIKit;

namespace XamarinTestApp.iOS
{
	public enum DrawPattern : int
	{
		Circle,
		Triangle,
		Rectangle
	};

	public class DrawView : UIView
	{
		private float lineWidth;
		private DrawPattern drawPattern;
		private UIColor fillColor;
		private UIColor strokeColor;
		private CGPathDrawingMode drawMode;


		public DrawView(DrawPattern drawPattern = DrawPattern.Circle,
						float lineWidth = 2f,
		                UIColor fillColor = null,
		                UIColor strokeColor = null, 
		                CGPathDrawingMode drawMode = CGPathDrawingMode.FillStroke)
		{
			this.drawPattern = drawPattern;
			this.lineWidth = lineWidth;
			this.fillColor = fillColor != null ? fillColor : UIColor.Black;
			this.strokeColor = strokeColor != null ? strokeColor : UIColor.Gray;
			this.drawMode = drawMode;
		}

		public override void Draw(CoreGraphics.CGRect rect)
		{
			base.Draw(rect);

			//get graphics context
			using (CGContext context = UIGraphics.GetCurrentContext())
			{
				fillColor.SetFill();
				strokeColor.SetStroke();
				context.SetLineWidth(lineWidth);

				switch (drawPattern)
				{
					case DrawPattern.Rectangle:
                        DrawRectangle(context, rect);
						break;
					case DrawPattern.Triangle:
                        DrawTriangle(context, rect);
						break;
					default:
						DrawCircle(context, rect);
						break;
				}
			}
		}

		private void DrawCircle(CGContext context, CGRect frame)
		{
			context.AddEllipseInRect(frame);
			context.DrawPath(drawMode);
		}

		private void DrawTriangle(CGContext context, CGRect frame)
		{
			var path = new CGPath();
			float lineOffset = lineWidth / 2;

			path.AddLines(new CGPoint[]{
				new CGPoint(0 + lineOffset, frame.Height - lineOffset),
				new CGPoint(frame.Width/2 - lineOffset, lineOffset),
				new CGPoint(frame.Width - lineOffset, frame.Height - lineOffset)
			});

			path.CloseSubpath();

			context.AddPath(path);
			context.DrawPath(drawMode);
		}

		private void DrawRectangle(CGContext context, CGRect frame)
		{
			
		}
	}
}
