
using System;
using System.Linq;
using System.Windows;

namespace MFMFMSF.UI.Utilities
{
    public enum AppTheme
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        public static AppTheme CurrentTheme { get; private set; }
            = AppTheme.Light;

        public static void ToggleTheme()
        {
            SetTheme(
                CurrentTheme == AppTheme.Light
                    ? AppTheme.Dark
                    : AppTheme.Light);
        }

        public static void SetTheme(AppTheme theme)
        {
            var app = Application.Current;

            if (app == null)
                return;

            var dictionaries = app.Resources.MergedDictionaries;

            var existingTheme = dictionaries.FirstOrDefault(dictionary =>
                dictionary.Source != null &&
                (
                    dictionary.Source.OriginalString.EndsWith(
                        "/Themes/Colors.xaml",
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    dictionary.Source.OriginalString.EndsWith(
                        "/Themes/DarkColors.xaml",
                        StringComparison.OrdinalIgnoreCase)
                ));

            if (existingTheme != null)
                dictionaries.Remove(existingTheme);

            var themeUri = theme == AppTheme.Dark
                ? new Uri(
                    "pack://application:,,,/MFMFMSF.UI;component/Themes/DarkColors.xaml",
                    UriKind.Absolute)
                : new Uri(
                    "pack://application:,,,/MFMFMSF.UI;component/Themes/Colors.xaml",
                    UriKind.Absolute);

            dictionaries.Add(new ResourceDictionary
            {
                Source = themeUri
            });

            CurrentTheme = theme;
        }
    }
}
