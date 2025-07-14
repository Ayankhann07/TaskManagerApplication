using Mopups.Pages;
using Mopups.Services;
using TaskManagerApplication.Models;
using Microsoft.Maui.Controls;

namespace TaskManagerApplication.Views
{
    public partial class TaskDetailsPopup : PopupPage
    {
        private TaskModel _task;

        public TaskDetailsPopup(TaskModel task)
        {
            InitializeComponent();
            _task = task;
            BindingContext = _task;
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync();
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync(); 
            await Shell.Current.GoToAsync(nameof(AddEditTaskPage), new ShellNavigationQueryParameters
            {
                { "TaskToEdit", _task }
            });
        }
    }
}
