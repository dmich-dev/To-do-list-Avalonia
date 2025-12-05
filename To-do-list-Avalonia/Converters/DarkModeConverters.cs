using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace To_do_list_Avalonia.Converters;

/// <summary>
/// Converter for dark mode button icon.
/// Shows sun emoji when dark mode is OFF, moon emoji when ON.
/// </summary>
public class DarkModeIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isDarkMode)
        {
            return isDarkMode ? "??" : "??";
        }
        return "??";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converter for dark mode button text.
/// Shows appropriate text based on current mode.
/// </summary>
public class DarkModeTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isDarkMode)
        {
            return isDarkMode ? "Light" : "Dark";
        }
        return "Dark";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
