//
//  checarController.h
//  3Parcial
//
//  Created by aCADc14 m03 on 09/04/11.
//  Copyright 2011 ITESM Campus Zacatecas. All rights reserved.
//

#import <UIKit/UIKit.h>



@interface checarController : UIViewController {
	
	IBOutlet UILabel *tittless;
	IBOutlet UILabel *sinopsis;
	IBOutlet UILabel *ano;
	IBOutlet UILabel *tittledes;
	IBOutlet UILabel *sinopsisdes;
	IBOutlet UILabel *anodes;
	
	
}
@property (nonatomic,retain) UILabel *tittless;
@property (nonatomic,retain) UILabel *sinopsis;
@property (nonatomic,retain) UILabel *ano;
@property (nonatomic,retain) UILabel *tittledes;
@property (nonatomic,retain) UILabel *sinopsisdes;
@property (nonatomic,retain) UILabel *anodes;

@end
