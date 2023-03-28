using ElectronicCad.Diagramming.Nodes;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// 
/// </summary>
internal class GeometryDiagramItem<TGeometry> : DiagramItem where TGeometry : GeometryObject
{
    /// <summary>
    /// Domain geometry object.
    /// </summary>
    public TGeometry DomainObject { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="domainObject">Domain object.</param>
    public GeometryDiagramItem(TGeometry domainObject)
    {
        DomainObject = domainObject;
    }

    protected void CalculateBoundingBox()
    {
        var boundingBox = DomainObject.CalculateBoundingBox();
    }

    /// <summary>
    /// Check point hitting.
    /// </summary>
    /// <param name="position">Point position.</param>
    /// <returns>Whether hit or not.</returns>
    public override bool CheckHit(Point position)
    {
        var pointF = new System.Drawing.PointF((float)position.X, (float)position.Y);
        return DomainObject.CheckHit(pointF);
    }
}