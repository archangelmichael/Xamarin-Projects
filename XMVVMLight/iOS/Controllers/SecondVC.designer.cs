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
	[Register ("SecondVC")]
	partial class SecondVC
	{
		[Outlet]
		UIKit.UIButton BtnBack { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BtnBack != null) {
				BtnBack.Dispose ();
				BtnBack = null;
			}
		}
	}
}
