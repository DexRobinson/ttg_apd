
//#import "AppController.h"
//#import "UnityAppController.h"		//For Newer versions of Unity
//#import <MediaPlayer/MediaPlayer.h>
#import "WRPBasicMoviePlayer.h"
#import "UnityInterface.h"

extern "C"
{
	void playMovieWithName( char *movieName );
	void playMovieWithURL( char *movieNameWithPath );
	void addSkipButtonWithImageNameAndPosition( char *imageFileName, float coordinateX, float coordinateY );
	void removeSkipButton();
	void shouldPauseUnity( bool thisValue );
	void setObjectName( char *objectName );
} 

void playMovieWithName( char *movieName )
{
	NSLog( @"-> File Name: %s", movieName );
    
	[ WRPBasicMoviePlayer playMovieWithName: movieName ];
}

void playMovieWithURL( char *movieNameWithPath )
{
    NSLog( @"-> File Name with Path: %s", movieNameWithPath );
	
	[ WRPBasicMoviePlayer playMovieWithURL: movieNameWithPath ];
}

void addSkipButtonWithImageNameAndPosition( char *imageFileName, float coordinateX, float coordinateY )
{
	[ WRPBasicMoviePlayer addSkipButtonWithImageName: imageFileName AtPositionX: coordinateX PositionY: coordinateY ];
}

void removeSkipButton()
{
	[ WRPBasicMoviePlayer removeSkipButton ];
}

void shouldPauseUnity( bool thisValue )
{
	[ WRPBasicMoviePlayer shouldPauseUnity: thisValue ];
}

void setObjectName( char *objectName )
{
	NSLog( @"-> Object Name: %s", objectName );
    
	[ WRPBasicMoviePlayer setCallObject: [ UnityInterface sharedInstance ] ];
	[ WRPBasicMoviePlayer setObjectName: objectName ];
}