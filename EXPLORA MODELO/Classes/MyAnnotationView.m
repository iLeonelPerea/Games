//
//  MyAnnotationView.m
//  MapaModelo
//
//  Created by Ramon Badillo on 4/28/11.
//  Copyright 2011 ITESM. All rights reserved.
//

#import "MyAnnotationView.h"


@implementation MyAnnotationView
@synthesize imageView;

#define kHeight 35
#define kWidth  35
#define kBorder 2

- (id)initWithAnnotation:(id <MKAnnotation>)annotation reuseIdentifier:(NSString *)reuseIdentifier
{
	MyAnnotation* myAnnotation = (MyAnnotation*)annotation;
	
	if([myAnnotation annotationType] == AnnotationTypeExtra)
	{
		self = [super initWithAnnotation:myAnnotation reuseIdentifier:reuseIdentifier];
		self.frame = CGRectMake(0, 0, kWidth, kHeight);
		self.backgroundColor = [UIColor clearColor];
		
		imageView = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@"Extra.png"]];
		imageView.frame = CGRectMake(kBorder, kBorder, kWidth - 2 * kBorder, kWidth - 2 * kBorder);
		[self addSubview:imageView];
	}
	
	else if([myAnnotation annotationType] == AnnotationTypeModelorama)
	{
		self = [super initWithAnnotation:myAnnotation reuseIdentifier:reuseIdentifier];
		self.frame = CGRectMake(0, 0, kWidth, kHeight);
		self.backgroundColor = [UIColor clearColor];
		
		imageView = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@"Modelorama.png"]];
		imageView.frame = CGRectMake(kBorder, kBorder, kWidth - 2 * kBorder, kWidth - 2 * kBorder);
		[self addSubview:imageView];
	}
	
	else if([myAnnotation annotationType] == AnnotationTypeCervecerias)
	{
		self = [super initWithAnnotation:myAnnotation reuseIdentifier:reuseIdentifier];
		self.frame = CGRectMake(0, 0, kWidth, kHeight);
		self.backgroundColor = [UIColor clearColor];
		
		imageView = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@"Cerveceria.png"]];
		imageView.frame = CGRectMake(kBorder, kBorder, kWidth - 2 * kBorder, kWidth - 2 * kBorder);
		[self addSubview:imageView];
	}
	
	[imageView setContentMode:UIViewContentModeScaleAspectFill];
	
	return self;
}

@end
