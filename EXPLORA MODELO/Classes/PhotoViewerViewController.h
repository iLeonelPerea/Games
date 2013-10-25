//
//  PhotoViewerViewController.h
//  PhotoViewer
//
//  Created by Ing. Efrén Mazatán Cruz on 12/02/11.
//  Copyright 2011 ITESM. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface PhotoViewerViewController : UIViewController {
	IBOutlet UIImageView *image01;
	IBOutlet UIImageView *image02;
	IBOutlet UIPageControl *pageControl;
	UIImageView *tmpImage,*bgImage;
}
@property (nonatomic,retain) UIImageView *image01;
@property (nonatomic,retain) UIImageView *image02;
@property (nonatomic,retain) UIPageControl *pageControl;
-(IBAction)cambiar;
@end

