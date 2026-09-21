using System;
using System.Globalization;
using System.Windows.Data;

namespace MFMFMSF.UI.Converters
{
    public class RowNumberConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            if (int.TryParse(value.ToString(), out int index))
                return (index + 1).ToString();

            return string.Empty;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}