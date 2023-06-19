using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ElectronicCad.Desktop.Infrastructure.Converters
{
    /// <summary>
    /// Convert boolean value to visiblity.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <inhertidoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            var invert = parameter == null ? false : Boolean.Parse(parameter.ToString());

            if ((bool)value ^ invert)
            {
                return Visibility.Visible;
            } 
            else
            {
                return Visibility.Collapsed;
            }
        }

        /// <inhertidoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
