using System;
using System.Numerics;
using System.Windows.Input;
using System.Collections.Generic;
using SkiaSharp;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Selection frame diagram item.
/// </summary>
internal class SelectionFrameDiagramItem : GroupDiagramItem
{
    /// <inhertidoc/>
    public override bool IsAuxiliary => true;

    /// <summary>
    /// Selected geometry object.
    /// </summary>
    public GeometryObject? SelectedItem { get; internal set; }

    private readonly SelectionFrameArea frameArea;
    private readonly GizmoDiagramItem topLefGizmo;
    private readonly GizmoDiagramItem topRigthGizmo;
    private readonly GizmoDiagramItem bottomLeftGizmo;
    private readonly GizmoDiagramItem bottomRigthGizmo;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SelectionFrameDiagramItem()
    {
        ZIndex = int.MaxValue;
        MouseMove += HandleMouseMove;

        frameArea = new SelectionFrameArea();
        topLefGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize);
        topRigthGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize);
        bottomLeftGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize);
        bottomRigthGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize);

        var children = new List<DiagramItem>()
        {
            topLefGizmo,
            topRigthGizmo,
            bottomLeftGizmo,
            bottomRigthGizmo,
            frameArea
        };

        Children = children;
    }

    private void HandleMouseMove(object? sender, MovingMouseParameters mouse)
    {
        if (SelectedItem != null && mouse.LeftButton == MouseButtonState.Pressed)
        {
            var translateMatrix = Matrix3x2.CreateTranslation(mouse.Delta.ToVector2());

            using var scope = SelectedItem.StartDiagramModifcation();
            SelectedItem.StartModification();
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
        topLefGizmo.SetCenterPoint(boundingBox.GetTopLeft());
        topRigthGizmo.SetCenterPoint(boundingBox.GetTopRight());
        bottomLeftGizmo.SetCenterPoint(boundingBox.GetBottomLeft());
        bottomRigthGizmo.SetCenterPoint(boundingBox.GetBottomRight());
        frameArea.BoundingBox = boundingBox;
        
        base.Draw(context);
    }
}

/// <summary>
/// Selection fram area item.
/// </summary>
internal class SelectionFrameArea : DiagramItem
{
    /// <inheritdoc />
    public override SKPaint StrokePaint => new SKPaint
    {
        Color = Colors.Foreground,
        Style = SKPaintStyle.Stroke,
        StrokeWidth = 1,
        PathEffect = SKPathEffect.CreateDash(new float[] { 5, 5 }, 0),
    };

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext context)
    {
        context.DrawRect(BoundingBox, StrokePaint);
    }
}

/// <summary>
/// Gizmo diagram item.
/// </summary>
internal class GizmoDiagramItem : DiagramItem
{
    /// <summary>
    /// Default size of gizmo element.
    /// </summary>
    public static readonly SKSize DefaultSize = new SKSize(10, 10);

    /// <inheritdoc />
    public override Cursor GetCurrentCursor()
    {
        return Cursors.SizeNS;
    }

    /// <inheritdoc />
    public override SKPaint StrokePaint => new SKPaint
    {
        Color = Colors.Foreground,
        Style = SKPaintStyle.StrokeAndFill
    };

    private readonly SKSize size;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GizmoDiagramItem(SKSize size)
    {
        this.size = size;

        MouseMove += GizmoDiagramItem_MouseMove;
    }

    private void GizmoDiagramItem_MouseMove(object? sender, MovingMouseParameters e)
    {
        Console.WriteLine();
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
        context.DrawRect(BoundingBox, StrokePaint);
    }
}
