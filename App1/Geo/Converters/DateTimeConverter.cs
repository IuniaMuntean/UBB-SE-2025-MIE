using System;
using Microsoft.UI.Xaml.Data;

namespace App1.Geo.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime == DateTime.MinValue)
                    return null;
                return new DateTimeOffset(dateTime);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.DateTime;
            }
            return DateTime.MinValue;
        }
    }
} 