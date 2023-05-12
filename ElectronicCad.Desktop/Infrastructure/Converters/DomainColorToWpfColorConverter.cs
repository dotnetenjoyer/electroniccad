
using ElectronicCad.Domain.Geometry;
using System;
using System.Globalization;
using System.Windows.Data;
using WpfColor = System.Windows.Media.Color;


namespace ElectronicCad.Desktop.Infrastructure.Converters;

/// <summary>
/// Value converter from domain color to wpf color.
/// </summary>
public class DomainColorToWpfColorConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            var wpfColor = new WpfColor();
            wpfColor.R = color.Red;
            wpfColor.G = color.Green;
            wpfColor.B = color.Blue;
            wpfColor.A = color.Alpha;
            return wpfColor;
        }

        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is WpfColor wpfColor)
        {
            return new Color(wpfColor.R, wpfColor.G, wpfColor.B, wpfColor.A);
        }

        throw new NotSupportedException();
    }
}