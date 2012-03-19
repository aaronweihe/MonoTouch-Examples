using System;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.IO;

using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Mono.Data.Sqlite;

namespace MasterDetailDemo
{
	public partial class RootViewController : UITableViewController
	{
		SqliteConnection connection;
		string dbPath;
		List<Customer> customerList;
		
		public RootViewController () : base ("RootViewController", null)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Master", "Master");
			
			// Custom initialization
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			
			//Create the DB and insert some rows
			var document = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			dbPath = Path.Combine (document, "NavTestDB.db3");
			var dbExists = File.Exists (dbPath);
			if (!dbExists)
				SqliteConnection.CreateFile (dbPath);
			connection = new SqliteConnection ("Data Source=" + dbPath);
			try {
				connection.Open ();
				using (SqliteCommand cmd = connection.CreateCommand()) {
					cmd.CommandType = CommandType.Text;
					if (!dbExists) {
						const string TblColDefs = " Customers (CustID INTEGER NOT NULL, FirstName ntext, LastName ntext, Town ntext)";
						const string TblCols = " Customers (CustID, FirstName, LastName, Town)";
						string[] statements = {
							"CREATE TABLE" + TblColDefs, 
							"INSERT INTO" + TblCols + "VALUES (1, 'John', 'Smith', 'Manchester')",
							"INSERT INTO" + TblCols + "VALUES (2, 'John', 'Doe', 'Dorchester')",
							"INSERT INTO" + TblCols + "VALUES (3, 'Fred', 'Bloggs', 'Winchester')",
							"INSERT INTO" + TblCols + "VALUES (4, 'Walter P.', 'Jabsco', 'Ilchester')",
							"INSERT INTO" + TblCols + "VALUES (5, 'Jane', 'Smith', 'Silchester')",
							"INSERT INTO" + TblCols + "VALUES (6, 'Raymond', 'Luxury-Yacht', 'Colchester')"};
						foreach (string stmt in statements) {
							cmd.CommandText = stmt;
							cmd.ExecuteNonQuery ();
						}
					}
					customerList = new List<Customer> ();
					cmd.CommandText = "SELECT CustID, FirstName, LastName, Town FROM Customers ORDER BY LastName";
					using (SqliteDataReader reader = cmd.ExecuteReader()) {
						while (reader.Read()) {
							var cust = new Customer
							{
								CustID = Convert.ToInt32 (reader ["CustID"]),
								FirstName = reader ["FirstName"].ToString (),
								LastName = reader ["LastName"].ToString (),
								Town = reader ["Town"].ToString ()
							};
							customerList.Add (cust);
						}
					}
				}
			} catch (Exception) {
				connection.Close ();
			}
				
			TableView.Source = new DataSource (this);
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
			using (SqliteCommand cmd = connection.CreateCommand()) {
				cmd.CommandText = "DROP TABLE IF EXISTS Customers";
				cmd.CommandType = CommandType.Text;
				connection.Open ();
				cmd.ExecuteNonQuery ();
				connection.Close ();
			}
			File.Delete (dbPath);
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
				return 1;
			}
			
			public override int RowsInSection (UITableView tableview, int section)
			{
				return controller.customerList.Count;
			}
			
			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				string cellIdentifier = "Cell";
				var cell = tableView.DequeueReusableCell (cellIdentifier);
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Subtitle, cellIdentifier);
					cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
				}
				
				// Configure the cell.
				var cust = controller.customerList [indexPath.Row];
				cell.TextLabel.Text = string.Format ("{0} {1}", cust.FirstName, cust.LastName);
				cell.DetailTextLabel.Text = cust.Town;
				//cell.TextLabel.Text = NSBundle.MainBundle.LocalizedString ("Detail", "Detail");
				
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
				return;
			}
			
			private void InfoAlert (string msg)
			{
				using (UIAlertView av = new UIAlertView("Info", msg, null, "OK", null)) {
					av.Show ();
				}
			}
			
			public override void AccessoryButtonTapped(UITableView tableView, NSIndexPath indexPath)
			{
				var cust = controller.customerList[indexPath.Row];
				InfoAlert(string.Format("{0} {1} has ID {2}", cust.FirstName, cust.LastName, cust.CustID));
			}
		}
	}
}
