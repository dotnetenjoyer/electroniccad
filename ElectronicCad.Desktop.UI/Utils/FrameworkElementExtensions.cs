using System.Windows;
using System.Windows.Input;

namespace ElectronicCad.Desktop.Styles.Utils;

/// <summary>
/// Contains extension methods for framework elements.
/// </summary>
public static class FrameworkElementExtensions
{
    /// <summary>
    /// Calculates the position of an element without exceeding a boundary.
    /// </summary>
    /// <param name="element">Target element.</param>
    /// <param name="mouseEventArgs">Mouse event arguments.</param>
    public static Point GetPosition(this FrameworkElement element, MouseEventArgs mouseEventArgs)
    {
        var position = mouseEventArgs.GetPosition(element);

        var elementPosition = new Point(position.X, position.Y);

        if (elementPosition.Y < 0)
        {
            elementPosition.Y = 0;
        }
        else if (elementPosition.Y > element.ActualHeight)
        {
            elementPosition.Y = element.ActualHeight;
        }

        if (elementPosition.X < 0)
        {
            elementPosition.X = 0;
        }
        else if (elementPosition.X > element.ActualWidth)
        {
            elementPosition.Y = element.ActualWidth;
        }

        return elementPosition;
    }

}