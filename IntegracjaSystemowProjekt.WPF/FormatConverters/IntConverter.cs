using System;
using System.Windows.Data;

namespace IntegracjaSystemowProjekt.WPF.FormatConverters
{
    public class IntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return String.Empty;

            if (value.ToString().Contains("-"))
                return String.Empty;

            string strVal = value.ToString();

            if (string.IsNullOrEmpty(strVal))
                return 0;

            return int.Parse(strVal);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
                return null;

            return value?.ToString();
        }
    }
}