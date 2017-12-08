using System;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using UIKit;
using XMVVMLight.Model;
using XMVVMLight.ViewModel;

namespace XMVVMLight.iOS
{
    public partial class TasksVC : UIViewController
    {
        public TasksVC(IntPtr param) : base(param) { }
        public TasksVC() : base("TasksVC", null) { }

        private MainViewModel Vm => Application.Locator.Main;

        // private ObservableTableViewController<TaskModel> tableViewController;
        private ObservableTableViewSource<TaskModel> source;

        public UIBarButtonItem ButtonAddTask { get; private set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ButtonAddTask = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            this.NavigationItem.SetRightBarButtonItem(ButtonAddTask, false);

            ButtonAddTask.Clicked += (sender, e) => {};
            ButtonAddTask.SetCommand("Clicked", Vm.AddTaskCommand);


            source = Vm.TodoTasks.GetTableViewSource(CreateTaskCell,
                                                     BindTaskCell,
                                                     factory: () => new TaskListObservableTableSource());
            TableTasks.Source = source;

            // tableViewController = Vm.TodoTasks.GetController(CreateTaskCell, BindTaskCell);
            // tableViewController.TableView = TableTasks;
        }

        private void BindTaskCell(UITableViewCell cell,
                                  TaskModel taskModel,
                                  NSIndexPath path)
        {
            cell.TextLabel.Text = taskModel.Name;
            cell.DetailTextLabel.Text = taskModel.Notes;
        }

        private UITableViewCell CreateTaskCell(NSString cellIdentifier)
        {
            var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, null);
            cell.TextLabel.TextColor = UIColor.FromRGB(55, 63, 255);
            cell.DetailTextLabel.LineBreakMode = UILineBreakMode.TailTruncation;

            return cell;
        }
    }
}

