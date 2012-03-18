using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace NavDemo
{
	public partial class InfoViewController : UIViewController
	{
		NSTimer UpdateBatteryStatusTimer;
		NSTimer ClearMotionLabelTimer;
		private PointF StartCoord;
		
		public InfoViewController () : base ("InfoViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.MultipleTouchEnabled = true;
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return true;
		}
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			DeviceLabel.Text = string.Format("{0}, iOS v{1}", DeviceHardware.VersionString, UIDevice.CurrentDevice.SystemVersion);
			UpdateUIMetrics();
			statusBarSwitch.On = !UIApplication.SharedApplication.StatusBarHidden;
			statusBarSwitch.ValueChanged += HandleStatusBarSwitchValueChanged;
			
			UIDevice.CurrentDevice.ProximityMonitoringEnabled = true;
			if(UIDevice.CurrentDevice.ProximityMonitoringEnabled)
				NSNotificationCenter.DefaultCenter.AddObserver(UIDevice.ProximityStateDidChangeNotification, (obj) => {proximityLabel.Text = UIDevice.CurrentDevice.ProximityState ? "Proximity detected" : "Proximity not detected";});
			else
				proximityLabel.Text  = "Proximity sensor not available";
			UpdateBatteryStatusTimer = NSTimer.CreateRepeatingScheduledTimer(60, ReadBatteryStatus);
			ReadBatteryStatus();
			BecomeFirstResponder();
			UIApplication.SharedApplication.ApplicationSupportsShakeToEdit = true;
		}

		void HandleStatusBarSwitchValueChanged (object sender, EventArgs e)
		{
			UIApplication.SharedApplication.StatusBarHidden = !statusBarSwitch.On;
			if((View != null) && (View.Window != null))
				View.Window.Frame = UIScreen.MainScreen.ApplicationFrame;
			
			var AppDel = (AppDelegate)UIApplication.SharedApplication.Delegate;
			var NavController = AppDel.NavController;
			NavController.SetNavigationBarHidden(true, false);
			NavController.SetNavigationBarHidden(false, false);
			UpdateUIMetrics();
		}
		
		private void UpdateUIMetrics()
		{
			var scrn = UIScreen.MainScreen;
			resolutionLabel.Text = string.Format("{0}x{1} points, {2}x{3} pixels", scrn.Bounds.Width, scrn.Bounds.Height, scrn.Bounds.Width * scrn.Scale, scrn.Bounds.Height * scrn.Scale);
			frameSizeLabel.Text = string.Format("{0}x{1} points", scrn.ApplicationFrame.Width, scrn.ApplicationFrame.Height);
		}
		
		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			if(UIDevice.CurrentDevice.ProximityMonitoringEnabled)
				UIDevice.CurrentDevice.ProximityMonitoringEnabled = false;
			UpdateBatteryStatusTimer.Invalidate();
			ResignFirstResponder();
		}
		
		private void ReadBatteryStatus()
		{
			var dev = UIDevice.CurrentDevice;
			dev.BatteryMonitoringEnabled = true;
			if(dev.BatteryMonitoringEnabled)
			try
			{
				batteryLabel.Text = string.Format("{0}% - {1}", Math.Round(dev.BatteryLevel *100), dev.BatteryState);
			}
			finally
			{
				dev.BatteryMonitoringEnabled = false;
			}
			else
			{
				batteryLabel.Text = "Battery level monitoring not available";
				UpdateBatteryStatusTimer.Invalidate();
			}
		}
		
		public override void WillAnimateRotation (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			switch(toInterfaceOrientation)
			{
			case UIInterfaceOrientation.Portrait:
				interactionLabel.Text = "iPhone is oriented normally";
				break;
			case UIInterfaceOrientation.LandscapeLeft:
				interactionLabel.Text = "iPhone has been rotated right";
				break;
			case UIInterfaceOrientation.LandscapeRight:
				interactionLabel.Text = "iPhone has been rotated left";
				break;
			case UIInterfaceOrientation.PortraitUpsideDown:
				interactionLabel.Text = "iPhone is upside down";
				break;
			}
			//SetTiimerToClearMotionLabel();
			UpdateUIMetrics();
		}
		
		private void SetTimerToClearMotionLabel()
		{
			if(ClearMotionLabelTimer != null)
				ClearMotionLabelTimer.Invalidate();
			ClearMotionLabelTimer = NSTimer.CreateScheduledTimer(3, () => 
			                                                    {
				interactionLabel.Text = "None";
				ClearMotionLabelTimer = null;
			});
		}
		
		public override bool CanBecomeFirstResponder 
		{
			get { return true;}
		}
		
		public override void MotionEnded (UIEventSubtype motion, UIEvent evt)
		{
			if(motion == UIEventSubtype.MotionShake)
			{
				interactionLabel.Text=  "iPhone was shaken";
				SetTimerToClearMotionLabel();
			}
		}
		
		private string DescribeTouch(UITouch touch)
		{
			string desc;
			switch(touch.TapCount)
			{
			case 0:
				desc = "Swipe";
				break;
			case 1:
				desc = "Single Tap";
				break;
			case 2:
				desc = "Double Tap";
				break;
			default:
				desc = "Multiple tap";
				break;
			}
			switch(touch.Phase)
			{
			case UITouchPhase.Began:
				desc += " started";
				break;
			case UITouchPhase.Moved:
				desc += " moved";
				break;
			case UITouchPhase.Cancelled:
				desc += " cancelled";
				break;
			case UITouchPhase.Ended:
				desc += " ended";
				break;
			case UITouchPhase.Stationary:
				desc += " hasn't moved";
				break;
			}
			return desc;
		}
		
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			var touchArray = touches.ToArray<UITouch>();
			if(touches.Count>0)
			{
				var coord = touchArray[0].LocationInView(touchArray[0].View);
				if(touchArray[0].TapCount < 2)
					StartCoord = coord;
				interactionLabel.Text= string.Format("{0} ({1},{2})", DescribeTouch(touchArray[0]), coord.X, coord.Y);
			}
		}
		
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			var touchArray = touches.ToArray<UITouch>();
			if(touches.Count>0)
			{
				var coord = touchArray[0].LocationInView(touchArray[0].View);
				interactionLabel.Text= string.Format("{0} ({1},{2})", DescribeTouch(touchArray[0]), coord.X, coord.Y);
			}
		}
		
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			var touchArray = touches.ToArray<UITouch>();
			if(touches.Count>0)
			{
				var coord = touchArray[0].LocationInView(touchArray[0].View);
				interactionLabel.Text= string.Format("{0} ({1},{2})", DescribeTouch(touchArray[0]), coord.X, coord.Y);
				SetTimerToClearMotionLabel();
			}
		}
	}
}

