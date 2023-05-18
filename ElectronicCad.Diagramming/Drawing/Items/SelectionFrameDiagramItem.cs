using SkiaSharp;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Domain.Geometry;
using System.Diagnostics;
using System.Numerics;
using ElectronicCad.Diagramming.Drawing;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Selection frame diagram item.
/// </summary>
internal class SelectionFrameDiagramItem : GroupDiagramItem
{
    /// <inhertidoc/>
    internal override bool IsAuxiliary => true;

    /// <summary>
    /// Selected geometry object.
    /// </summary>
    public GeometryObject? SelectedItem { get; internal set; }

    private readonly SelectionFrameArea selectionFrameArea;

    private readonly GizmoDiagramItem topLefGizmo;
    private readonly GizmoDiagramItem topRigthGizmo;
    private readonly GizmoDiagramItem bottomLeftGizmo;
    private readonly GizmoDiagramItem bottomRigthGizmo;

    public SelectionFrameDiagramItem()
    {
        ZIndex = int.MaxValue;

        selectionFrameArea = new SelectionFrameArea();
        GroupedItems.Add(selectionFrameArea);

        topLefGizmo = new GizmoDiagramItem();
        GroupedItems.Add(topLefGizmo);

        topRigthGizmo = new GizmoDiagramItem();
        GroupedItems.Add(topRigthGizmo);

        bottomLeftGizmo = new GizmoDiagramItem();
        GroupedItems.Add(bottomLeftGizmo);

        bottomRigthGizmo = new GizmoDiagramItem();
        GroupedItems.Add(bottomRigthGizmo);

        MouseMove += HandleMouseMove;
    }

    private void HandleMouseMove(object? sender, MovingMouseParameters mouse)
    {
        if (SelectedItem != null && mouse.LeftButton == MouseButtonState.Pressed)
        {
            using var scope = SelectedItem.StartDiagramModifcation();
            SelectedItem.StartModification();
            var translateMatrix = Matrix3x2.CreateTranslation(mouse.Delta.ToVector2());
            SelectedItem.Transform(translateMatrix);
            SelectedItem.CompleteModification();
        }
    }

    /// <inheritdoc/>
    public override void Draw(SkiaDrawingContext context)
    {
        if (SelectedItem == null)
        {
            return;
        }

        var boundingBox = SelectedItem.BoundingBox.ToSKRect();
        selectionFrameArea.BoundingBox = boundingBox;
        topLefGizmo.SetCenterPoint(boundingBox.GetTopLeft());
        topRigthGizmo.SetCenterPoint(boundingBox.GetTopRight());
        bottomLeftGizmo.SetCenterPoint(boundingBox.GetBottomLeft());
        bottomRigthGizmo.SetCenterPoint(boundingBox.GetBottomRight());

        base.Draw(context);
    }
}

internal class SelectionFrameArea : DiagramItem
{
    private static readonly SKPaint areaPaint;

    static SelectionFrameArea()
    {
        areaPaint = new SKPaint
        {
            Color = SKColors.White,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 1,
            PathEffect = SKPathEffect.CreateDash(new float[] { 5, 5 }, 0),
        };
    }

    public override void Draw(SkiaDrawingContext context)
    {
        context.DrawRect(BoundingBox, areaPaint);
    }
}

internal class GizmoDiagramItem : DiagramItem
{
    private static readonly SKPaint paint = new SKPaint
    {
        Color = Colors.Foreground,
        Style = SKPaintStyle.StrokeAndFill,
    };

    private static readonly SKSize size = new SKSize(8, 8);

    /// <summary>
    /// Constructor.
    /// </summary>
    public GizmoDiagramItem()
    {
        MouseMove += GizmoDiagramItem_MouseMove;
    }

    private void GizmoDiagramItem_MouseMove(object? sender, MovingMouseParameters e)
    {
        Debug.WriteLine("");
    }

    public void SetCenterPoint(SKPoint point)
    {
        point.Offset(-(size.Width / 2), -(size.Height / 2));
        BoundingBox = new SKRect(point.X, point.Y, point.X + size.Width, point.Y + size.Height);
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext context)
    {
        context.DrawRect(BoundingBox, paint);
    }
}
