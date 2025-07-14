using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using System;
using System.Threading.Tasks;
using TaskManagerApplication.Interfaces;
using TaskManagerApplication.Models;

namespace TaskManagerApplication.ViewModels
{
    public partial class TaskDetailViewModel : ObservableObject
    {
        private readonly ITaskService _taskService;

        [ObservableProperty] private int id;
        [ObservableProperty] private string title;
        [ObservableProperty] private string description;
        [ObservableProperty] private DateTime dueDate = DateTime.Today;
        [ObservableProperty] private string priority;
        [ObservableProperty] private bool isCompleted;
        [ObservableProperty] private string imagePath;


        public TaskDetailViewModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [RelayCommand]
        //public async Task SaveTask()
        //{
        //    if (string.IsNullOrWhiteSpace(Title))
        //    {
        //        await App.Current.MainPage.DisplayAlert("Error", "Title is required", "OK");
        //        return;
        //    }

        //    var task = new TaskModel
        //    {
        //        Id = Id,
        //        Title = Title,
        //        Description = Description,
        //        DueDate = DueDate,
        //        Priority = Priority,
        //        IsCompleted = IsCompleted
        //    };

        //    if (Id == 0)
        //        await _taskService.AddTaskAsync(task);
        //    else
        //        await _taskService.UpdateTaskAsync(task);

        //    await Shell.Current.GoToAsync(".."); // Go back
        //}

        public void LoadTask(TaskModel task)
        {
            if (task == null) return;

            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            DueDate = task.DueDate;
            Priority = task.Priority;
            IsCompleted = task.IsCompleted;
            ImagePath = ImagePath;
        }
        public List<string> Priorities { get; } = new() { "Low", "Medium", "High" };

        [RelayCommand]
        private async Task SaveTask()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Title is required", "OK");
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
        //[RelayCommand]
        //public async Task PickImageAsync()
        //{
        //    try
        //    {
        //        var result = await MediaPicker.PickPhotoAsync();
        //        if (result != null)
        //        {
        //            var path = result.FullPath;
        //            ImagePath = path;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        //    }
        //    var task = new TaskModel
        //    {
        //        Id = Id,
        //        Title = Title,
        //        Description = Description,
        //        DueDate = DueDate,
        //        Priority = Priority,
        //        IsCompleted = IsCompleted,
        //        ImagePath = ImagePath // 👈 this
        //    };

        //}
        [RelayCommand]
        public async Task PickImage()
        {
            try
            {
                string action = await Shell.Current.DisplayActionSheet(
                    "Select Image Source",
                    "Cancel",
                    null,
                    "📸 Take Photo",
                    "🖼️ Pick from Gallery");

                FileResult result = null;

                if (action == "📸 Take Photo")
                {
                    result = await MediaPicker.CapturePhotoAsync();
                }
                else if (action == "🖼️ Pick from Gallery")
                {
                    result = await MediaPicker.PickPhotoAsync();
                }

                if (result != null)
                {
                    ImagePath = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}
