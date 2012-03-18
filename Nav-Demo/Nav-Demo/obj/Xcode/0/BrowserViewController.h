// WARNING
// This file has been generated automatically by MonoDevelop to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import <CoreGraphics/CoreGraphics.h>


@interface BrowserViewController : UIViewController {
	UIButton *_backButton;
	UIButton *_fwdButton;
	UIBarButtonItem *_refreshButton;
	UITextField *_urlField;
	UIWebView *_webBrowser;
}

@property (nonatomic, retain) IBOutlet UIButton *backButton;

@property (nonatomic, retain) IBOutlet UIButton *fwdButton;

@property (nonatomic, retain) IBOutlet UIBarButtonItem *refreshButton;

@property (nonatomic, retain) IBOutlet UITextField *urlField;

@property (nonatomic, retain) IBOutlet UIWebView *webBrowser;

@end
