//
//  addController.h
//  3Parcial
//
//  Created by aCADc14 m03 on 09/04/11.
//  Copyright 2011 ITESM Campus Zacatecas. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "sqlite3.h";


@interface addController : UIViewController {
	
	IBOutlet UILabel *tittless;
	IBOutlet UILabel *sinopsis;
	IBOutlet UILabel *ano;
	IBOutlet UITextView *tittledes;
	IBOutlet UITextView *sinopsisdes;
	IBOutlet UITextView *anodes;
	IBOutlet UITextField *tipo;
	
	IBOutlet UIButton *add;
	sqlite3 *db;
	NSString *dbName;

}
-(IBAction)insertar; 
-(IBAction)eliminar;

@property (nonatomic,retain) UILabel *tittless;
@property (nonatomic,retain) UILabel *sinopsis;
@property (nonatomic,retain) UILabel *ano;
@property (nonatomic,retain) UITextView *tittledes;
@property (nonatomic,retain) UITextView *sinopsisdes;
@property (nonatomic,retain) UITextView *anodes;
@property (nonatomic,retain) UITextField *tipo;
@property (nonatomic,retain) UIButton *add;

@property (nonatomic,retain) NSString *dbName;

@end
