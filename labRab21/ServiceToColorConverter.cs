using System;
using System.Windows.Data;
using System.Windows.Media;

namespace labRab21
{
    internal class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string status = value?.ToString();
            if (status == "Сдано")
            {
                return Brushes.Green;
            }
            if (status == "Не сдано")
            {
                return Brushes.Red;
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}