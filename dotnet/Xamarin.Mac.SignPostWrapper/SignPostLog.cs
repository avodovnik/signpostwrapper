using System;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace Xamarin.Mac.SignPostWrapper
{
    public class SignPostLog
    {
        [DllImport("libSignPostWrapper.dylib", CallingConvention = CallingConvention.Cdecl)]
        private static extern void signpost_wrapper_log(
            IntPtr log,
            IntPtr signpost_id,
            byte type,
            string name,
            string format,
            params string[] metadata);

        [DllImport("__Internal", EntryPoint = "os_log_create")]
        private static extern IntPtr os_log_create(string subsystem, string category);

        [DllImport(Constants.libSystemLibrary)]
        private static extern IntPtr os_signpost_id_make_with_pointer(IntPtr log,
            IntPtr ptr,
            params string[] additional);

        protected readonly IntPtr _pointer;

        public SignPostLog(string subystem, string category)
        {
            this._pointer = os_log_create(subystem, category);
        }

        public SignPostLogger GenerateLogger()
        {
            return new SignPostLogger(this);
        }

        public class SignPostLogger
        {
            private readonly IntPtr _idPtr;
            private readonly SignPostLog _log;

            public SignPostLogger(SignPostLog log)
            {
                var ptr = GCHandle.ToIntPtr(GCHandle.Alloc(log));
                _idPtr = os_signpost_id_make_with_pointer(log._pointer, ptr);
                this._log = log;
            }

            public IDisposable CreateInterval(string name, string metadata)
            {
                return new SignPostInterval(this, name, metadata);
            }

            private class SignPostInterval : IDisposable
            {
                private const byte Event = 0;
                private const byte IntervalBegin = 1;
                private const byte IntervalEnd = 2;

                private readonly SignPostLogger _logger;
                private readonly string _name;
                private readonly string _metadata;

                public SignPostInterval(SignPostLogger logger, string name, string metadata)
                {
                    this._logger = logger;
                    this._name = name;
                    this._metadata = metadata;

                    signpost_wrapper_log(logger._log._pointer, logger._idPtr, IntervalBegin, _name, _metadata);
                }

                public void Dispose()
                {
                    signpost_wrapper_log(_logger._log._pointer, _logger._idPtr, IntervalEnd, _name, _metadata);
                }
            }
        }
    }
}