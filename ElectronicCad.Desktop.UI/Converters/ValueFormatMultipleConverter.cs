
using System;
using System.Globalization;
using System.Windows.Data;

namespace ElectronicCad.Desktop.UI.Converters;

/// <summary>
/// The converter that allow you to apply a format to a value,
/// order in the multiple binding is as folows [value, format]
/// </summary>
public class ValueFormatMultipleConverter : IMultiValueConverter
{
    /// <inhertidoc />
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var value = (string) values[0];
        var format = (string) values[1];

        if (!string.IsNullOrEmpty(format))
        {
            if (float.TryParse(value, out var floatValue))
            {
                if (value.EndsWith(".") || value.EndsWith("0"))
                {
                    return value;
                }

                return string.Format(format, floatValue);
            }
        }

        return value;
    }

    /// <inhertidoc />
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return new[] { value };
    }
}