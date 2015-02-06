//
//  WRPBasicMoviePlayer.h
//
//  Created by fhq on 7/15/13.
//  Copyright (c) 2013 weRplay. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface WRPBasicMoviePlayer : NSObject
{
	
}

+( void ) playMovieWithName: ( const char * )movieName;
+( void ) playMovieWithURL: ( const char * )movieNameWithPath;

+( void ) addSkipButtonWithImageName: ( const char * )imageFileName AtPositionX: ( float )coordinateX PositionY: ( float )coordinateY;

+( void ) removeSkipButton;

+( void ) shouldPauseUnity: ( bool )thisValue;

+( void ) setObjectName: ( const char * )objectName;
+( void ) setCallObject: ( id )givenObject;

@end