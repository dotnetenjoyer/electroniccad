using System.Collections.Generic;
using SkiaSharp;
using ElectronicCad.Diagramming.Drawing.Items;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

/// <summary>
/// Group of geometry diagram items.
/// </summary>
internal class GeometryGroupDiagramItem : GeometryObjectDiagramItem<GeometryGroup>
{
    public IEnumerable<GeometryObjectDiagramItem> Children => children;

    private readonly List<GeometryObjectDiagramItem> children = new();

    private readonly GroupDiagramItem groupDiagramItem;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryGroup">Geometry group.</param>
    /// <param name="children">Children geometry object diagram items.</param>
    public GeometryGroupDiagramItem(GeometryGroup geometryGroup, 
        IEnumerable<GeometryObjectDiagramItem> children) : base(geometryGroup)
    {
        this.children.AddRange(children);
        groupDiagramItem = new GroupDiagramItem(children);
        
        UpdateViewState();
    }

    /// <summary>
    /// Adds a new child.
    /// </summary>
    /// <param name="child">Child to add.</param>
    public void AddChild(GeometryObjectDiagramItem child)
    {
        children.Add(child);
        groupDiagramItem.AddChild(child);
    }

    /// <summary>
    /// Removes the child.
    /// </summary>
    /// <param name="child">Child to remove.</param>
    public void RemoveChild(GeometryObjectDiagramItem child)
    {
        children.Remove(child);
        groupDiagramItem.RemoveChild(child);
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
        groupDiagramItem.Draw(drawingContext);
    }

    /// <inheritdoc />
    public override bool CheckBoundingBoxHit(ref SKPoint point)
    {
        return groupDiagramItem.CheckBoundingBoxHit(ref point);
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
}