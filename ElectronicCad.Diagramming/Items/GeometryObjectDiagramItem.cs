using ElectronicCad.Domain.Geometry;
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

    /// <summary>
    /// Check point hitting.
    /// </summary>
    /// <param name="position">Point position.</param>
    /// <returns>Whether hit or not.</returns>
    //public override bool CheckHit(Point position)
    //{
    //    var pointF = new System.Drawing.PointF((float)position.X, (float)position.Y);
    //    return DomainObject.CheckHit(pointF);
    //}
}
