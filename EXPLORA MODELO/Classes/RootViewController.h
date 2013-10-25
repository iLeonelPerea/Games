//
//  RootViewController.h
//  3Parcial
//
//  Created by aCADc14 m03 on 09/04/11.
//  Copyright 2011 ITESM Campus Zacatecas. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <sqlite3.h>
#import "addController.h";
#import "checarController.h";
#import "MapaModeloViewController.h";
#import "PublicidadViewController.h"
@interface RootViewController : UITableViewController {
	
	NSDictionary *movieList;
	NSString *language;
	addController *addControl;
	checarController *checarControl;
	MapaModeloViewController * mapa;
	PublicidadViewController * publicidad;	
	sqlite3 *db;
	NSString *dbName;
	NSMutableArray *titles;
	NSMutableArray *sinopsis;
	NSMutableArray *ano;
}

@property (nonatomic,retain)NSDictionary *movieList;
@property (nonatomic,retain)NSString *language;
@property(nonatomic,retain) addController *addControl;
@property(nonatomic,retain) checarController *checarControl;
@property (nonatomic,retain) NSString *dbName;
@property (nonatomic,retain) NSMutableArray *titles;
@property (nonatomic,retain) NSMutableArray *ano;
@property (nonatomic,retain) NSMutableArray *sinopsis;
@property(nonatomic,retain) MapaModeloViewController *mapa;
@property(nonatomic,retain) PublicidadViewController *publicidad;

@end
