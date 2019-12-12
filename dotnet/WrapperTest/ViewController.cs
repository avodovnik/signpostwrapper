using System;

using AppKit;
using Foundation;

using Xamarin.Mac.SignPostWrapper;

namespace WrapperTest
{
    public partial class ViewController : NSViewController
    {
        private SignPostLog _log;
        private SignPostLog.SignPostLogger _logger;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            this._log = new SignPostLog("com.vodovnik.test", "WrapperTest");
            this._logger = _log.GenerateLogger();

            using (var _ = _logger.CreateInterval("Loop", ""))
            {
                for(int i = 0; i < 30; i++)
                {
                    Console.WriteLine(i); 
                }
            }
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
