using SkiaSharp;

namespace ElectronicCad.Diagramming.Extensions;

/// <summary>
/// Skia rectangle extensions.
/// </summary>
public static class SkiaRectExtensions
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
    /// Get top right point of skia rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Top right point.</returns>
    public static SKPoint GetTopRight(this SKRect skRect)
    {
        return new SKPoint(skRect.Right, skRect.Top);
    }
    
    /// <summary>
    /// Get bottom left point of skia rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Bottom left point.</returns>
    public static SKPoint GetBottomLeft(this SKRect skRect)
    {
        return new SKPoint(skRect.Left, skRect.Bottom);
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
        return new SKPoint(skRect.MidX, skRect.MidY);
    }
    
    /// <summary>
    /// Get top center point of ska rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Top center point.</returns>
    public static SKPoint GetTopCenter(this SKRect skRect)
    {
        return new SKPoint(skRect.MidX, skRect.Top);
    }
    
    /// <summary>
    /// Get bottom center point of ska rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Bottom center point.</returns>
    public static SKPoint GetBottomCenter(this SKRect skRect)
    {
        return new SKPoint(skRect.MidX, skRect.Bottom);
    }
    
    /// <summary>
    /// Get left center point of ska rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Left center point.</returns>
    public static SKPoint GetLeftCenter(this SKRect skRect)
    {
        return new SKPoint(skRect.Left, skRect.MidY);
    }

    /// <summary>
    /// Get right center point of ska rectangle.
    /// </summary>
    /// <param name="skRect">Skia rectangle.</param>
    /// <returns>Right center point.</returns>
    public static SKPoint GetRightCenter(this SKRect skRect)
    {
        return new SKPoint(skRect.Right, skRect.MidY);
    }

    /// <summary>
    /// Returns collection of plot points.
    /// </summary>
    /// <param name="rectangle">Rectangle.</param>
    /// <returns>Collection of plot points.</returns>
    public static SKPoint[] GetPlotPoints(this SKRect rectangle)
    {
        return new[]
        {
            rectangle.GetTopLeft(),
            rectangle.GetTopRight(),
            rectangle.GetBottomLeft(),
            rectangle.GetBottomRight()
        };
    }
}