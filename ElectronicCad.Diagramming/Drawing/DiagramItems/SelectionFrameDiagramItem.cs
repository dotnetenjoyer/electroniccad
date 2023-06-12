using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Utils;
using System.Reflection;
using System.Windows.Input;

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

    private bool isMoveStarted;
    private bool isResizeStarted;


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

        frameArea = new SelectionFrameArea();
        topLefGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize, Cursors.SizeNWSE);
        topRigthGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize, Cursors.SizeNESW);
        bottomLeftGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize, Cursors.SizeNESW);
        bottomRigthGizmo = new GizmoDiagramItem(GizmoDiagramItem.DefaultSize, Cursors.SizeNWSE);

        frameArea.MouseDown += HandleFrameAreaMouseDown;
        frameArea.MouseUp += HandleFrameAreaMouseUp;
        frameArea.MouseMove += HandleFrameAreaMouseMove;

        topLefGizmo.MouseDown += HandleGizmoMouseDown;
        topLefGizmo.MouseUp += HandleGizmoMouseUp;
        topLefGizmo.MouseMove += HandleGizmoMouseMove;

        topRigthGizmo.MouseDown += HandleGizmoMouseDown;
        topRigthGizmo.MouseUp += HandleGizmoMouseUp;
        topRigthGizmo.MouseMove += HandleGizmoMouseMove;

        bottomLeftGizmo.MouseDown += HandleGizmoMouseDown;
        bottomLeftGizmo.MouseUp += HandleGizmoMouseUp;
        bottomLeftGizmo.MouseMove += HandleGizmoMouseMove;

        bottomRigthGizmo.MouseDown += HandleGizmoMouseDown;
        bottomRigthGizmo.MouseUp += HandleGizmoMouseUp;
        bottomRigthGizmo.MouseMove += HandleGizmoMouseMove;

        AddChildren(new DiagramItem[]
        {
            topLefGizmo,
            topRigthGizmo,
            bottomLeftGizmo,
            bottomRigthGizmo,
            frameArea
        });
    }

    private void HandleFrameAreaMouseDown(object? sender, MouseParameters mouse)
    {
        isMoveStarted = true;
    }

    private void HandleFrameAreaMouseUp(object? sender, MouseParameters mouse)
    {
        isMoveStarted = false;
    }

    private void HandleFrameAreaMouseMove(object? sender, MovingMouseParameters mouse)
    {
        if (!isMoveStarted || !(SelectedItems.Any() && mouse.LeftButton == MouseButtonState.Pressed))
        {
            return;
        }

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

    private void HandleGizmoMouseDown(object? sender, MouseParameters mouse)
    {
        isResizeStarted = true;
    }

    private void HandleGizmoMouseUp(object? sender, MouseParameters mouse)
    {
        isResizeStarted = false;
    }

    private void HandleGizmoMouseMove(object? sender, MovingMouseParameters mouse)
    {
        if (!isResizeStarted)
        {
            return;
        }
        
        var newSize = (mouse.Position - BoundingBox.GetCenter()).ToVector2() * 2;
        var scale = new Vector2(newSize.X / BoundingBox.Width, newSize.Y / BoundingBox.Height);
        var scaleTransform = Matrix3x2.CreateScale(scale);

        using var scope = SelectedItems.First().StartDiagramModifcation();
        foreach(var geometryObject in SelectedItems)
        {
            geometryObject.StartModification();

            var translateToOrigin = Matrix3x2.CreateTranslation(-geometryObject.BoundingBox.Center.ToVector2());
            var translateToNewCenter = Matrix3x2.CreateTranslation(geometryObject.BoundingBox.Center.ToVector2() + mouse.Delta.ToVector2() / 2);
            var transformMatrix = translateToOrigin * scaleTransform * translateToNewCenter;
            geometryObject.Transform(transformMatrix);
            
            geometryObject.CompleteModification();
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
        BoundingBox = PointsUtils.CalculateBoundingBox(points).ToSKRect();
        
        topLefGizmo.SetCenterPoint(BoundingBox.GetTopLeft());
        topRigthGizmo.SetCenterPoint(BoundingBox.GetTopRight());
        bottomLeftGizmo.SetCenterPoint(BoundingBox.GetBottomLeft());
        bottomRigthGizmo.SetCenterPoint(BoundingBox.GetBottomRight());
        frameArea.BoundingBox = BoundingBox;
        
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
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2,
                Color = new SKColor(12, 140, 233, 100)
            };
        }

        /// <inheritdoc />
        public override void Draw(SkiaDrawingContext context)
        {
            context.DrawRect(BoundingBox, StrokePaint);
        }
    }
}
