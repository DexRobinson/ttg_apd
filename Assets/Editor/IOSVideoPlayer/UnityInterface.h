//
//  UnityInterface.h
//
//  Created by fhq on 6/28/13.
//  Copyright (c) 2013 weRplay. All rights reserved.
//

#import <Foundation/Foundation.h>


@protocol InterfaceProtocol < NSObject >

-( void ) callBreak: ( BOOL )givenValue;
-( void ) callInfoFirst: ( NSString * )firstArgument Second: ( NSString * )secondArgument Third: ( NSString * )thirdArgument;

@end



////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////



@interface UnityInterface : NSObject < InterfaceProtocol >
{
	
}

+( UnityInterface * ) sharedInstance;

@end