// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MicrosoftGraph.iOS
{
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnConnect { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnMail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblInfo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnConnect != null) {
                btnConnect.Dispose ();
                btnConnect = null;
            }

            if (btnMail != null) {
                btnMail.Dispose ();
                btnMail = null;
            }

            if (lblInfo != null) {
                lblInfo.Dispose ();
                lblInfo = null;
            }
        }
    }
}