using SkiaSharp;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Selection area diagram item.
/// </summary>
internal class SelectionAreaDiagramItem : DiagramItem
{
    private SKPaint fillPaint;

    /// <inhertidoc/>
    public override bool IsAuxiliary => true;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SelectionAreaDiagramItem()
    {
        StrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            Color = new SKColor(12, 140, 233, 100)
        };

        fillPaint = new SKPaint
        {
            Style = SKPaintStyle.StrokeAndFill,
            Color = new SKColor(12, 140, 233, 25)
        };
    }

    /// <summary>
    /// Sets the selection area start point.
    /// </summary>
    /// <param name="point">Start point position.</param>
    internal void SetStartPoint(SKPoint point)
    {
        BoundingBox = new SKRect(point.X, point.Y, point.X, point.Y);
    }

    /// <summary>
    /// Sets the selection area end point.
    /// </summary>
    /// <param name="point">End point position.</param>
    internal void SetEndPoint(SKPoint point)
    {
        BoundingBox = new SKRect(BoundingBox.Left, BoundingBox.Top, point.X, point.Y);
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext drawingContext)
    {
        drawingContext.DrawRect(BoundingBox, fillPaint);
        drawingContext.DrawRect(BoundingBox, StrokePaint);
    }

    /// <inheritdoc />
    protected override void DisposeManagedResources()
    {
        fillPaint.Dispose();
    }
}