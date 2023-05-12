using ElectronicCad.Domain.Geometry;
using System.Numerics;

namespace ElectronicCad.Domain.Utils;

/// <summary>
/// Contains utility methods for domain points.
/// </summary>
public static class PointsUtils
{
    /// <summary>
    /// Converts a domain point to a Vector2.
    /// </summary>
    /// <param name="point">Domain point to convert.</param>
    /// <returns>Vector2 instance.</returns>
    public static Vector2 ToVector2(this Point point) 
    { 
        return new Vector2(point.X, point.Y);
    }
}