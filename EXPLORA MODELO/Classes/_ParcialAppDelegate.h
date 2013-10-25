//
//  _ParcialAppDelegate.h
//  3Parcial
//
//  Created by aCADc14 m03 on 09/04/11.
//  Copyright 2011 ITESM Campus Zacatecas. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface _ParcialAppDelegate : NSObject <UIApplicationDelegate> {
    
    UIWindow *window;
    UINavigationController *navigationController;
}

@property (nonatomic, retain) IBOutlet UIWindow *window;
@property (nonatomic, retain) IBOutlet UINavigationController *navigationController;

@end

