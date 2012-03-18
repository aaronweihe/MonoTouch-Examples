using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;
using MonoTouch.MapKit;

namespace NavDemo
{
	public partial class GPSViewController : UIViewController
	{
		private CLLocationManager locationManager;
		
		public GPSViewController () : base ("GPSViewController", null)
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
			return true;
		}
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			
			//Device Rotation
			if((InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft) || (InterfaceOrientation == UIInterfaceOrientation.LandscapeRight))
				SetupUIForOrientation(InterfaceOrientation);
			//Initialize Map View
			mapView.WillStartLoadingMap += (sender, e) => 
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			};
			mapView.MapLoaded += (sender, e) => 
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			};
			mapView.LoadingMapFailed += (sender, e) => 
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			};
			mapView.MapType = MKMapType.Hybrid;
			mapView.ShowsUserLocation = true;
			mapView.UserLocation.Title = "Your are here!";
			mapView.UserLocation.Subtitle = "YES!";
			
			//Initialize location mananger
			locationManager = new CLLocationManager ();
			locationManager.DesiredAccuracy = -1;
			locationManager.DistanceFilter = 50;
			locationManager.HeadingFilter = 1;
			//Add 2 delegates
			locationManager.UpdatedHeading += UpdatedHeading;
			locationManager.UpdatedLocation += UpdatedLocation;
			locationManager.StartUpdatingLocation ();
			locationManager.StartUpdatingHeading ();
			
		}
		
		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			locationManager.StopUpdatingHeading ();
			locationManager.StopUpdatingLocation ();
			locationManager.Dispose ();
			locationManager = null;
		}
		
		private void UpdatedHeading (object sender, CLHeadingUpdatedEventArgs args)
		{
			if (args.NewHeading.HeadingAccuracy >= 0) {
				mgHeadingLabel.Text = string.Format ("{0:F1}° ± {1:F1}°", args.NewHeading.MagneticHeading, args.NewHeading.HeadingAccuracy);
				trueHeadingLabel.Text = string.Format ("{0:F1}° ± {1:F1}°", args.NewHeading.TrueHeading, args.NewHeading.HeadingAccuracy);
			} else {
				mgHeadingLabel.Text = "N/A";
				trueHeadingLabel.Text = "N/A";
			}
		}
		
		private void UpdatedLocation (object sender, CLLocationUpdatedEventArgs args)
		{
			const double LatitudeDelta = 0.002;
			
			const double LongtitudeDelta = LatitudeDelta;
			var PosAccuracy = args.NewLocation.HorizontalAccuracy;
			if (PosAccuracy >= 0) {
				var Coord = args.NewLocation.Coordinate;
				latitudeLabel.Text = string.Format ("{0:F6}° ± {1} m", Coord.Latitude, PosAccuracy);
				longtitudeLabel.Text = string.Format ("{0:F6}° ± {1} m", Coord.Longitude, PosAccuracy);
				if (Coord.IsValid ()) {
					var region = new MKCoordinateRegion (Coord, new MKCoordinateSpan (LatitudeDelta, LongtitudeDelta));
					mapView.SetRegion (region, false);
					mapView.SetCenterCoordinate (Coord, false);
					//mapView.SelectedAnnotations (mapView.UserLocation, false);
				}
			} else {
				latitudeLabel.Text = "N/A";
				longtitudeLabel.Text = "N/A";
			}
			if (args.NewLocation.VerticalAccuracy >= 0)
				altitudeLabel.Text = string.Format ("{0:F6} m ± {1:F6} m", args.NewLocation.Altitude, args.NewLocation.VerticalAccuracy);
			else
				altitudeLabel.Text = "N/A";
			if (args.NewLocation.Course >= 0)
				courseLabel.Text = string.Format ("{0}°", args.NewLocation.Course);
			else
				courseLabel.Text = "N/A";
			speedLabel.Text = string.Format ("{0} m/s", args.NewLocation.Speed);
		}
		
		private void SetupUIForOrientation (UIInterfaceOrientation orientation)
		{
			//iPhone is 320x480
			//iPad is 768x1024
			var DeviceHeight = (int)UIScreen.MainScreen.Bounds.Height;
			var DeviceWidth = (int)UIScreen.MainScreen.Bounds.Width;
			const int NavBarHghtPortrait = 44;
			const int NavBarHghtLandscape = 32;
			const int TextLabelsWidth = 270;
			const int TextLabelsHeight = 257;
			var AppFrame = UIScreen.MainScreen.ApplicationFrame;
			if ((orientation == UIInterfaceOrientation.Portrait) || (orientation == UIInterfaceOrientation.PortraitUpsideDown))
				mapView.Frame = RectangleF.FromLTRB (0, TextLabelsHeight, DeviceWidth, AppFrame.Height - NavBarHghtPortrait);
			else
				mapView.Frame = RectangleF.FromLTRB (TextLabelsWidth, 0, DeviceHeight, AppFrame.Width - NavBarHghtLandscape);
		}
	}
}

