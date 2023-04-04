using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Domain.Geometry;
using SkiaSharp;
using System.Collections.Generic;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Selection frame diagram item.
/// </summary>
internal class SelectionFrameDiagramItem : DiagramItem
{
    private static readonly SKPaint areaPaint;

    /// <inhertidoc/>
    internal override bool IsAuxiliary => true;

    /// <summary>
    /// Selected geometry object.
    /// </summary>
    public GeometryObject? SelectedItem { get; internal set; }

    /// <summary>
    /// Static constructor.
    /// </summary>
    static SelectionFrameDiagramItem()
    {
        areaPaint = new SKPaint
        {
            Color = Colors.Primary,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            PathEffect = SKPathEffect.CreateDash(new float[] { 5, 5 }, 0),
        };
    }

    public SelectionFrameDiagramItem()
    {
        ZIndex = int.MaxValue;
    }

    /// <inheritdoc />
    public override void HandleMouseMove(MovingMouseParameters mouse)
    {
        if (mouse.LeftButton == MouseButtonState.Pressed && SelectedItem != null)
        {
            using var scope = SelectedItem.Layer.Diagram.StartModification();
         
            for (int i = 0; i < SelectedItem.ControlPoints.Count; i++)
            {
                var controlPoint = SelectedItem.ControlPoints[i];
                SelectedItem.UpdateControlPoint(i, controlPoint.X + mouse.Delta.X, controlPoint.Y + mouse.Delta.Y);
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

        var gizmo = new GizmoDiagramItem();
        gizmo.SetCenterPoint(BoundingBox.GetTopRight());
        gizmo.Draw(canvas);
    }
}

internal class GizmoDiagramItem : DiagramItem
{
    private static readonly SKPaint gizmoPaint = new SKPaint
    {
        Color = Colors.Foreground,
        Style = SKPaintStyle.StrokeAndFill,
    };

    private readonly SKSize size = new SKSize(10, 10);

    public void SetCenterPoint(SKPoint point)
    {
        point.Offset(-(size.Width / 2), -(size.Height / 2));
        BoundingBox = new SKRect(point.X, point.Y, point.X + size.Width, point.Y + size.Height);
    }

    /// <inheritdoc />
    public override void Draw(SKCanvas canvas)
    {
        canvas.DrawRect(BoundingBox.Left, BoundingBox.Top, BoundingBox.Width, BoundingBox.Height, gizmoPaint);
    }
}

internal class GroupDiagramItem : DiagramItem
{
    private readonly List<DiagramItem> groupedItems = new();

    public override void Draw(SKCanvas canvas)
    {
        foreach (DiagramItem item in groupedItems)
        {
            item.Draw(canvas);
        }
    }
}