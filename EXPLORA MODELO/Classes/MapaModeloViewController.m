//
//  MapaModeloViewController.m
//  MapaModelo
//
//  Created by Ramon Badillo on 4/28/11.
//  Copyright 2011 ITESM. All rights reserved.
//

#import "MapaModeloViewController.h"
#import "MyAnnotation.h"

@implementation MapaModeloViewController
@synthesize tableview;
@synthesize mapView;
@synthesize Nombre,Latitud,Longitud,Direccion,Tipo;
@synthesize dbName;
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

//tipoGenerico text, direccion text, nombre text, marcas text, longitud double, latitud double



-(void)queryNombreDB{
	NSString *sql = @"SELECT title FROM Movie;";
	NSMutableArray *array = [[NSMutableArray alloc]init];
	if (sqlite3_open([self.dbName UTF8String], &db)==SQLITE_OK) {
		sqlite3_stmt *stmt;
		sqlite3_prepare(db, [sql UTF8String], -1, &stmt, nil);
		while (sqlite3_step(stmt)==SQLITE_ROW) {
			char *field1=(char *)sqlite3_column_text(stmt, 0);	
			NSString *field1String = [[NSString alloc] initWithUTF8String:field1];
			[array addObject:field1String];
		}
		sqlite3_finalize(stmt);
	}
	sqlite3_close(db);
	[array addObject:@"Cerveceria Zacatecas"];
	[array addObject:@"Cerveceria algo"];
	[array addObject:@"Cerveceria algo"];

	self.Nombre=array;
	[array release];
}

-(void)queryLatitudDB{
	NSString *sql = @"SELECT year FROM Movie;";
	NSMutableArray *array = [[NSMutableArray alloc]init];
	if (sqlite3_open([self.dbName UTF8String], &db)==SQLITE_OK) {
		sqlite3_stmt *stmt;
		sqlite3_prepare(db, [sql UTF8String], -1, &stmt, nil);
		while (sqlite3_step(stmt)==SQLITE_ROW) {
			char *field1=(char *)sqlite3_column_text(stmt, 0);	
			NSString *field1String = [[NSString alloc] initWithUTF8String:field1];
			[array addObject:field1String];
		}
		sqlite3_finalize(stmt);
	}
	sqlite3_close(db);
	[array addObject:@"22.970425"];
	[array addObject:@"19.440765"];
	[array addObject:@"20.440765"];
	self.Latitud=array;
	[array release];
}


-(void)queryLongitudDB{
	NSString *sql = @"SELECT synopsis FROM Movie;";
	NSMutableArray *array = [[NSMutableArray alloc]init];
	if (sqlite3_open([self.dbName UTF8String], &db)==SQLITE_OK) {
		sqlite3_stmt *stmt;
		sqlite3_prepare(db, [sql UTF8String], -1, &stmt, nil);
		while (sqlite3_step(stmt)==SQLITE_ROW) {
			char *field1=(char *)sqlite3_column_text(stmt, 0);	
			NSString *field1String = [[NSString alloc] initWithUTF8String:field1];
			[array addObject:field1String];
		}
		sqlite3_finalize(stmt);
	}
	sqlite3_close(db);
	[array addObject:@"-102.706482"];
	[array addObject:@"-99.189516"];
	[array addObject:@"-99.189516"];
	self.Longitud=array;

	[array release];
}


-(void)queryDireccionDB{
	NSString *sql = @"SELECT tipo FROM Movie;";
	NSMutableArray *array = [[NSMutableArray alloc]init];
	if (sqlite3_open([self.dbName UTF8String], &db)==SQLITE_OK) {
		sqlite3_stmt *stmt;
		sqlite3_prepare(db, [sql UTF8String], -1, &stmt, nil);
		while (sqlite3_step(stmt)==SQLITE_ROW) {
			char *field1=(char *)sqlite3_column_text(stmt, 0);	
			NSString *field1String = [[NSString alloc] initWithUTF8String:field1];
			[array addObject:field1String];
		}
		sqlite3_finalize(stmt);
	}
	sqlite3_close(db);
	[array addObject:@"Extra"];
	[array addObject:@"Modelorama"];
	[array addObject:@"Cerveceria"];
	self.Direccion=array;
	for (NSString *a in array) {
		NSLog(@"%@",a);
	}
	[array release];
}






-(void) checkAndCreateDatabase{
	NSArray *documentPaths = 
	NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
	NSString *documentsDir = [documentPaths objectAtIndex:0];
	NSString *databasePath = [documentsDir stringByAppendingPathComponent:@"db.sql"];
	self.dbName=databasePath;
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




// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad {
    [super viewDidLoad];
	//[self checkAndCreateDatabase];
	[self queryNombreDB];
	[self queryLongitudDB];
	[self queryLatitudDB];
	[self queryDireccionDB];
	[self loadOurAnnotations];
	//mapView.showsUserLocation=YES;
	mapView.mapType=MKMapTypeHybrid;
	MKCoordinateRegion region;
	region.center.latitude=22.970425;
	region.center.longitude=-102.706482;
	region.span.latitudeDelta=10;
	region.span.longitudeDelta=10;
	[mapView setRegion:region animated:YES]; 
	[mapView setDelegate:self];
	
	
}



/*
// Override to allow orientations other than the default portrait orientation.
- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation {
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
*/




-(void)loadOurAnnotations
{
	CLLocationCoordinate2D workingCoordinate;
	puntos=[[NSMutableArray alloc] init];

	int i=0;
	for (NSString *N in Nombre) {
		//NSLog(@"%@",N);
		double lat =  [[Latitud objectAtIndex:i] doubleValue];
		//NSLog(@"%d",lat);
		double lon =  [[Longitud objectAtIndex:i] doubleValue];
		//NSLog(@"%d",lon);
		workingCoordinate.latitude = lat;
		workingCoordinate.longitude = lon;
		MyAnnotation *ann = [[MyAnnotation alloc] initWithCoordinate:workingCoordinate];
		[ann setTitle:N];
		//NSLog(@"%@",[Direccion objectAtIndex:i]);
		[ann setSubtitle:[Direccion objectAtIndex:i]];
		if ([[Direccion objectAtIndex:i]isEqualToString:@"Cerveceria"]) {
			[ann setAnnotationType:AnnotationTypeCervecerias];
		}if ([[Direccion objectAtIndex:i]isEqualToString:@"Extra"]) {
			[ann setAnnotationType:AnnotationTypeExtra];
		}else {//if ([[Direccion objectAtIndex:i]isEqualToString:@"Modelorama"]){
			[ann setAnnotationType:AnnotationTypeModelorama];
		}
		//NSLog(@"adsfased%@",ann.title);
		[puntos addObject:ann];
		i++;
	}
	[mapView addAnnotations:puntos];
	
}







- (void)mapView:(MKMapView *)mapView regionWillChangeAnimated:(BOOL)animated
{
	
}
- (void)mapView:(MKMapView *)mapView regionDidChangeAnimated:(BOOL)animated
{
	
}


- (MyAnnotationView *)mapView:(MKMapView *)mapView viewForAnnotation:(id <MKAnnotation>)annotation{
	MyAnnotationView *annotationView = nil;
		// determine the type of annotation, and produce the correct type of annotation view for it.
	MyAnnotation* myAnnotation = (MyAnnotation *)annotation;
	
	
	
	if(myAnnotation.annotationType == AnnotationTypeExtra)
	{
		NSString* identifier = @"Extra";
		MyAnnotationView *newAnnotationView = (MyAnnotationView *)[self.mapView dequeueReusableAnnotationViewWithIdentifier:identifier];
		
		if(nil == newAnnotationView)
		{
			newAnnotationView = [[[MyAnnotationView alloc] initWithAnnotation:myAnnotation reuseIdentifier:identifier] autorelease];
		}
		
		annotationView = newAnnotationView;
	}
	else if(myAnnotation.annotationType == AnnotationTypeModelorama)
	{
		NSString* identifier = @"Modelorama";
		
		MyAnnotationView *newAnnotationView = (MyAnnotationView *)[self.mapView dequeueReusableAnnotationViewWithIdentifier:identifier];
		
		if(nil == newAnnotationView)
		{
			newAnnotationView = [[[MyAnnotationView alloc] initWithAnnotation:myAnnotation reuseIdentifier:identifier] autorelease];
		}
		
		annotationView = newAnnotationView;
	}
	else if(myAnnotation.annotationType == AnnotationTypeCervecerias)
	{
		NSString* identifier = @"Cerveceria";
		
		MyAnnotationView *newAnnotationView = (MyAnnotationView *)[self.mapView dequeueReusableAnnotationViewWithIdentifier:identifier];
		
		if(nil == newAnnotationView)
		{
			newAnnotationView = [[[MyAnnotationView alloc] initWithAnnotation:myAnnotation reuseIdentifier:identifier] autorelease];
		}
		
		annotationView = newAnnotationView;
	}
	
	[annotationView setEnabled:YES];
	[annotationView setCanShowCallout:YES];
	
	return annotationView;
	
}




- (void)mapView:(MKMapView *)mapView annotationView:(MKAnnotationView *)view calloutAccessoryControlTapped:(UIControl *)control
{
	
}

#pragma mark Table view methods

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView{
    return 1;
}

- (NSString *)tableView:(UITableView *)tableView titleForHeaderInSection:(NSInteger)section
{
	return @"Puntos Modelo";
	
}

// Customize the number of rows in the table view.
- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section 
{
	return Nombre.count;
}//


// Customize the appearance of table view cells.
- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath 
{    

    static NSString *CellIdentifier = @"Cell";
    
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIdentifier];
	if (cell == nil) {
        cell = [[[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:CellIdentifier] autorelease];
    }
	
	NSMutableArray *annotations = [[NSMutableArray alloc] init];
	for(MyAnnotation *annotation in [mapView annotations])
	{
		[annotations addObject:annotation];
	}
	[cell textLabel].text=[[annotations objectAtIndex:indexPath.row] title];
	return cell;
	
	
	 
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
	
	for(MyAnnotation *annotation in [mapView annotations])
	{
		if([[[[tableView cellForRowAtIndexPath:indexPath] textLabel] text] isEqualToString:[annotation title]])
		{	
			
			[mapView setRegion:MKCoordinateRegionMake([annotation coordinate], MKCoordinateSpanMake(.01, .01)) animated:YES];
		}
	}
	 
}

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
