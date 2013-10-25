//
//  PublicidadViewController.m
//  Publicidad
//
//  Created by aCADc16 m03 on 29/04/11.
//  Copyright 2011 ITESM Campus Zacatecas. All rights reserved.
//

#import "PublicidadViewController.h"
#import "PhotoViewerViewController.h"

@implementation PublicidadViewController

@synthesize webView;
@synthesize btnModelo, btnConsumo, btnMarcas;
@synthesize toolBtn;
@synthesize address1, address2,  addreess3;
@synthesize photos;
//NSString address1;



-(IBAction)goToModelo :(UIBarButtonItem *)sender{
	NSURL *url =[NSURL URLWithString:@"http://www.gmodelo.com.mx/"];
	NSURLRequest *requestObj = [NSURLRequest requestWithURL:url];
	[webView loadRequest:requestObj];

		}


-(IBAction)goToConsumo :(UIBarButtonItem *)sender{
	NSURL *url =[NSURL URLWithString:@"http://www.gmodelo.com.mx/index-8.asp"];
	NSURLRequest *requestObj = [NSURLRequest requestWithURL:url];
	[webView loadRequest:requestObj];	
}

-(IBAction)goToMarcas :(UIBarButtonItem *)sender{
	NSURL *url =[NSURL URLWithString:@"http://www.gmodelo.com.mx/index-2.asp"];
	NSURLRequest *requestObj = [NSURLRequest requestWithURL:url];
	[webView loadRequest:requestObj];
}

-(IBAction)goToEventos :(UIBarButtonItem *)sender{
	NSURL *url =[NSURL URLWithString:@"http://www.gmodelo.com.mx/index-7.asp?go=eventos"];
	NSURLRequest *requestObj = [NSURLRequest requestWithURL:url];
	[webView loadRequest:requestObj];
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
	self.photos=[[PhotoViewerViewController alloc] initWithNibName:@"PhotoViewerViewController" bundle:nil];
	[UIView beginAnimations:@"flipping view" context:nil]; 
	[UIView setAnimationDuration:1]; 
	[UIView setAnimationCurve:UIViewAnimationCurveEaseInOut]; 
	[UIView setAnimationTransition: UIViewAnimationTransitionCurlDown
						   forView:self.view cache:YES];	
	[self.view addSubview:self.photos.view];
	[UIView commitAnimations];
	self.photos=nil;
	
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
    [super dealloc];
}

@end
