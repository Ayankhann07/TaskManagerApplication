using TaskManagerApplication.Views;

namespace TaskManagerApplication
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddEditTaskPage), typeof(AddEditTaskPage));
            Routing.RegisterRoute(nameof(DeviceInfoPage), typeof(DeviceInfoPage));



        }
    }
}
