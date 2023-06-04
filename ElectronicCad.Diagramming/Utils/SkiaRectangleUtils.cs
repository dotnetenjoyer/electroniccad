using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using ElectronicCad.Diagramming.Extensions;

namespace ElectronicCad.Diagramming.Utils;

/// <summary>
/// Utils for skia rectangle.
/// </summary>
public static class SkiaRectangleUtils
{
    /// <summary>
    /// Calculates rectangle that is scribed the specified rectangles.
    /// </summary>
    /// <param name="rectangles">Rectangles.</param>
    /// <returns>Rectangle.</returns>
    public static SKRect CalculateScribedRectangle(IEnumerable<SKRect> rectangles)
    {
        var points = rectangles.SelectMany(rect => rect.GetPlotPoints());
        return CalculateRectangle(points);
    }

    /// <summary>
    /// Calculate rectangle based on set of points.
    /// </summary>
    /// <param name="points">Set of points.</param>
    /// <returns>Rectangle.</returns>
    public static SKRect CalculateRectangle(IEnumerable<SKPoint> points)
    {
        if (!points.Any())
        {
            return SKRect.Empty;
        }

        var firstPoint = points.First();

        float
            minX = firstPoint.X,
            maxX = firstPoint.X,
            minY = firstPoint.Y,
            maxY = firstPoint.Y;

        foreach (var point in points)
        {
            if (point.X < minX)
            {
                minX = point.X;
            }

            if (point.X > maxX)
            {
                maxX = point.X;
            }

            if (point.Y < minY)
            {
                minY = point.Y;
            }

            if (point.Y > maxY)
            {
                maxY = point.Y;
            }
        }

        return new SKRect(minX, minY, maxX, maxY);
    }
}