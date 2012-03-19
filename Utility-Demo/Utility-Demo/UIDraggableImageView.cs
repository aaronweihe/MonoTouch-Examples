using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace UtilityDemo
{
	public class UIDraggableImageView: UIImageView
	{
		public UIDraggableImageView (UIImage image): base(image)
		{
			UserInteractionEnabled = true;
		}
		
		private PointF startLocation;
		
		private void MoveImage (NSSet touches)
		{
			var pt = (touches.AnyObject as UITouch).LocationInView (this);
			
			var frame = Frame;
			var appFrame = UIScreen.MainScreen.ApplicationFrame;
			frame.X += pt.X - startLocation.X;
			if (frame.X < 0)
				frame.X = 0;
			if (frame.X + Image.Size.Width > appFrame.Width)
				frame.X = appFrame.Width - Image.Size.Width;
			frame.Y += pt.Y - startLocation.Y;
			if (frame.Y < 0)
				frame.Y = 0;
			if (frame.Y + Image.Size.Height > appFrame.Height)
				frame.Y = appFrame.Height - Image.Size.Height;
			Frame = frame;
		}
		
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			MoveImage (touches);
		}
		
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			var pt = (touches.AnyObject as UITouch).LocationInView (this);
			startLocation = pt;
			Superview.BringSubviewToFront (this);
		}
		
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			MoveImage (touches);
		}
	}
}

