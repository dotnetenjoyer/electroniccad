using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Diagramming.Utils;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Selection frame diagram item.
/// </summary>
internal class SelectionFrameDiagramItem : DiagramItem
{
    private readonly SKPaint _gizmoPaint;
    private readonly SKPaint _areaPaint;

    /// <inhertidoc/>
    internal override bool IsAuxiliary => true;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SelectionFrameDiagramItem()
    {
        _gizmoPaint = new SKPaint
        {
            Color = Colors.Foreground,
            Style = SKPaintStyle.StrokeAndFill,
        };
        
        _areaPaint = new SKPaint
        {
            Color = Colors.Primary,
            Style = SKPaintStyle.Stroke,
            PathEffect = SKPathEffect.CreateDash(new float[] {5, 5}, 0),
        };
    }
    
    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        canvas.DrawRect(BoundingBox, _areaPaint);
        DrawGizmos(canvas);
    }

    private void DrawGizmos(SKCanvas canvas)
    {
        DrawGizmo(canvas, BoundingBox.GetTopLeft());
        DrawGizmo(canvas, BoundingBox.GetTopRight());
        DrawGizmo(canvas, BoundingBox.GetBottomLeft());
        DrawGizmo(canvas, BoundingBox.GetBottomRight());
        DrawGizmo(canvas, BoundingBox.GetCenter());
        DrawGizmo(canvas, BoundingBox.GetLeftCenter());
        DrawGizmo(canvas, BoundingBox.GetRightCenter());
        DrawGizmo(canvas, BoundingBox.GetTopCenter());
        DrawGizmo(canvas, BoundingBox.GetBottomCenter());
    }

    private void DrawGizmo(SKCanvas canvas, SKPoint point)
    {
        var width = 8;
        var halfWidth = width / 2;
        canvas.DrawRect(point.X - halfWidth, point.Y - halfWidth, width, width, _gizmoPaint);
    }
}