// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace NavDemo
{
	[Register ("BrowserViewController")]
	partial class BrowserViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton backButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton fwdButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem refreshButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField urlField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIWebView webBrowser { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (backButton != null) {
				backButton.Dispose ();
				backButton = null;
			}

			if (fwdButton != null) {
				fwdButton.Dispose ();
				fwdButton = null;
			}

			if (refreshButton != null) {
				refreshButton.Dispose ();
				refreshButton = null;
			}

			if (urlField != null) {
				urlField.Dispose ();
				urlField = null;
			}

			if (webBrowser != null) {
				webBrowser.Dispose ();
				webBrowser = null;
			}
		}
	}
}
