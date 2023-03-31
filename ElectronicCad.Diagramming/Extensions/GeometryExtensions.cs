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
        return new SKPoint((float)point.X, (float)point.Y);
    }
}