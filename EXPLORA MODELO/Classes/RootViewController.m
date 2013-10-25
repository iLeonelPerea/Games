//
//  RootViewController.m
//  3Parcial
//
//  Created by aCADc14 m03 on 09/04/11.
//  Copyright 2011 ITESM Campus Zacatecas. All rights reserved.
//

#import "RootViewController.h"


@implementation RootViewController
@synthesize language,movieList,addControl,dbName,ano,sinopsis,titles,checarControl;
@synthesize mapa,publicidad;

//--------base de datos
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

//Metodo para el query

-(void)queryDBSpanish{
	titles=[[NSMutableArray alloc] init];
	ano=[[NSMutableArray alloc] init];
	sinopsis=[[NSMutableArray alloc] init];
	NSString *sql = @"SELECT titulo,ano,sinopsis FROM Pelicula;";
	if (sqlite3_open([self.dbName UTF8String], &db)==SQLITE_OK) {
				sqlite3_stmt *stmt;
		sqlite3_prepare(db, [sql UTF8String], -1, &stmt, nil);
		while (sqlite3_step(stmt)==SQLITE_ROW) {
			NSLog(@"trying to read..");

			char *field1=(char *)sqlite3_column_text(stmt, 0);	
			NSString *field1String = [[NSString alloc] initWithUTF8String:field1];
			[titles addObject:field1String];
			
			char *field2=(char *)sqlite3_column_text(stmt, 1);	
			NSString *field2String = [[NSString alloc] initWithUTF8String:field2];
			[ano addObject:field2String];
			
			char *field3=(char *)sqlite3_column_text(stmt, 2);	
			NSString *field3String = [[NSString alloc] initWithUTF8String:field3];
			[sinopsis addObject:field3String];
		}
		sqlite3_finalize(stmt);
	}
	sqlite3_close(db);
	[titles addObject:@"Nueva Receta"];
	[titles addObject:@"Mapas"];
	
}

-(void)queryDBEnglish{
	NSLog(@"Eng");
	titles=[[NSMutableArray alloc] init];
	ano=[[NSMutableArray alloc] init];
	sinopsis=[[NSMutableArray alloc] init];
	NSString *sql = @"SELECT title,year,synopsis,tipo FROM Movie;";
	if (sqlite3_open([self.dbName UTF8String], &db)==SQLITE_OK) {
		sqlite3_stmt *stmt;
		sqlite3_prepare(db, [sql UTF8String], -1, &stmt, nil);
		while (sqlite3_step(stmt)==SQLITE_ROW) {
			char *field1=(char *)sqlite3_column_text(stmt, 0);	
			NSString *field1String = [[NSString alloc] initWithUTF8String:field1];
			[titles addObject:field1String];
			
			char *field2=(char *)sqlite3_column_text(stmt, 1);	
			NSString *field2String = [[NSString alloc] initWithUTF8String:field2];
			[ano addObject:field2String];
			
			char *field3=(char *)sqlite3_column_text(stmt, 3);	
			NSString *field3String = [[NSString alloc] initWithUTF8String:field3];
			[sinopsis addObject:field3String];
			
		}
		sqlite3_finalize(stmt);
	}
	sqlite3_close(db);
	[titles addObject:@"Nueva marca"];
	[titles addObject:@"Mapas"];
	[titles addObject:@"Publicidad"];
}




//------------------------------------------------------------


#pragma mark -
#pragma mark View lifecycle



-(void) checarSetings{
	NSUserDefaults *defaults=[NSUserDefaults standardUserDefaults];
	NSString *strTittle=[defaults objectForKey:@"language"];
	if([strTittle isEqualToString:@"Espanol"]){
		checarControl.ano.text=@"Ingredientes";
		checarControl.tittless.text=@"Receta";
		checarControl.sinopsis.text=@"Preparacion";
	}
	else {
		checarControl.ano.text=@"Latitud";
		checarControl.tittless.text=@"Localizacion";
		checarControl.sinopsis.text=@"Longitud";
	}

}

-(void) addSetings{
	NSUserDefaults *defaults=[NSUserDefaults standardUserDefaults];
	NSString *strTittle=[defaults objectForKey:@"language"];
	if([strTittle isEqualToString:@"Espanol"]){
		addControl.ano.text=@"Ingredientes";
		addControl.tittless.text=@"Receta";
		addControl.sinopsis.text=@"Preparacion";
		addControl.add.titleLabel.text=@"Receta";
	}
	else {
		addControl.ano.text=@"Latitud";
		addControl.tittless.text=@"Localizacion";
		addControl.sinopsis.text=@"Longitud";
		addControl.add.titleLabel.text=@"Agregar";
	}
	
}



- (void)viewDidLoad {
    [super viewDidLoad];
	
	[self checkAndCreateDatabase];
	
	NSUserDefaults *defaults=[NSUserDefaults standardUserDefaults];
	NSString *strTittle=[defaults objectForKey:@"language"];
	if([strTittle isEqualToString:@"Espanol"]){
		self.title=@"Recetario";
		
	[self queryDBSpanish];
	}
	else {
		NSLog(@"db");

		self.title=@"Mapas";
		[self queryDBEnglish];
	}

	
	NSLog(@"%d",titles.count);

    // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
    // self.navigationItem.rightBarButtonItem = self.editButtonItem;
}



- (void)viewWillAppear:(BOOL)animated {
    [super viewWillAppear:animated];
	[self viewDidLoad];
	[self.tableView reloadData];
}

/*
- (void)viewDidAppear:(BOOL)animated {
    [super viewDidAppear:animated];
}
*/
/*
- (void)viewWillDisappear:(BOOL)animated {
	[super viewWillDisappear:animated];
}
*/
/*
- (void)viewDidDisappear:(BOOL)animated {
	[super viewDidDisappear:animated];
}
*/

/*
 // Override to allow orientations other than the default portrait orientation.
- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation {
	// Return YES for supported orientations.
	return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
 */


#pragma mark -
#pragma mark Table view data source

// Customize the number of sections in the table view.
- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView {
    return 1;
}


// Customize the number of rows in the table view.
- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return titles.count;
}


// Customize the appearance of table view cells.
- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    
    static NSString *CellIdentifier = @"Cell";
    
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIdentifier];
    if (cell == nil) {
        cell = [[[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:CellIdentifier] autorelease];
    }
    
	
	
	[cell textLabel].text=[titles objectAtIndex:indexPath.row];
	
		

   return cell;
}


/*
// Override to support conditional editing of the table view.
- (BOOL)tableView:(UITableView *)tableView canEditRowAtIndexPath:(NSIndexPath *)indexPath {
    // Return NO if you do not want the specified item to be editable.
    return YES;
}
*/


/*
// Override to support editing the table view.
- (void)tableView:(UITableView *)tableView commitEditingStyle:(UITableViewCellEditingStyle)editingStyle forRowAtIndexPath:(NSIndexPath *)indexPath {
    
    if (editingStyle == UITableViewCellEditingStyleDelete) {
        // Delete the row from the data source.
        [tableView deleteRowsAtIndexPaths:[NSArray arrayWithObject:indexPath] withRowAnimation:UITableViewRowAnimationFade];
    }   
    else if (editingStyle == UITableViewCellEditingStyleInsert) {
        // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
    }   
}
*/


/*
// Override to support rearranging the table view.
- (void)tableView:(UITableView *)tableView moveRowAtIndexPath:(NSIndexPath *)fromIndexPath toIndexPath:(NSIndexPath *)toIndexPath {
}
*/


/*
// Override to support conditional rearranging of the table view.
- (BOOL)tableView:(UITableView *)tableView canMoveRowAtIndexPath:(NSIndexPath *)indexPath {
    // Return NO if you do not want the item to be re-orderable.
    return YES;
}
*/


#pragma mark -
#pragma mark Table view delegate

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath {
	
	if([titles objectAtIndex:indexPath.row]==@"Nueva marca" || [titles objectAtIndex:indexPath.row]==@"Nueva Receta"){
		if(self.addControl==nil){
		
		addController *view=[[addController alloc] initWithNibName:@"add" bundle:[NSBundle mainBundle]];
			self.addControl=view;
		[view release];
		}
		[self.navigationController pushViewController:self.addControl animated:YES];
		[self addSetings];
	
	}else if([titles objectAtIndex:indexPath.row]==@"Mapas"){
		if(self.mapa==nil){
			
			MapaModeloViewController *view=[[MapaModeloViewController alloc] initWithNibName:@"MapaModeloViewController" bundle:[NSBundle mainBundle]];
			NSLog(@"no sirve1");
			self.mapa=view;
			NSLog(@"no sirve");
			[view release];
		}
		[self.navigationController pushViewController:self.mapa animated:YES];
		NSLog(@"no sirve");
		//[self addSetings];
		}else if([titles objectAtIndex:indexPath.row]==@"Publicidad"){
			if(self.publicidad==nil){
				
				PublicidadViewController *view=[[PublicidadViewController alloc] initWithNibName:@"PublicidadViewController" bundle:[NSBundle mainBundle]];
				NSLog(@"no sirve1");
				self.publicidad=view;
				NSLog(@"no sirve");
				[view release];
			}
			[self.navigationController pushViewController:self.publicidad animated:YES];
			NSLog(@"no sirve");
			//[self addSetings];
			
		
	}else{    
	if(self.checarControl==nil){
		
		checarController *view=[[checarController alloc] initWithNibName:@"checar" bundle:[NSBundle mainBundle]];
		self.checarControl=view;
		[view release];
	}
	[self.navigationController pushViewController:self.checarControl animated:YES];
	checarControl.anodes.text=[ano objectAtIndex:indexPath.row];
	checarControl.tittledes.text=[titles objectAtIndex:indexPath.row];
	checarControl.sinopsisdes.text=[sinopsis objectAtIndex:indexPath.row];
		double tavo =[[sinopsis objectAtIndex:indexPath.row] doubleValue];
		NSLog(@"%f",tavo*10);
	[self checarSetings];
	}
}


#pragma mark -
#pragma mark Memory management

- (void)didReceiveMemoryWarning {
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Relinquish ownership any cached data, images, etc that aren't in use.
}

- (void)viewDidUnload {
    // Relinquish ownership of anything that can be recreated in viewDidLoad or on demand.
    // For example: self.myOutlet = nil;
}


- (void)dealloc {
    [super dealloc];
}


@end

