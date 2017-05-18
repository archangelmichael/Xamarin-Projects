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
        UIKit.UIButton btnCall { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnShowHistory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnShowLocations { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnTranslate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ivAvatar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblPhoneNumber { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tvPhoneNumber { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vDrawing { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCall != null) {
                btnCall.Dispose ();
                btnCall = null;
            }

            if (btnShowHistory != null) {
                btnShowHistory.Dispose ();
                btnShowHistory = null;
            }

            if (btnShowLocations != null) {
                btnShowLocations.Dispose ();
                btnShowLocations = null;
            }

            if (btnTranslate != null) {
                btnTranslate.Dispose ();
                btnTranslate = null;
            }

            if (ivAvatar != null) {
                ivAvatar.Dispose ();
                ivAvatar = null;
            }

            if (lblPhoneNumber != null) {
                lblPhoneNumber.Dispose ();
                lblPhoneNumber = null;
            }

            if (tvPhoneNumber != null) {
                tvPhoneNumber.Dispose ();
                tvPhoneNumber = null;
            }

            if (vDrawing != null) {
                vDrawing.Dispose ();
                vDrawing = null;
            }
        }
    }
}