using SkiaSharp;

namespace ElectronicCad.Diagramming.Utils;

/// <summary>
/// Paint utils.
/// </summary>
public static class PaintUtils
{
    /// <summary>
    /// Black fill paint.
    /// </summary>
    public static SKPaint DarkFillPaint = new()
    {
        Color = SKColors.White,
        Style = SKPaintStyle.Fill,
    };
    
    /// <summary>
    /// Black stroke paint.
    /// </summary>
    public static SKPaint DarkStrokePaint = new()
    {
        Color = SKColors.Black,
        Style = SKPaintStyle.Stroke
    };
    
    /// <summary>
    /// Red stroke paint.
    /// </summary>
    public static SKPaint RedStrokePaint = new()
    {
        Color = SKColors.Red,
        Style = SKPaintStyle.Stroke
    };
}