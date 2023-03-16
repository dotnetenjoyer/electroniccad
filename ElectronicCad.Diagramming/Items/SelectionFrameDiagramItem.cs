using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Diagramming.Utils;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Nodes;

public class SelectionFrameDiagramItem : DiagramItem
{
    internal override bool IsAuxiliary => true;

    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        var paint = new SKPaint
        {
            Color = Colors.Brand,
            Style = SKPaintStyle.Stroke,
            PathEffect = SKPathEffect.CreateDash(new float[] {5, 5}, 0),
        };

        canvas.DrawRect(Bounds, paint);
        
        DrawGizmos(canvas, Bounds.GetTopLeft());
        DrawGizmos(canvas, Bounds.GetTopRight());
        DrawGizmos(canvas, Bounds.GetBottomLeft());
        DrawGizmos(canvas, Bounds.GetBottomRight());
        DrawGizmos(canvas, Bounds.GetCenter());
        DrawGizmos(canvas, Bounds.GetLeftCenter());
        DrawGizmos(canvas, Bounds.GetRightCenter());
        DrawGizmos(canvas, Bounds.GetTopCenter());
        DrawGizmos(canvas, Bounds.GetBottomCenter());
    }

    private void DrawGizmos(SKCanvas canvas, SKPoint point)
    {
        var paint = new SKPaint()
        {
            Color = SKColors.White,
            Style = SKPaintStyle.StrokeAndFill
        };

        var width = 10;
        canvas.DrawRect(point.X - width / 2, point.Y - width / 2, width, width, paint);
    }
}