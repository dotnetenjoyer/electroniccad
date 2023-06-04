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
using System.Security.Cryptography.Pkcs;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Selection frame diagram item.
/// </summary>
internal class SelectionFrameDiagramItem : GroupDiagramItem
{
    private readonly SelectionFrameArea frameArea;
    private readonly GizmoDiagramItem topLefGizmo;
    private readonly GizmoDiagramItem topRigthGizmo;
    private readonly GizmoDiagramItem bottomLeftGizmo;
    private readonly GizmoDiagramItem bottomRigthGizmo;

    /// <inhertidoc/>
    public override bool IsAuxiliary => true;
    
    /// <inhertidoc/>
    public override bool IsVisible => SelectedItems.Any();

    private IEnumerable<GeometryObject> SelectedItems 
        => Diagram?.SelectedItems ?? Array.Empty<GeometryObject>();
        
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

        AddChildren(new DiagramItem[]
        {
            topLefGizmo,
            topRigthGizmo,
            bottomLeftGizmo,
            bottomRigthGizmo,
            frameArea
        });
    }

    private void HandleMouseMove(object? sender, MovingMouseParameters mouse)
    {
        if (SelectedItems.Any() && mouse.LeftButton == MouseButtonState.Pressed)
        {
            var geometryObjectsToInteraction = GeometryUtils.FilterNestingGeometry(SelectedItems);
            var translateMatrix = Matrix3x2.CreateTranslation(mouse.Delta.ToVector2());
            using var scope = SelectedItems.First().StartDiagramModifcation();

            foreach (var selectedItem in geometryObjectsToInteraction)
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
    
    private class SelectionFrameArea : DiagramItem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SelectionFrameArea()
        {
            StrokePaint = new SKPaint
            {
                Color = Colors.Foreground,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2,
                PathEffect = SKPathEffect.CreateDash(new float[] { 5, 5 }, 0),
            };
        }

        /// <inheritdoc />
        public override void Draw(SkiaDrawingContext context)
        {
            context.DrawRect(BoundingBox, StrokePaint);
        }
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

    private readonly SKSize size;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GizmoDiagramItem(SKSize size)
    {
        this.size = size;

        StrokePaint = new SKPaint
        {
            Color = Colors.Foreground,
            Style = SKPaintStyle.StrokeAndFill
        };

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
