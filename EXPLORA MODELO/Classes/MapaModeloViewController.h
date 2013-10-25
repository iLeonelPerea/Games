//
//  MapaModeloViewController.h
//  MapaModelo
//
//  Created by Ramon Badillo on 4/28/11.
//  Copyright 2011 ITESM. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <MapKit/MapKit.h>

#import "MyAnnotation.h"
#import "MyAnnotationView.h"
#import <sqlite3.h>

@interface MapaModeloViewController : UIViewController<MKMapViewDelegate> {
	IBOutlet UITableView *tableview;
	IBOutlet MKMapView *mapView;
	sqlite3 *db;
	NSString *dbName;
	NSMutableArray *Nombre;
	NSMutableArray *Latitud;
	NSMutableArray *Longitud;
	NSMutableArray *Direccion;
	NSMutableArray *Tipo;
	NSMutableArray *puntos;
}

@property (nonatomic, retain) IBOutlet UITableView *tableview;
@property (nonatomic, retain) IBOutlet MKMapView *mapView;
@property (nonatomic,retain) NSString *dbName;
@property(nonatomic,retain) NSMutableArray *Nombre;
@property(nonatomic,retain) NSMutableArray *Latitud;
@property(nonatomic,retain) NSMutableArray *Longitud;
@property(nonatomic,retain) NSMutableArray *Direccion;
@property(nonatomic,retain) NSMutableArray *Tipo;

-(void)loadOurAnnotations;

@end


