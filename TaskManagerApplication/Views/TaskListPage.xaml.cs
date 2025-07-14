using Mopups.Services;
using TaskManagerApplication.ViewModels;
using TaskManagerApplication.Models;
using TaskManagerApplication.Views;
using Microsoft.Maui.Controls;

namespace TaskManagerApplication.Views
{
    public partial class TaskListPage : ContentPage
    {
        private readonly TaskListViewModel _viewModel;

        public TaskListPage(TaskListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!_viewModel.LoadTasksCommand.IsRunning)
                _viewModel.LoadTasksCommand.Execute(null);
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddEditTaskPage));
        }

        private async void OnTaskSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedTask = e.CurrentSelection.FirstOrDefault() as TaskModel;
            if (selectedTask == null) return;

            await MopupService.Instance.PushAsync(new TaskDetailsPopup(selectedTask));

            ((CollectionView)sender).SelectedItem = null;
        }
        private void OnToggleExpandClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton btn && btn.CommandParameter is TaskModel selected)
            {
                foreach (var task in _viewModel.Tasks)
                {
                    if (task != selected)
                        task.IsExpanded = false;
                }

                selected.IsExpanded = !selected.IsExpanded;
            }
        }

        private async void OnEditTaskClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton btn && btn.CommandParameter is TaskModel selected)
            {
                await Shell.Current.GoToAsync(nameof(AddEditTaskPage), true, new Dictionary<string, object>
        {
            { "Task", selected }
        });
            }
        }

        private async void OnDeleteTaskClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton btn && btn.CommandParameter is TaskModel selected)
            {
                bool confirm = await DisplayAlert("Confirm", $"Delete '{selected.Title}'?", "Yes", "No");
                if (confirm)
                {
                    await _viewModel.DeleteTaskAsync(selected); 
                }
            }
        }
        private async void OnDeviceInfoClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(DeviceInfoPage));
        }




    }
}
