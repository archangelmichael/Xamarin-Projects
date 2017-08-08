// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XMyCalendar
{
    [Register ("CalendarCollectionViewCell")]
    partial class CalendarCollectionViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDay { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vMark { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblDay != null) {
                lblDay.Dispose ();
                lblDay = null;
            }

            if (vMark != null) {
                vMark.Dispose ();
                vMark = null;
            }
        }
    }
}