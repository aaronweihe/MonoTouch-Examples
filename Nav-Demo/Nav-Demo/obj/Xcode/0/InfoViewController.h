// WARNING
// This file has been generated automatically by MonoDevelop to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import <CoreGraphics/CoreGraphics.h>


@interface InfoViewController : UIViewController {
	UILabel *_DeviceLabel;
	UILabel *_resolutionLabel;
	UILabel *_frameSizeLabel;
	UISwitch *_statusBarSwitch;
	UILabel *_proximityLabel;
	UILabel *_batteryLabel;
	UILabel *_interactionLabel;
}

@property (nonatomic, retain) IBOutlet UILabel *DeviceLabel;

@property (nonatomic, retain) IBOutlet UILabel *resolutionLabel;

@property (nonatomic, retain) IBOutlet UILabel *frameSizeLabel;

@property (nonatomic, retain) IBOutlet UISwitch *statusBarSwitch;

@property (nonatomic, retain) IBOutlet UILabel *proximityLabel;

@property (nonatomic, retain) IBOutlet UILabel *batteryLabel;

@property (nonatomic, retain) IBOutlet UILabel *interactionLabel;

@end
