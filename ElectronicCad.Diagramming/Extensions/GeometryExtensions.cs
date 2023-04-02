using ElectronicCad.Domain.Geometry;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Extensions;

/// <summary>
/// Extension methods for geometrys.
/// </summary>
internal static class GeometryExtensions
{
    /// <summary>
    /// Converts domain point to skia point.
    /// </summary>
    /// <param name="point">Point to convert.</param>
    /// <returns>Skia point.</returns>
    public static SKPoint ToSKPoint(this Point point)
    {
        return new SKPoint(point.X, point.Y);
    }

    /// <summary>
    /// Converts domain rectangle to skia rectangle.
    /// </summary>
    /// <param name="rectangle">Rectangle to convert.</param>
    /// <returns>Skia rectangle.</returns>
    public static SKRect ToSKRect(this Rectangle rectangle)
    {
        return new SKRect(rectangle.X, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
    }
}