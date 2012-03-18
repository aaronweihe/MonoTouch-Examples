// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace NavDemo
{
	[Register ("InfoViewController")]
	partial class InfoViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel DeviceLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel resolutionLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel frameSizeLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch statusBarSwitch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel proximityLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel batteryLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DeviceLabel != null) {
				DeviceLabel.Dispose ();
				DeviceLabel = null;
			}

			if (resolutionLabel != null) {
				resolutionLabel.Dispose ();
				resolutionLabel = null;
			}

			if (frameSizeLabel != null) {
				frameSizeLabel.Dispose ();
				frameSizeLabel = null;
			}

			if (statusBarSwitch != null) {
				statusBarSwitch.Dispose ();
				statusBarSwitch = null;
			}

			if (proximityLabel != null) {
				proximityLabel.Dispose ();
				proximityLabel = null;
			}

			if (batteryLabel != null) {
				batteryLabel.Dispose ();
				batteryLabel = null;
			}
		}
	}
}
