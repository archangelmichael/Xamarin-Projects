// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XamarinTestApp.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnHello { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnHi { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tvNumber { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnHello != null) {
                btnHello.Dispose ();
                btnHello = null;
            }

            if (btnHi != null) {
                btnHi.Dispose ();
                btnHi = null;
            }

            if (tvNumber != null) {
                tvNumber.Dispose ();
                tvNumber = null;
            }
        }
    }
}