using ElectronicCad.Diagramming.Drawing.Items;
using ElectronicCad.Domain.Geometry;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

internal class GeometryGroupDiagramItem : GeometryObjectDiagramItem<GeometryGroup>
{
    private readonly GroupDiagramItem group;
    private readonly IEnumerable<GeometryObjectDiagramItem> children;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="group"></param>
    public GeometryGroupDiagramItem(GeometryGroup group, IEnumerable<GeometryObjectDiagramItem> children) : base(group)
    {
        this.group = new GroupDiagramItem(children);
        this.children = children;
        
        UpdateViewState();
    }

    /// <inheritdoc />
    public override void UpdateViewState()
    {
        base.UpdateViewState();
     
        foreach (var child in children)
        {
            child.UpdateViewState();
        }

        group.RecalculateBoundingBox();
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext drawingContext)
    {
        group.Draw(drawingContext);
    }

    public override bool CheckBoundingBoxHit(ref SKPoint point)
    {
        return group.CheckBoundingBoxHit(ref point);
    }

    public override bool CheckShapeHit(ref SKPoint position)
    {
        return group.CheckShapeHit(ref position);
    }
}