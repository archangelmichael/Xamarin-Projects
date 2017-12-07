using System;

using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;

using XMVVMLight.ViewModel;
using GalaSoft.MvvmLight.Views;

using UIKit;

namespace XMVVMLight.iOS
{
    public partial class FirstVC : UIViewController
    {
        // Keep track of bindings to avoid premature garbage collection
        private readonly List<Binding> bindings = new List<Binding>();

        public FirstVC(IntPtr param) : base(param) { }
        public FirstVC() : base("FirstVC", null) { }

        private MainViewModel Vm
        {
            get
            {
                return Application.Locator.Main;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            bindings.Add(this.SetBinding(() => Vm.WelcomeTitle, () => LblWelcome.Text));
            BtnNext.SetCommand("TouchUpInside", Vm.NavigateCommand);
        }
    }
}

