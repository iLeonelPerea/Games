//
//  MyAnnotationView.h
//  MapaModelo
//
//  Created by Ramon Badillo on 4/28/11.
//  Copyright 2011 ITESM. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <MapKit/MapKit.h>
#import "MyAnnotation.h"

@interface MyAnnotationView : MKAnnotationView {
	UIImageView *imageView;
}
@property (nonatomic, retain) UIImageView *imageView;
@end
