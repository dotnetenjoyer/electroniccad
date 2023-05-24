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
    public static Rectangle CalculateBoundingBox(IEnumerable<Point> points)
    {
        if (points.Count() == 0)
        {
            return Rectangle.Empty;
        }

        var firstPoint = points.First();
        double
            maxX = firstPoint.X,
            minX = firstPoint.X,
            maxY = firstPoint.Y,
            minY = firstPoint.Y;

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