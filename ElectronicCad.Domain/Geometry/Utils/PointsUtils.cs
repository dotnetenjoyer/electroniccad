using ElectronicCad.Domain.Geometry;
using System.Numerics;

namespace ElectronicCad.Domain.Geometry.Utils;

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
        return new Vector2((float)point.X, (float)point.Y);
    }

    /// <summary>
    /// Calculates bounding box base on set of points.
    /// </summary>
    /// <param name="points">Set of points.</param>
    /// <returns>Bounding box.</returns>
    public static Rectangle CalculateBoundingBox(Point[] points)
    {
        if (points.Length == 0)
        {
            return Rectangle.Empty;
        }

        double
            maxX = points[0].X,
            minX = points[0].X,
            maxY = points[0].Y,
            minY = points[0].Y;

        foreach (var point in points)
        {
            maxX = Math.Max(maxX, point.X);
            minX = Math.Min(minX, point.X);
            maxY = Math.Max(maxY, point.Y);
            minY = Math.Min(minY, point.Y);
        }

        var width = maxX - minX;
        var height = maxY - minY;
        return new Rectangle(new Point(minX, minY), width, height);
    }
}