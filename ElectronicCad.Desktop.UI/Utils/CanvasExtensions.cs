using System.Windows;
using System.Windows.Controls;

namespace ElectronicCad.Desktop.Styles.Utils;

/// <summary>
/// Contains extension methods for canvas.
/// </summary>
public static class CanvasExtensions
{
    /// <summary>
    /// Align element to the center of the canvas.
    /// </summary>
    /// <param name="canvas">Canvas.</param>
    /// <param name="element">Target element.</param>
    public static void AlignHorizontalCenter(this Canvas canvas, FrameworkElement element)
    {
        var canvasWidth = canvas.ActualWidth > 0 ? canvas.ActualWidth : canvas.Width;
        var elementWidth = element.ActualWidth > 0 ? element.ActualWidth : element.Width;
        Canvas.SetRight(element, canvasWidth / 2 - elementWidth / 2);
    }
}
