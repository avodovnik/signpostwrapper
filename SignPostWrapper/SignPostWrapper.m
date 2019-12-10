//
//  SignPostWrapper.m
//  SignPostWrapper
//
//  Created by Anze Vodovnik on 05/12/2019.
//  Copyright © 2019 Anze Vodovnik. All rights reserved.
//

#import "SignPostWrapper.h"

@implementation SignPostWrapper

void signpost_wrapper_log(os_log_t log, os_signpost_id_t signpost_id, os_signpost_type_t type, const char *name, const char *format, ...)
{
    va_list args;
    va_start(args, format);
    uint8_t *t = va_arg(args, uint8_t *);
    _os_signpost_emit_with_name_impl(&__dso_handle, log , type, signpost_id, name, format, t, sizeof t);
}

@end
