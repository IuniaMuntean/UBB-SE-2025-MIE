using System;
using Microsoft.UI.Xaml.Data;

namespace App1.Geo.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue.ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string stringValue && decimal.TryParse(stringValue, out decimal result))
            {
                return result;
            }
            return 0m;
        }
    }
} 