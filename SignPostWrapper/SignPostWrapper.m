//
//  SignPostWrapper.m
//  SignPostWrapper
//
//  Created by Anze Vodovnik on 05/12/2019.
//  Copyright © 2019 Anze Vodovnik. All rights reserved.
//

#import "SignPostWrapper.h"

@implementation SignPostWrapper

void xamarin_os_signpost_interval_begin(os_log_t logger, os_signpost_id_t signpost_id, const char *message)
{
    os_signpost_interval_begin(logger, signpost_id, "SignPostWrapper", "%{public}s", message);
}

void xamarin_os_signpost_interval_end(os_log_t logger, os_signpost_id_t signpost_id)
{
    os_signpost_interval_end(logger, signpost_id, "SignPostWrapper");
}

void* xamarin_os_get_dso_handle(os_log_t log, os_signpost_id_t signpost_id, const char *name, const char *format, ...)
{
    return &__dso_handle;
}

void anze_test_log(os_log_t log, os_signpost_id_t signpost_id, os_signpost_type_t type, const char *name, const char *format, ...)
{
    va_list args;
    va_start(args, format);
    uint8_t *t = va_arg(args, uint8_t *);
    _os_signpost_emit_with_name_impl(&__dso_handle, log , type, signpost_id, name, format, t, sizeof t);
}

@end
