//
//  SignPostWrapper.h
//  SignPostWrapper
//
//  Created by Anze Vodovnik on 05/12/2019.
//  Copyright © 2019 Anze Vodovnik. All rights reserved.
//

#import <Foundation/Foundation.h>
#include <os/log.h>
#include <os/signpost.h>

@interface SignPostWrapper : NSObject

void signpost_wrapper_log(os_log_t log, os_signpost_id_t signpost_id, os_signpost_type_t type, const char *name, const char *format, ...);

@end
