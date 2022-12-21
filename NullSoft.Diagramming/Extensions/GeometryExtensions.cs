using System.Windows;
using SkiaSharp;

namespace NullSoft.Diagramming.Extensions;

/// <summary>
/// Geometry extensions.
/// </summary>
public static class GeometryExtensions
{
    /// <summary>
    /// Get top left point of skia rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Top left point.</returns>
    public static SKPoint TopLeft(this SKRect skRect)
    {
        return new SKPoint(skRect.Left, skRect.Top);
    }

    /// <summary>
    /// Get bottom right point of skia rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Bottom right point.</returns>
    public static SKPoint BottomRight(this SKRect skRect)
    {
        return new SKPoint(skRect.Bottom, skRect.Right);
    }

    /// <summary>
    /// Get center point of ska rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Center point.</returns>
    public static SKPoint Center(this SKRect skRect)
    {
        return new SKPoint(skRect.Left + skRect.Width / 2, skRect.Top + skRect.Height / 2);
    }
}