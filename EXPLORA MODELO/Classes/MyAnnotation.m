//
//  MyAnnotation.m
//  MapaModelo
//
//  Created by Ramon Badillo on 4/28/11.
//  Copyright 2011 ITESM. All rights reserved.
//

#import "MyAnnotation.h"


@implementation MyAnnotation
@synthesize coordinate;
@synthesize title;
@synthesize subtitle;
@synthesize annotationType;

-init
{
	return self;
}

-initWithCoordinate:(CLLocationCoordinate2D)inCoord
{
	coordinate = inCoord;
	return self;
}

@end
