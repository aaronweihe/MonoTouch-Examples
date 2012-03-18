using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace NavDemo
{
	public partial class InfoViewController : UIViewController
	{
		NSTimer UpdateBatteryStatusTimer;
		
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
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
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
	}
}

