    //
//  addController.m
//  3Parcial
//
//  Created by aCADc14 m03 on 09/04/11.
//  Copyright 2011 ITESM Campus Zacatecas. All rights reserved.
//

#import "addController.h"


@implementation addController
@synthesize ano,sinopsis,tittless,anodes,sinopsisdes,tittledes,add,dbName,tipo;



-(void) checkAndCreateDatabase{
	NSArray *documentPaths = 
	NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
	NSString *documentsDir = [documentPaths objectAtIndex:0];
	NSString *databasePath = [documentsDir stringByAppendingPathComponent:@"db.sql"];
	self.dbName=databasePath;
	NSLog(databasePath);
	BOOL success;
	
	NSFileManager *fileManager = [NSFileManager defaultManager];
	
	success = [fileManager fileExistsAtPath:databasePath];
	
	// If the database already exists then return without doing anything
	if(success) return;
	
	// If not then proceed to copy the database from the application to the users filesystem
	
	// Get the path to the database in the application package
	NSString *databasePathFromApp = 
	[[[NSBundle mainBundle] resourcePath] stringByAppendingPathComponent:@"movies.sql"];
	
	// Copy the database from the package to the users filesystem
	[fileManager copyItemAtPath:databasePathFromApp toPath:databasePath error:nil];
	
	[fileManager release];
}

-(IBAction)eliminar{

	NSString *sql = [NSString stringWithFormat:@"delete from movie;"]; 
	char *err;
	if (sqlite3_open([self.dbName UTF8String], &db)==SQLITE_OK) {
		sqlite3_exec(db, [sql UTF8String], nil, nil, &err);
	}
	

}

-(IBAction)insertar{
	
	NSUserDefaults *defaults=[NSUserDefaults standardUserDefaults];
	NSString *strTittle=[defaults objectForKey:@"language"];
		NSString *a,*b,*c,*d;
	a=self.tittledes.text;
	b=self.sinopsisdes.text;
	c=self.anodes.text;
	
	if([strTittle isEqualToString:@"Espanol"]){
		
		UIAlertView *alert=[[UIAlertView alloc] initWithTitle:@"Aviso SQL" message:@"Receta Agregada" delegate:self cancelButtonTitle:@"Ok" otherButtonTitles:nil];
		[alert addButtonWithTitle:@"Cerrar"];
		[alert show];
		[alert release];
	
				
		
		NSString *sql = [NSString stringWithFormat:@"INSERT INTO Pelicula(titulo,ano,sinopsis) VALUES('%@','%@','%@');",a,b,c]; 
		char *err;
		if (sqlite3_open([self.dbName UTF8String], &db)==SQLITE_OK) {
			sqlite3_exec(db, [sql UTF8String], nil, nil, &err);
		}
		
	}
	else {
		d=self.tipo.text;
		
		NSString *sql =  [NSString stringWithFormat:@"INSERT INTO Movie(title,year,synopsis,tipo) VALUES('%@','%@','%@','%@');",a,b,c,d];
		char *err;
		if (sqlite3_open([self.dbName UTF8String], &db)==SQLITE_OK) {
			sqlite3_exec(db, [sql UTF8String], nil, nil, &err);
			UIAlertView *alert=[[UIAlertView alloc] initWithTitle:@"Sql Advertise" message:@"Inserted Movie" delegate:self cancelButtonTitle:@"Ok" otherButtonTitles:nil];
			[alert addButtonWithTitle:@"Close"];
			[alert show];
			[alert release];
		}
		
	}
	

}

-(void)releaseOutlets{

	self.ano=nil;
	self.sinopsis=nil;
	self.tittless=nil;
	self.add=nil;
}

// The designated initializer.  Override if you create the controller programmatically and want to perform customization that is not appropriate for viewDidLoad.
/*
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil {
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization.
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
	[self checkAndCreateDatabase];
}


/*
// Override to allow orientations other than the default portrait orientation.
- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation {
    // Return YES for supported orientations.
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
*/

- (void)didReceiveMemoryWarning {
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc. that aren't in use.
}

- (void)viewDidUnload {
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}


- (void)dealloc {
    [super dealloc];
	[self releaseOutlets];
}


@end
