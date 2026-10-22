using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MFMFMSF.UI.Converters
{
    public sealed class IncomeSeriesColorConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            string? resourceKey = value?.ToString() switch
            {
                "Offerings" => "PrimaryBrush",
                "Tithes" => "InfoBrush",
                "Seeds (Faith Promise)" => "SuccessBrush",
                "Other Income" => "WarningBrush",
                _ => null
            };

            if (resourceKey is not null &&
                Application.Current.TryFindResource(resourceKey)
                    is Brush brush)
            {
                return brush;
            }

            return Application.Current.TryFindResource("PrimaryTextBrush")
                   as Brush ?? Brushes.Gray;
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
