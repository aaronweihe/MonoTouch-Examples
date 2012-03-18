// WARNING
// This file has been generated automatically by MonoDevelop to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import <CoreGraphics/CoreGraphics.h>


@interface GPSViewController : UIViewController {
	UILabel *_latitudeLabel;
	UILabel *_longtitudeLabel;
	UILabel *_altitudeLabel;
	UILabel *_courseLabel;
	UILabel *_speedLabel;
	UILabel *_mgHeadingLabel;
	UILabel *_trueHeadingLabel;
	MKMapView *_mapView;
}

@property (nonatomic, retain) IBOutlet UILabel *latitudeLabel;

@property (nonatomic, retain) IBOutlet UILabel *longtitudeLabel;

@property (nonatomic, retain) IBOutlet UILabel *altitudeLabel;

@property (nonatomic, retain) IBOutlet UILabel *courseLabel;

@property (nonatomic, retain) IBOutlet UILabel *speedLabel;

@property (nonatomic, retain) IBOutlet UILabel *mgHeadingLabel;

@property (nonatomic, retain) IBOutlet UILabel *trueHeadingLabel;

@property (nonatomic, retain) IBOutlet MKMapView *mapView;

@end
