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

    public static SKPaint DebugPaint { get; private set; }

    /// <summary>
    /// Static constructor.
    /// </summary>
    static Paints()
    {
        ForegroundSolid = new SKPaint
        {
            Color = Colors.Foreground,
            Style = SKPaintStyle.StrokeAndFill
        };

        DebugPaint = new SKPaint
        {
            Color = SKColors.Green,
            Style = SKPaintStyle.StrokeAndFill
        };
    }
}