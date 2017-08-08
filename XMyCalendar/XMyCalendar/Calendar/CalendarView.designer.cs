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

namespace XMyCalendar
{
    [Register ("CalendarView")]
    partial class CalendarView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnNext { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnPrev { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UICollectionView cvDays { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblFriday { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblMonday { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblMonth { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblSaturday { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblSunday { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblThursday { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTuesday { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblWednesday { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnNext != null) {
                btnNext.Dispose ();
                btnNext = null;
            }

            if (btnPrev != null) {
                btnPrev.Dispose ();
                btnPrev = null;
            }

            if (cvDays != null) {
                cvDays.Dispose ();
                cvDays = null;
            }

            if (lblFriday != null) {
                lblFriday.Dispose ();
                lblFriday = null;
            }

            if (lblMonday != null) {
                lblMonday.Dispose ();
                lblMonday = null;
            }

            if (lblMonth != null) {
                lblMonth.Dispose ();
                lblMonth = null;
            }

            if (lblSaturday != null) {
                lblSaturday.Dispose ();
                lblSaturday = null;
            }

            if (lblSunday != null) {
                lblSunday.Dispose ();
                lblSunday = null;
            }

            if (lblThursday != null) {
                lblThursday.Dispose ();
                lblThursday = null;
            }

            if (lblTuesday != null) {
                lblTuesday.Dispose ();
                lblTuesday = null;
            }

            if (lblWednesday != null) {
                lblWednesday.Dispose ();
                lblWednesday = null;
            }
        }
    }
}