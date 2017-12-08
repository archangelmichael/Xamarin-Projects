using System.Collections.ObjectModel;
using XMVVMLight.Model;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace XMVVMLight.ViewModel
{
    // This class contains properties that the main View can data bind to.
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<TaskModel> TodoTasks { get; private set; }

        private readonly INavigationService navigationService;

        private string welcomeTitle;
        private RelayCommand nextCommand;
        private RelayCommand tasksCommand; 
        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            WelcomeTitle = "First Page";

            TodoTasks = new ObservableCollection<TaskModel>()
            {
                new TaskModel { Name = "Make Lunch", Notes = "" },
                new TaskModel { Name = "Pack Lunch", Notes = "In the bag, make sure we don't squash anything." },
                new TaskModel { Name = "Goto Work", Notes = "Walk if it's sunny" },
                new TaskModel { Name = "Eat Lunch", Notes = "" },
            };

            AddTaskCommand = new RelayCommand(AddTask);
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
        public RelayCommand ShowNextCommand
        {
            get
            {
                return nextCommand ?? (nextCommand = new RelayCommand(() => navigationService.NavigateTo(ViewModelLocator.SecondVCKey)));
            }
        }

        public RelayCommand ShowTasksCommand
        {
            get
            {
                return tasksCommand ?? (tasksCommand = new RelayCommand(() => navigationService.NavigateTo(ViewModelLocator.TasksVCKey)));
            }
        }

        public RelayCommand AddTaskCommand { get; set; }

        private void AddTask()
        {
            TodoTasks.Add(new TaskModel
            {
                Name = "New Task",
                Notes = ""
            });
        }
    }
}