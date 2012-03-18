using System;
using System.Drawing;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace NavDemo
{
	public partial class RootViewController : UITableViewController
	{
		public RootViewController () : base ("RootViewController", null)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Master", "Master");
			
			// Custom initialization
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			
			TableView.Source = new DataSource (this);
			Title = "Aaron's Stuff";
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
		
		class DataSource : UITableViewSource
		{
			static NSString cellIdentifier = new NSString ("CellId");
			RootViewController controller;

			public DataSource (RootViewController controller)
			{
				this.controller = controller;
			}
			
			// Customize the number of sections in the table view.
			public override int NumberOfSections (UITableView tableView)
			{
				return 3;
			}
			
			public override int RowsInSection (UITableView tableview, int section)
			{
				return 1;
			}
			
			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = new UITableViewCell (UITableViewCellStyle.Subtitle, "");
				if ((indexPath.Section == 0) && (indexPath.Row == 0))
					cell.TextLabel.Text = "Web Browser";
				if ((indexPath.Section == 1) && (indexPath.Row == 0))
					cell.TextLabel.Text = "GPS information";
				if ((indexPath.Section == 2) && (indexPath.Row == 0))
					cell.TextLabel.Text = "Device information";
				return cell;
			}

			/*
			// Override to support conditional editing of the table view.
			public override bool CanEditRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				// Return false if you do not want the specified item to be editable.
				return true;
			}
			*/
			
			/*
			// Override to support editing the table view.
			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					// Delete the row from the data source.
					controller.TableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
				} else if (editingStyle == UITableViewCellEditingStyle.Insert) {
					// Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
				}
			}
			*/
			
			/*
			// Override to support rearranging the table view.
			public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
			{
			}
			*/
			
			/*
			// Override to support conditional rearranging of the table view.
			public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the item to be re-orderable.
				return true;
			}
			*/
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				if ((indexPath.Section == 0) && (indexPath.Row == 0))
					controller.NavigationController.PushViewController (new BrowserViewController (), true);
				if ((indexPath.Section == 1) && (indexPath.Row == 0))
					controller.NavigationController.PushViewController (new GPSViewController (), true);
				if ((indexPath.Section == 2) && (indexPath.Row == 0))
					controller.NavigationController.PushViewController (new InfoViewController (), true);
			}
			
			public override string TitleForHeader (UITableView tableView, int section)
			{
				// TODO: Implement - see: http://go-mono.com/docs/index.aspx?link=T%3aMonoTouch.Foundation.ModelAttribute
				switch (section) {
				case 0:
					return "UIKit Example";
				case 1:
					return "CoreLocation & MapKit Example";
				default:
					return "Device information example";
				}
			}
		}
	}
}
