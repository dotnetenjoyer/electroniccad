using System;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using ElectronicCad.Diagramming.Drawing.Items;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

/// <summary>
/// Group of geometry diagram items.
/// </summary>
internal class GeometryGroupDiagramItem : GeometryObjectDiagramItem<GeometryGroup>, IDiagramItemContainer
{
    private readonly List<GeometryObjectDiagramItem> children = new();

    private readonly GroupDiagramItem groupDiagramItem;

    /// <inheritdoc />
    IEnumerable<DiagramItem> IDiagramItemContainer.Children => Children;

    /// <summary>
    /// Children geometry object diagram items.
    /// </summary>
    public IEnumerable<GeometryObjectDiagramItem> Children => children;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryGroup">Geometry group.</param>
    /// <param name="children">Children geometry object diagram items.</param>
    public GeometryGroupDiagramItem(GeometryGroup geometryGroup,
        IEnumerable<GeometryObjectDiagramItem> children) : base(geometryGroup)
    {
        groupDiagramItem = new GroupDiagramItem();
        AddChildren(children);
        UpdateViewState();
    }

    /// <inheritdoc />
    public override void UpdateViewState()
    {
        base.UpdateViewState();

        groupDiagramItem.BoundingBox = BoundingBox;
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext drawingContext)
    {
        if (!IsVisible)
        {
            return;
        }

        groupDiagramItem.Draw(drawingContext);
    }

    /// <inheritdoc />
    public override bool CheckShapeHit(ref SKPoint position)
    {
        return groupDiagramItem.CheckShapeHit(ref position);
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseDown(MouseParameters mouse)
    {
        return groupDiagramItem.HandleDiagramMouseDown(mouse);
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseUp(MouseParameters mouse)
    {
        return groupDiagramItem.HandleDiagramMouseUp(mouse);
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseMove(MovingMouseParameters mouse)
    {
        return groupDiagramItem.HandleDiagramMouseMove(mouse);
    }

    /// <inheritdoc />
    public void AddChildren(IEnumerable<DiagramItem> children)
    {
        var geometryDiagramItems = children
            .OfType<GeometryObjectDiagramItem>()
            .ToList();

        if (geometryDiagramItems.Count != children.Count())
        {
            throw new ArgumentException("The items to add is not geometry diagram items.");
        }

        this.children.AddRange(geometryDiagramItems);
        groupDiagramItem.AddChildren(geometryDiagramItems);

        foreach (var item in geometryDiagramItems)
        {
            item.Group = this;
        }
    }

    /// <inheritdoc />
    public void RemoveChildren(IEnumerable<DiagramItem> children)
    {
        var geometryDiagramItems = children
            .OfType<GeometryObjectDiagramItem>()
            .ToList();

        if (geometryDiagramItems.Count != children.Count())
        {
            throw new ArgumentException("The items to remove is not geometry diagram items.");
        }

        groupDiagramItem.RemoveChildren(geometryDiagramItems);

        foreach (var item in geometryDiagramItems)
        {
            var isRemoveSuccessed = this.children.Remove(item);
        
            if (isRemoveSuccessed)
            {
                item.Group = null;
            }
        }
    }
}