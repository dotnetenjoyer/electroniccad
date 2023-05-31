using System;
using System.Numerics;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Diagramming.Utils;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Utils;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Selection frame diagram item.
/// </summary>
internal class SelectionFrameDiagramItem : GroupDiagramItem
{
    /// <inhertidoc/>
    public override bool IsAuxiliary => true;
    
    /// <inhertidoc/>
    public override bool IsVisible => SelectedItems != null && SelectedItems.Any();

    private IEnumerable<GeometryObject> SelectedItems => diagram.SelectedItems;


    private readonly Diagram diagram;

    private readonly SelectionFrameArea frameArea;
    private readonly GizmoDiagramItem topLefGizmo;
    private readonly GizmoDiagramItem topRigthGizmo;
    private readonly GizmoDiagramItem bottomLeftGizmo;
    private readonly GizmoDiagramItem bottomRigthGizmo;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SelectionFrameDiagramItem(Diagram diagram)
    {
        this.diagram = diagram;
        ZIndex = int.MaxValue;
        MouseMove += HandleMouseMove;

        frameArea = new SelectionFrameArea();
        topLefGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize);
        topRigthGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize);
        bottomLeftGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize);
        bottomRigthGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize);

        AddChild(topLefGizmo);
        AddChild(topRigthGizmo);
        AddChild(bottomLeftGizmo);
        AddChild(bottomRigthGizmo);
        AddChild(frameArea);
    }

    private void HandleMouseMove(object? sender, MovingMouseParameters mouse)
    {
        if (SelectedItems.Any() && mouse.LeftButton == MouseButtonState.Pressed)
        {
            var translateMatrix = Matrix3x2.CreateTranslation(mouse.Delta.ToVector2());

            using var scope = SelectedItems.First().StartDiagramModifcation();
    
            foreach (var selectedItem in SelectedItems)
            {
                selectedItem.StartModification();
                selectedItem.Transform(translateMatrix);
                selectedItem.CompleteModification();
            }
        }
    }

    /// <inheritdoc/>
    public override void Draw(SkiaDrawingContext context)
    {
        if (!IsVisible)
        {
            return;
        }

        var points = SelectedItems.SelectMany(x => x.ControlPoints);
        var boundingBox = PointsUtils.CalculateBoundingBox(points).ToSKRect();
        
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
        StrokeWidth = 2,
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
