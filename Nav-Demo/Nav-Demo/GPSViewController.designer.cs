// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace NavDemo
{
	[Register ("GPSViewController")]
	partial class GPSViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel latitudeLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel longtitudeLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel altitudeLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel courseLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel speedLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel mgHeadingLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel trueHeadingLabel { get; set; }

		[Outlet]
		MonoTouch.MapKit.MKMapView mapView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (latitudeLabel != null) {
				latitudeLabel.Dispose ();
				latitudeLabel = null;
			}

			if (longtitudeLabel != null) {
				longtitudeLabel.Dispose ();
				longtitudeLabel = null;
			}

			if (altitudeLabel != null) {
				altitudeLabel.Dispose ();
				altitudeLabel = null;
			}

			if (courseLabel != null) {
				courseLabel.Dispose ();
				courseLabel = null;
			}

			if (speedLabel != null) {
				speedLabel.Dispose ();
				speedLabel = null;
			}

			if (mgHeadingLabel != null) {
				mgHeadingLabel.Dispose ();
				mgHeadingLabel = null;
			}

			if (trueHeadingLabel != null) {
				trueHeadingLabel.Dispose ();
				trueHeadingLabel = null;
			}

			if (mapView != null) {
				mapView.Dispose ();
				mapView = null;
			}
		}
	}
}
