using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Base implementation of <see cref="IGeometryObjectDiagramItem"/>
/// </summary>
internal abstract class GeometryObjectDiagramItem : DiagramItem, IGeometryObjectDiagramItem
{
    /// <summary>
    /// Domain geometry object.
    /// </summary>
    public GeometryObject GeometryObject { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="domainObject">Domain object.</param>
    public GeometryObjectDiagramItem(GeometryObject domainObject)
    {
        GeometryObject = domainObject;
        UpdateViewState();
    }

    /// <inheritdoc />
    public virtual void UpdateViewState()
    {
        RecalculateBoundingBox();
    }

    /// <summary>
    /// Recalculate bounding box.
    /// </summary>
    protected void RecalculateBoundingBox()
    {
        var boundingBox = GeometryObject.CalculateBoundingBox();
        BoundingBox = boundingBox.ToSKRect();
    }

    /// <inheritdoc />
    public override bool CheckHit(ref SKPoint position)
    {
        var domainPoint = new Point(position.X, position.Y);
        return GeometryObject.CheckHit(domainPoint);
    }
}
