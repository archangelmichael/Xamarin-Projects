// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace XamarinSideMenu.iOS
{
    [Register ("Content2ViewController")]
    partial class Content2ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnMenu2 { get; set; }

        [Action ("UIButtonRC6H8ns1_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButtonRC6H8ns1_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnMenu2 != null) {
                btnMenu2.Dispose ();
                btnMenu2 = null;
            }
        }
    }
}