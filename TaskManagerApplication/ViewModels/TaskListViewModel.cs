using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TaskManagerApplication.Models;
using TaskManagerApplication.Interfaces;
using TaskManagerApplication.Views;

namespace TaskManagerApplication.ViewModels
{
    public partial class TaskListViewModel : ObservableObject
    {
        private readonly ITaskService _taskService;

        [ObservableProperty]
        private ObservableCollection<TaskModel> tasks = new();

        [ObservableProperty]
        private string searchQuery;

        [ObservableProperty]
        private bool showCompletedOnly;

        public TaskListViewModel(ITaskService taskService)
        {
            _taskService = taskService;
            LoadTasksCommand = new AsyncRelayCommand(LoadTasksAsync);
        }

        public IAsyncRelayCommand LoadTasksCommand { get; }

        [RelayCommand]
        public async Task DeleteTaskAsync(TaskModel task)
        {
            if (task == null) return;

            await _taskService.DeleteTaskAsync(task);
            await LoadTasksAsync();
        }

        [RelayCommand]
        public async Task ViewImageAsync(TaskModel task)
        {
            if (!string.IsNullOrEmpty(task?.ImagePath))
                await App.Current.MainPage.Navigation.PushModalAsync(new ImagePreviewPage(task.ImagePath));
        }

        private async Task LoadTasksAsync()
        {
            try
            {
                var allTasks = await _taskService.GetTasksAsync();

                if (!string.IsNullOrWhiteSpace(SearchQuery))
                {
                    allTasks = allTasks
                        .Where(t => t.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (ShowCompletedOnly)
                {
                    allTasks = allTasks
                        .Where(t => t.IsCompleted)
                        .ToList();
                }

                Tasks = new ObservableCollection<TaskModel>(allTasks);
            }
            catch (Exception ex)
            {
                await ShowAlert("Error loading tasks", ex.Message);
            }
        }

        partial void OnSearchQueryChanged(string value)
        {
            LoadTasksCommand.Execute(null);
        }

        partial void OnShowCompletedOnlyChanged(bool value)
        {
            LoadTasksCommand.Execute(null);
        }

        private static Task ShowAlert(string title, string message) =>
            Shell.Current.DisplayAlert(title, message, "OK");
    }
}
