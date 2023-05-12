using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ElectronicCad.Desktop.UI.Converters
{
    /// <summary>
    /// Tooltip visiblity converter, defines a visibility value for a text block.
    /// </summary>
    internal class TooltipVisibilityConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var textBlock = value as TextBlock;
            
            if(textBlock == null)
            {
                return Visibility.Hidden;
            }

            return IsTextTrimmed(textBlock)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
        
        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool IsTextTrimmed(TextBlock textBlock)
        {
            var pixelsPerDip = VisualTreeHelper.GetDpi(textBlock).PixelsPerDip;

            var typeface = new Typeface(
               textBlock.FontFamily,
               textBlock.FontStyle,
               textBlock.FontWeight,
               textBlock.FontStretch);

            var formattedText = new FormattedText(
                textBlock.Text,
                System.Threading.Thread.CurrentThread.CurrentCulture,
                textBlock.FlowDirection,
                typeface,
                textBlock.FontSize,
                textBlock.Foreground,
                pixelsPerDip)
            {
                MaxTextWidth = textBlock.ActualWidth,
            };

            return Math.Round(formattedText.Height) > Math.Round(textBlock.ActualHeight)
                || Math.Round(formattedText.MinWidth) > Math.Round(formattedText.MaxTextWidth);
        }
    }
}
