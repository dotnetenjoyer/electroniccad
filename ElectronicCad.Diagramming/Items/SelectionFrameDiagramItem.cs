using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Domain.Geometry;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Selection frame diagram item.
/// </summary>
internal class SelectionFrameDiagramItem : DiagramItem
{
    private static readonly SKPaint gizmoPaint;
    private static readonly SKPaint areaPaint;

    private bool isMoving;
    private SKPoint startMovingPosition;


    /// <inhertidoc/>
    internal override bool IsAuxiliary => true;

    /// <summary>
    /// Selected geometry object.
    /// </summary>
    public GeometryObject SelectedItem { get; internal set; }

    /// <summary>
    /// Static constructor.
    /// </summary>
    static SelectionFrameDiagramItem()
    {
        gizmoPaint = new SKPaint
        {
            Color = Colors.Foreground,
            Style = SKPaintStyle.StrokeAndFill,
        };

        areaPaint = new SKPaint
        {
            Color = Colors.Primary,
            Style = SKPaintStyle.Stroke,
            PathEffect = SKPathEffect.CreateDash(new float[] { 5, 5 }, 0),
        };
    }

    /// <inheritdoc />
    public override void HandleMouseDown(SKPoint position)
    {
        if(SelectedItem != null)
        {
            isMoving = true;
            startMovingPosition = position;
        }
    }

    /// <inheritdoc />
    public override void HandleMouseUp(SKPoint position)
    {
        isMoving = false;
    }

    /// <inheritdoc />
    public override void HandleMouseMove(SKPoint currentPosition)
    {
        if (isMoving && SelectedItem != null)
        {
            var delta = currentPosition - startMovingPosition;
            startMovingPosition = currentPosition;
            
            using var scope = SelectedItem.Layer.Diagram.StartModification();
            for (int i = 0; i < SelectedItem.ControlPoints.Count; i++)
            {
                var controlPoint = SelectedItem.ControlPoints[i];
                SelectedItem.UpdateControlPoint(i, controlPoint.X + delta.X, controlPoint.Y + delta.Y);
            }
        }
    }

    /// <inheritdoc/>
    public override void Draw(SKCanvas canvas)
    {
        if(SelectedItem != null)
        {
            var boundingBox = SelectedItem.CalculateBoundingBox();
            BoundingBox = boundingBox.ToSKRect();
        }

        canvas.DrawRect(BoundingBox, areaPaint);
        DrawGizmos(canvas);
    }

    private void DrawGizmos(SKCanvas canvas)
    {
        DrawGizmo(canvas, BoundingBox.GetTopLeft());
        DrawGizmo(canvas, BoundingBox.GetTopRight());
        DrawGizmo(canvas, BoundingBox.GetBottomLeft());
        DrawGizmo(canvas, BoundingBox.GetBottomRight());
        DrawGizmo(canvas, BoundingBox.GetCenter());
    }

    private void DrawGizmo(SKCanvas canvas, SKPoint point)
    {
        var width = 8;
        var halfWidth = width / 2;
        canvas.DrawRect(point.X - halfWidth, point.Y - halfWidth, width, width, gizmoPaint);
    }
}