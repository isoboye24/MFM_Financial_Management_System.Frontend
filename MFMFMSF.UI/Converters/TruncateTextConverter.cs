using System.Globalization;
using System.Windows.Data;

namespace MFMFMSF.UI.Converters
{
    public class TruncateTextConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value is not string text)
                return string.Empty;

            if (!int.TryParse(parameter?.ToString(), out int maxLength))
                maxLength = 40;

            if (text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength) + "...";
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