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

void           xamarin_os_signpost_interval_begin(os_log_t logger, os_signpost_id_t signpost_id, const char *message);
void           xamarin_os_signpost_interval_end(os_log_t logger, os_signpost_id_t signpost_id);

@end
