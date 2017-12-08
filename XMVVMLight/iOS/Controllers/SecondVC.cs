using System;

using UIKit;

using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Helpers;
using XMVVMLight.ViewModel;

namespace XMVVMLight.iOS
{
    public partial class SecondVC : UIViewController
    {
        public SecondVC(IntPtr param) : base(param) { }
        public SecondVC() : base("SecondVC", null) { }

        private MainViewModel Vm => Application.Locator.Main;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            BtnTasks.SetCommand("TouchUpInside", Vm.ShowTasksCommand);
            BtnBack.TouchUpInside += (s, e) =>
            {
                var nav = ServiceLocator.Current.GetInstance<INavigationService>();
                nav.GoBack();
            };
        }
    }
}

