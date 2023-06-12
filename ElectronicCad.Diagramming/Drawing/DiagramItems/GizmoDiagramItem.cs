using System.Windows.Input;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Gizmo diagram item.
/// </summary>
internal class GizmoDiagramItem : DiagramItem
{
    /// <summary>
    /// Default size of gizmo element.
    /// </summary>
    public static readonly SKSize DefaultSize = new SKSize(10, 10);

    private readonly SKSize size;
    private readonly Cursor cursor;
    private readonly SKPaint fillPaint;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GizmoDiagramItem(SKSize size, Cursor cursor)
    {
        this.size = size;
        this.cursor = cursor;

        StrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            Color = new SKColor(12, 140, 233, 100)
        };

        fillPaint = new SKPaint
        {
            Style = SKPaintStyle.StrokeAndFill,
            StrokeWidth = 2,
            Color = SKColors.White
        };
    }

    /// <inheritdoc />
    public override Cursor GetCurrentCursor()
    {
        return cursor;
    }

    /// <summary>
    /// Set the center point.
    /// </summary>
    /// <param name="point">Center point coordinates.</param>
    public void SetCenterPoint(SKPoint point)
    {
        var left = point.X - size.Width / 2;
        var right = left + size.Width;
        var top = point.Y - size.Height / 2;
        var bottom = top + size.Height;
        BoundingBox = new SKRect(left, top, right, bottom);
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext context)
    {
        context.DrawRect(BoundingBox, fillPaint);
        context.DrawRect(BoundingBox, StrokePaint);
    }
}
