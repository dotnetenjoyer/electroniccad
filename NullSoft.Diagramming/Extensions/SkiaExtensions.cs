using System.Windows;
using SkiaSharp;

namespace NullSoft.Diagramming.Extensions;

/// <summary>
/// Geometry extensions.
/// </summary>
public static class SkiaExtensions
{
    /// <summary>
    /// Get top left point of skia rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Top left point.</returns>
    public static SKPoint GetTopLeft(this SKRect skRect)
    {
        return new SKPoint(skRect.Left, skRect.Top);
    }

    /// <summary>
    /// Get bottom right point of skia rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Bottom right point.</returns>
    public static SKPoint GetBottomRight(this SKRect skRect)
    {
        return new SKPoint(skRect.Right, skRect.Bottom);
    }

    /// <summary>
    /// Get center point of ska rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Center point.</returns>
    public static SKPoint GetCenter(this SKRect skRect)
    {
        return new SKPoint(skRect.Left + skRect.Width / 2, skRect.Top + skRect.Height / 2);
    }

    /// <summary>
    /// Extension method to i
    /// </summary>
    /// <param name="skRect"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static bool ContainsTest(this SKRect skRect, SKPoint point)
    {
        var topLeft = skRect.GetTopLeft();
        var bottomRight = skRect.GetBottomRight();

        double maxX, minX, maxY, minY;

        if (topLeft.X > bottomRight.X)
        {
            maxX = topLeft.X;
            minX = bottomRight.X;
        }
        else
        {
            maxX = bottomRight.X;
            minX = topLeft.X;
        }

        if (topLeft.Y > bottomRight.Y)
        {
            maxY = topLeft.Y;
            minY = bottomRight.Y;
        }
        else
        {
            minY = topLeft.Y;
            maxY = bottomRight.Y;
        }

        return point.X > minX && point.X < maxX && point.Y > minY && point.Y < maxY;
    }
}