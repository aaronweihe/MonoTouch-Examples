using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using UtilityDemo.www.w3schools.com;

namespace UtilityDemo
{
	public partial class FlipsideViewController : UIViewController
	{
		private TempConvert converter;
		public FlipsideViewController () : base ("FlipsideViewController", null)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			resultLabel.Text = "";
			activityIndicator.HidesWhenStopped = true;
			converter = new TempConvert();
			convertButton.TouchUpInside += HandleConvertButtonTouchUpInside;
			convertButton.TouchUpInside += AnyNonTextControlTouched;
			conversionSelectionButton.ValueChanged += AnyNonTextControlTouched;
		}
		
		private void AnyNonTextControlTouched(object sender, EventArgs e)
		{
			temperatureTextBox.ResignFirstResponder();
		}

		void HandleConvertButtonTouchUpInside (object sender, EventArgs e)
		{
			resultLabel.Text="";
			double inputTemp;
			if(double.TryParse(temperatureTextBox.Text, out inputTemp))
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible =true;
				activityIndicator.StartAnimating();
				if(conversionSelectionButton.SelectedSegment==0)
					converter.BeginCelsiusToFahrenheit(temperatureTextBox.Text, FinishedConversion, true);
				else
					converter.BeginFahrenheitToCelsius(temperatureTextBox.Text, FinishedConversion, false);
			}
			else
				resultLabel.Text = "Invalid input temperature";
		}
		
		private void FinishedConversion(IAsyncResult asyncResult)
		{
			var fromCelsius = (bool)asyncResult.AsyncState;
			var answer = fromCelsius ? converter.EndCelsiusToFahrenheit(asyncResult) :converter.EndFahrenheitToCelsius(asyncResult);
			
			InvokeOnMainThread(() => {
				resultLabel.Text = string.Format(fromCelsius ? "{0}°C is {1:F2}°F" : "{0}°F is {1:F2}°C", temperatureTextBox.Text, Convert.ToDouble(answer));
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				activityIndicator.StopAnimating();
			});
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
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
		
		partial void done (UIBarButtonItem sender)
		{
			if (Done != null)
				Done (this, EventArgs.Empty);
		}
		
		public event EventHandler Done;
	}
}

