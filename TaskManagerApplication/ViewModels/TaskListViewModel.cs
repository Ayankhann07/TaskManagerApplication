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

        public IAsyncRelayCommand LoadTasksCommand { get; }

        public TaskListViewModel(ITaskService taskService)
        {
            _taskService = taskService;

            LoadTasksCommand = new AsyncRelayCommand(LoadTasksAsync);
        }

        private async Task LoadTasksAsync()
        {
            var allTasks = await _taskService.GetTasksAsync();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
                allTasks = allTasks
                    .Where(t => t.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (ShowCompletedOnly)
                allTasks = allTasks
                    .Where(t => t.IsCompleted)
                    .ToList();

            Tasks = new ObservableCollection<TaskModel>(allTasks);
        }

        partial void OnSearchQueryChanged(string value)
        {
            LoadTasksCommand.Execute(null);
        }

        partial void OnShowCompletedOnlyChanged(bool value)
        {
            LoadTasksCommand.Execute(null);
        }
        public async Task DeleteTaskAsync(TaskModel task)
        {
            await _taskService.DeleteTaskAsync(task);
            await LoadTasksAsync(); 
        }
        [RelayCommand]
        private async Task ViewImageAsync(TaskModel task)
        {
            if (!string.IsNullOrEmpty(task?.ImagePath))
                await App.Current.MainPage.Navigation.PushModalAsync(new ImagePreviewPage(task.ImagePath));
        }


    }

}
