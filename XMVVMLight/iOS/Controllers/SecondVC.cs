using System;

using UIKit;

using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace XMVVMLight.iOS
{
    public partial class SecondVC : UIViewController
    {
        public SecondVC(IntPtr param) : base(param) { }
        public SecondVC() : base("SecondVC", null) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            BtnBack.TouchUpInside += (s, e) =>
            {
                var nav = ServiceLocator.Current.GetInstance<INavigationService>();
                nav.GoBack();
            };
        }
    }
}

