//
//  UnityInterface.m
//
//  Created by fhq on 6/28/13.
//  Copyright (c) 2013 weRplay. All rights reserved.
//

#import "UnityInterface.h"


void UnityPause( bool );

@implementation UnityInterface

static UnityInterface *_sharedInstance = nil;


+( UnityInterface * ) sharedInstance
{
    @synchronized( [ UnityInterface class ] )
    {
        if( !_sharedInstance )
        {
            _sharedInstance = [ [ UnityInterface alloc ] init ];
        }
        
        return _sharedInstance;
    }
    return nil;
}

+( id ) alloc
{
    @synchronized( [ UnityInterface class ] )
    {
        NSAssert( ( _sharedInstance == nil ), @"Attempted to allocate a second instance of UnityInterface." );
        _sharedInstance = [ super alloc ];
        
        return _sharedInstance;
    }
    
    return nil;
}

-( id ) init
{
    if( self = [ super init ] )
	{
		
	}
	
    return self;
}

-( void ) callBreak: ( BOOL )givenValue
{
	bool theValue = givenValue;

	UnityPause( theValue );
}


-( void ) callInfoFirst: ( NSString * )firstArgument Second: ( NSString * )secondArgument Third:( NSString * )thirdArgument
{
	char *targetString = new char[ firstArgument.length + 1 ];
	sprintf( targetString, "%s", [ firstArgument UTF8String ] );
	
	char *methodString = new char[ secondArgument.length + 1 ];
	sprintf( methodString, "%s", [ secondArgument UTF8String ] );
	
	char *argumentString = new char[ thirdArgument.length + 1 ];
	sprintf( argumentString, "%s", [ thirdArgument UTF8String ] );
	
	UnitySendMessage( targetString, methodString, argumentString );
}

-( void ) dealloc
{
	[ _sharedInstance release ];
	_sharedInstance = nil;
	
	[ super dealloc ];
}

@end