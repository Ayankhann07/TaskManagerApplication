using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Devices;

namespace TaskManagerApplication.ViewModels
{
    public partial class DeviceInfoViewModel : ObservableObject
    {
        public string Model => DeviceInfo.Model;
        public string Manufacturer => DeviceInfo.Manufacturer;
        public string Platform => DeviceInfo.Platform.ToString();
        public string OSVersion => DeviceInfo.VersionString;
        public string Idiom => DeviceInfo.Idiom.ToString();
        public string DeviceType => DeviceInfo.DeviceType.ToString();
    }
}
