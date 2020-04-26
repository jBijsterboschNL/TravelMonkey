using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TravelMonkey.Models;
using Xamarin.Forms;

namespace TravelMonkey.Converters
{
    public class FirstDestinationImageOrDefaultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<DestinationImage> images && images.Any())
            {
                return images.FirstOrDefault().ImageUrl;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}