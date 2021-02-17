using NewWPF.Models.Common;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NewWPF.UI.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class MessageDialogIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var iconName = "InformationOutline";

            if (value != null && value is MessageDialogType type)
            {
                iconName = type switch
                {
                    MessageDialogType.Done => "Check",
                    MessageDialogType.Error => "AlertCircleOutline",
                    MessageDialogType.Warning => "Alert",
                    MessageDialogType.Info => "InformationOutline",
                    MessageDialogType.Question => "HelpCircleOutline",
                    MessageDialogType.Stop => "HandLeft",
                    _ => "InformationOutline",
                };
            }

            return Application.Current.FindResource(iconName) as Geometry;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}