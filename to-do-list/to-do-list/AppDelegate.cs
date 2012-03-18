using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace todolist
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		to_do_listViewController viewController;
		UINavigationController nav;
		DialogViewController rootVC;
		RootElement root;
		UIBarButtonItem btnAdd;
		
		int n = 0;
		
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			root = new RootElement ("To Do List"){new Section()};
			
			rootVC = new DialogViewController (root);
			nav = new UINavigationController(rootVC);
			btnAdd = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			rootVC.NavigationItem.RightBarButtonItem = btnAdd;
			
			btnAdd.Clicked += (sender, e) => {
				++n;
				var task = new Task{Name = "task" + n, DueDate = DateTime.Now};
				var taskElement = new RootElement(task.Name){
					new Section (){
						new EntryElement (task.Name, "Enter task description", task.Description)
					},
					new Section(){
						new DateElement("Due Date", task.DueDate)
					}
				};
				root[0].Add (taskElement);
			};
			
			viewController = new to_do_listViewController ();
			window.RootViewController = nav;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

