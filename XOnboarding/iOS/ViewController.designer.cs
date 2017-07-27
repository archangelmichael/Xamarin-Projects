// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XOnboarding.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnLoad { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblProgress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vProgress { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnLoad != null) {
                btnLoad.Dispose ();
                btnLoad = null;
            }

            if (lblProgress != null) {
                lblProgress.Dispose ();
                lblProgress = null;
            }

            if (vProgress != null) {
                vProgress.Dispose ();
                vProgress = null;
            }
        }
    }
}