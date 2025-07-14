using Microsoft.Extensions.Logging;
using TaskManagerApplication.Services;
using TaskManagerApplication.Interfaces;
using TaskManagerApplication.ViewModels;
using TaskManagerApplication.Views;
using Mopups.Hosting;



namespace TaskManagerApplication
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
             .ConfigureMopups();

#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddSingleton<ITaskService, TaskService>();
            builder.Services.AddSingleton<TaskListViewModel>();
            builder.Services.AddSingleton<TaskListPage>();
            builder.Services.AddTransient<TaskDetailViewModel>();
            builder.Services.AddTransient<TaskDetailViewModel>();
            builder.Services.AddTransient<AddEditTaskPage>();
            builder.Services.AddTransient<DeviceInfoPage>();
            builder.Services.AddTransient<DeviceInfoViewModel>();
            builder.Services.AddTransient<ImagePreviewPage>();

#endif

            return builder.Build();
        }
    }
}
