using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace XMVVMLight.ViewModel
{
    // This class contains properties that the main View can data bind to.
    public class MainViewModel : ViewModelBase
    {
        private string welcomeTitle;
        private RelayCommand navigateCommand;
        private readonly INavigationService navigationService; 
        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            WelcomeTitle = "First Page";
        }          public string WelcomeTitle
        {
            get
            {
                return welcomeTitle;
            }
            set
            {
                Set(ref welcomeTitle, value);
            }
        } 
        public RelayCommand NavigateCommand
        {
            get
            {
                return navigateCommand ?? (navigateCommand = new RelayCommand(() => navigationService.NavigateTo(ViewModelLocator.SecondPageKey)));
            }
        }
    }
}