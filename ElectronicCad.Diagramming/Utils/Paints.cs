using SkiaSharp;

namespace ElectronicCad.Diagramming.Utils;

/// <summary>
/// Skia paints.
/// </summary>
internal class Paints
{
    /// <summary>
    /// Foreground solid paint.
    /// </summary>
    public static SKPaint ForegroundSolid { get; private set; }
    
    /// <summary>
    /// Foreground stroke paint.
    /// </summary>
    public static SKPaint ForegroundStroke { get; private set; }

    /// <summary>
    /// Debug paint.
    /// </summary>
    public static SKPaint DebugPaint { get; private set; }

    /// <summary>
    /// Static constructor.
    /// </summary>
    static Paints()
    {
        ForegroundSolid = new SKPaint
        {
            Color = Colors.Foreground,
            Style = SKPaintStyle.StrokeAndFill,
            StrokeWidth = 2,
        };

        ForegroundStroke = new SKPaint
        {
            Color = Colors.Foreground,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
        };

        DebugPaint = new SKPaint
        {
            Color = SKColors.Green,
            Style = SKPaintStyle.Stroke
        };
    }
}