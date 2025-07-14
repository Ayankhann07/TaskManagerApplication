using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApplication.ViewModels; 

namespace TaskManagerApplication.Views
{
    public partial class DeviceInfoPage : ContentPage
    {
        public DeviceInfoPage(DeviceInfoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }

}
