// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace XMVVMLight.iOS
{
	[Register ("FirstVC")]
	partial class FirstVC
	{
		[Outlet]
		UIKit.UIButton BtnNext { get; set; }

		[Outlet]
		UIKit.UILabel LblWelcome { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LblWelcome != null) {
				LblWelcome.Dispose ();
				LblWelcome = null;
			}

			if (BtnNext != null) {
				BtnNext.Dispose ();
				BtnNext = null;
			}
		}
	}
}
