//
//  MyAnnotation.h
//  MapaModelo
//
//  Created by Ramon Badillo on 4/28/11.
//  Copyright 2011 ITESM. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <MapKit/MapKit.h>

typedef enum {
	AnnotationTypeExtra = 0,
	AnnotationTypeModelorama = 1,
	AnnotationTypeCervecerias = 2
} AnnotationType;

@interface MyAnnotation : NSObject <MKAnnotation>
{
	CLLocationCoordinate2D coordinate;
	NSString *title;
	NSString *subtitle;
	AnnotationType annotationType;
}

@property (nonatomic) CLLocationCoordinate2D coordinate;
@property (nonatomic, retain) NSString *title;
@property (nonatomic, retain) NSString *subtitle;
@property (nonatomic) AnnotationType annotationType;

@end
