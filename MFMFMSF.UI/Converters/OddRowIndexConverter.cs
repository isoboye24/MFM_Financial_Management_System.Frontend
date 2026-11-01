using System;
using System.Globalization;
using System.Windows.Data;

namespace MFMFMSF.UI.Converters
{
    public class OddRowIndexConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value == null)
                return false;

            if (int.TryParse(value.ToString(), out int index))
                return index % 2 == 1;

            return false;
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