//
//  PhotoViewerViewController.m
//  PhotoViewer
//
//  Created by Ing. Efrén Mazatán Cruz on 12/02/11.
//  Copyright 2011 ITESM. All rights reserved.
//

#import "PhotoViewerViewController.h"

@implementation PhotoViewerViewController

@synthesize image01,image02;
@synthesize pageControl;
-(IBAction)cambiar{
		
	
	[self.view removeFromSuperview];
	

}

/*
// The designated initializer. Override to perform setup that is required before the view is loaded.
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil {
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}
*/

/*
// Implement loadView to create a view hierarchy programmatically, without using a nib.
- (void)loadView {
}
*/



// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad {
	[super viewDidLoad];
	[pageControl addTarget:self action:@selector(pageSwitch:) forControlEvents:UIControlEventTouchUpInside];
	tmpImage = image02;
	[image01 setHidden:NO];
	[image02 setHidden:YES];
}

-(void)pageSwitch:(UIPageControl *)sender{
	
	NSInteger index = sender.currentPage;
	NSLog(@"%d",index);
	switch (index) {
		case 0:
			tmpImage.image=[UIImage imageNamed:@"image01.png"];
			break;
		case 1:
			tmpImage.image=[UIImage imageNamed:@"image02.png"];
			break;
		case 2:
			tmpImage.image=[UIImage imageNamed:@"Corona.png"];
			break;
		case 3:
			tmpImage.image=[UIImage imageNamed:@"Modelo.png"];
			break;
		case 4:
			tmpImage.image=[UIImage imageNamed:@"Victoria.png"];
			break;
			
		case 5:
			tmpImage.image=[UIImage imageNamed:@"Barrilito.png"];
			break;
		case 6:
			tmpImage.image=[UIImage imageNamed:@"leon.png"];
			break;
			
		case 7:
			tmpImage.image=[UIImage imageNamed:@"Estrella.png"];
			break;
		case 8:
			tmpImage.image=[UIImage imageNamed:@"Pacifico_light.png"];
			break;
		case 9:
			tmpImage.image=[UIImage imageNamed:@"Corona_light.png"];
			break;
		case 10:
			tmpImage.image=[UIImage imageNamed:@"Modelo_ligth.png"];
			break;
		default:
			break;
	}
	if (tmpImage.tag==1) {
		tmpImage=image02;
		bgImage=image01;
	}else {
		tmpImage=image01;
		bgImage=image02;
	}
	
	[UIView beginAnimations:@"flipping view" context:nil];
	[UIView	setAnimationDuration:0.5];
	[UIView setAnimationCurve:UIViewAnimationCurveEaseInOut];
	[UIView setAnimationTransition: UIViewAnimationTransitionFlipFromLeft forView:tmpImage cache:YES];	
	[tmpImage setHidden:YES];
	[UIView commitAnimations];
	
	[UIView beginAnimations:@"flipping view" context:nil];
	[UIView	setAnimationDuration:0.5];
	[UIView setAnimationCurve:UIViewAnimationCurveEaseInOut];
	[UIView setAnimationTransition: UIViewAnimationTransitionFlipFromRight forView:bgImage cache:YES];
	
	[bgImage setHidden:NO];
	[UIView commitAnimations];
}

/*
// Override to allow orientations other than the default portrait orientation.
- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation {
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
*/

- (void)didReceiveMemoryWarning {
	// Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
	
	// Release any cached data, images, etc that aren't in use.
}

- (void)viewDidUnload {
	// Release any retained subviews of the main view.
	// e.g. self.myOutlet = nil;
}


- (void)dealloc {
	[image01 release];
	[image02 release];
	[pageControl release];
    [super dealloc];
}

@end
