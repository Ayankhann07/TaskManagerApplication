using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerApplication.Interfaces;
using TaskManagerApplication.Models;
using Microsoft.Maui.Storage;

namespace TaskManagerApplication.ViewModels
{
    public partial class TaskDetailViewModel : ObservableObject
    {
        private readonly ITaskService _taskService;

        public TaskDetailViewModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [ObservableProperty] private int id;
        [ObservableProperty] private string title;
        [ObservableProperty] private string description;
        [ObservableProperty] private DateTime dueDate = DateTime.Today;
        [ObservableProperty] private string priority = "Low";
        [ObservableProperty] private bool isCompleted;
        [ObservableProperty] private string imagePath;

        public List<string> Priorities { get; } = new() { "Low", "Medium", "High" };

        [RelayCommand]
        public void LoadTask(TaskModel task)
        {
            if (task == null) return;

            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            DueDate = task.DueDate;
            Priority = task.Priority;
            IsCompleted = task.IsCompleted;
            ImagePath = task.ImagePath;
        }

        [RelayCommand]
        private async Task SaveTaskAsync()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                await ShowAlert("Error", "Title is required");
                return;
            }

            var task = new TaskModel
            {
                Id = Id,
                Title = Title,
                Description = Description,
                DueDate = DueDate,
                Priority = Priority,
                IsCompleted = IsCompleted,
                ImagePath = ImagePath
            };

            if (Id == 0)
                await _taskService.AddTaskAsync(task);
            else
                await _taskService.UpdateTaskAsync(task);

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task PickImageAsync()
        {
            try
            {
                string action = await Shell.Current.DisplayActionSheet(
                    "Select Image Source",
                    "Cancel",
                    null,
                    "📸 Take Photo",
                    "🖼️ Pick from Gallery");

                FileResult result = action switch
                {
                    "📸 Take Photo" => await MediaPicker.CapturePhotoAsync(),
                    "🖼️ Pick from Gallery" => await MediaPicker.PickPhotoAsync(),
                    _ => null
                };

                if (result != null)
                    ImagePath = result.FullPath;
            }
            catch (Exception ex)
            {
                await ShowAlert("Error", ex.Message);
            }
        }

        private static Task ShowAlert(string title, string message) =>
            Shell.Current.DisplayAlert(title, message, "OK");
    }
}
