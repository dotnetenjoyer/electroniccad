using SkiaSharp;
using ElectronicCad.Domain.Geometry;
using System.Numerics;

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
        return new SKPoint((float)point.X, (float)point.Y);
    }

    /// <summary>
    /// Converts skia point to domain point.
    /// </summary>
    /// <param name="point">Point to convert.</param>
    /// <returns>Domain point.</returns>
    public static Point ToDomainPoint(this SKPoint point)
    {
        return new Point(point.X, point.Y);
    }

    /// <summary>
    /// Converts a skia point to Vector2.
    /// </summary>
    /// <param name="point">Point to convert.</param>
    /// <returns>A two dimensions vector.</returns>
    public static Vector2 ToVector2(this SKPoint point)
    {
        return new Vector2(point.X, point.Y);
    }

    /// <summary>
    /// Converts domain rectangle to skia rectangle.
    /// </summary>
    /// <param name="rectangle">Rectangle to convert.</param>
    /// <returns>Skia rectangle.</returns>
    public static SKRect ToSKRect(this Rectangle rectangle)
    {
        return new SKRect((float)rectangle.Start.X, (float)rectangle.Start.Y, (float)rectangle.End.X, (float)rectangle.End.Y);
    }

    /// <summary>
    /// Converts domain color to skia color.
    /// </summary>
    /// <param name="color">Domain color.</param>
    /// <returns>Skia color.</returns>
    public static SKColor ToSKColor(this Color color)
    {
        return new SKColor(color.Red, color.Green, color.Blue, color.Alpha);
    }
}