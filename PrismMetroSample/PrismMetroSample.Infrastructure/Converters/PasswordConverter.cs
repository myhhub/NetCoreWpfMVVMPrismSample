using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace PrismMetroSample.Infrastructure.Converters
{
    public class PasswordConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Dictionary<string, PasswordBox> keyValues = new Dictionary<string, PasswordBox>();
            foreach (var item in values)
            {
                var password = (PasswordBox)item;
                keyValues.Add(password.Name, password);
            }
            return keyValues;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
