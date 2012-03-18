using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace NavDemo
{
	public partial class BrowserViewController : UIViewController
	{
		public BrowserViewController () : base ("BrowserViewController", null)
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
			InitButtonsAndTextField();
			InitBrowser();
			LoadPage("tuaw.com");
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
		
		private void Alert(string caption, string msg)
		{
			using (UIAlertView av = new UIAlertView(caption, msg, null, "OK", null))
			{
				av.Show();
			}
		}
		
		private void InitButtonsAndTextField()
		{
			backButton.TouchUpInside += (sender, e) => {webBrowser.GoBack();};
			fwdButton.TouchUpInside += (sender, e) => {webBrowser.GoForward();};
			refreshButton.Clicked += (sender, e) => {webBrowser.Reload();};
			urlField.ShouldReturn = (textField) => 
			{
				LoadPage(textField.Text.ToString());
				return textField.ResignFirstResponder();
			};
		}
		
		private void InitBrowser()
		{
			webBrowser.LoadStarted += (sender, e) => 
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			};
			webBrowser.LoadFinished+= (sender, e) => 
			{
				urlField.Text = webBrowser.Request.Url.AbsoluteString;
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
			};
			webBrowser.LoadError += (sender, e) => 
			{
				UIApplication .SharedApplication.NetworkActivityIndicatorVisible = false;
				Alert("Browser error", "Web page failed to load: " + e.Error.ToString());
			};
		}
		
		private void LoadPage(string url)
		{
			if(url != "")
			{
				if(!url.StartsWith("http"))
					url = string.Format("http://{0}", url);
				webBrowser.LoadRequest(new NSUrlRequest(new NSUrl(url)));
			}
			urlField.Text = url;
		}
	}
}

