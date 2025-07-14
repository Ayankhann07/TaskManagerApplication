using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace TaskManagerApplication.Converters
{
    public class BoolToArrowIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isExpanded)
            {
                return isExpanded ? "up_arrow.png" : "down.png"; 
            }

            return "down.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}



