using TaskManagerApplication.Models;
using TaskManagerApplication.ViewModels;
using Microsoft.Maui.Controls;

namespace TaskManagerApplication.Views
{
    public partial class AddEditTaskPage : ContentPage, IQueryAttributable
    {
        private readonly TaskDetailViewModel _viewModel;

        public AddEditTaskPage(TaskDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        // 👇 This method runs when navigation passes query parameters
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Task", out var taskObj) && taskObj is TaskModel task)
            {
                _viewModel.LoadTask(task); // fills Title, Description, DueDate, etc.
            }
        }
    }
}
