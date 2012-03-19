// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace UtilityDemo
{
	[Register ("FlipsideViewController")]
	partial class FlipsideViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField temperatureTextBox { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISegmentedControl conversionSelectionButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton convertButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView activityIndicator { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel resultLabel { get; set; }

		[Action ("done:")]
		partial void done (MonoTouch.UIKit.UIBarButtonItem sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (temperatureTextBox != null) {
				temperatureTextBox.Dispose ();
				temperatureTextBox = null;
			}

			if (conversionSelectionButton != null) {
				conversionSelectionButton.Dispose ();
				conversionSelectionButton = null;
			}

			if (convertButton != null) {
				convertButton.Dispose ();
				convertButton = null;
			}

			if (activityIndicator != null) {
				activityIndicator.Dispose ();
				activityIndicator = null;
			}

			if (resultLabel != null) {
				resultLabel.Dispose ();
				resultLabel = null;
			}
		}
	}
}
