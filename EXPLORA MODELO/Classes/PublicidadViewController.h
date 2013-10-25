//
//  PublicidadViewController.h
//  Publicidad
//
//  Created by aCADc16 m03 on 29/04/11.
//  Copyright 2011 ITESM Campus Zacatecas. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "PhotoViewerViewController.h";

@interface PublicidadViewController : UIViewController {
	
	IBOutlet UIWebView *webView;
	IBOutlet UIBarButtonItem *btnModelo;
	IBOutlet UIBarButtonItem *btnConsumo;
	IBOutlet UIBarButtonItem *btnMarcas;
	IBOutlet UIToolbar *toolBtn;
	PhotoViewerViewController *photos;
	NSString *address1, *address2, * addreess3;
	

}

@property (nonatomic, retain) UIView *webView;
@property (nonatomic, retain) PhotoViewerViewController *photos;
@property (nonatomic, retain) UIBarButtonItem *btnModelo, *btnConsumo, *btnMarcas;
@property (nonatomic, retain) UIToolbar *toolBtn;
@property (nonatomic, retain) NSString  *address1, *address2, * addreess3;

-(IBAction)goToModelo :(UIBarButtonItem *)sender;
-(IBAction)goToConsumo :(UIBarButtonItem *)sender;
-(IBAction)goToMarcas :(UIBarButtonItem *)sender;
-(IBAction)goToEventos :(UIBarButtonItem *)sender;


@end

