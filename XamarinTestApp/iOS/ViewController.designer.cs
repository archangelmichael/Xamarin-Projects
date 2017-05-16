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
        UIKit.UIButton BtnCall { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnShowHistory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnTranslate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblPhoneNumber { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TvPhoneNumber { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BtnCall != null) {
                BtnCall.Dispose ();
                BtnCall = null;
            }

            if (BtnShowHistory != null) {
                BtnShowHistory.Dispose ();
                BtnShowHistory = null;
            }

            if (BtnTranslate != null) {
                BtnTranslate.Dispose ();
                BtnTranslate = null;
            }

            if (LblPhoneNumber != null) {
                LblPhoneNumber.Dispose ();
                LblPhoneNumber = null;
            }

            if (TvPhoneNumber != null) {
                TvPhoneNumber.Dispose ();
                TvPhoneNumber = null;
            }
        }
    }
}