using ElectronicCad.Domain.Geometry;
using SkiaSharp.Views.Desktop;
using System.Windows;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Base class for diagram items, that binds with domain geomtry objects.
/// </summary>
internal abstract class GeometryDiagramItem<TGeometry> : DiagramItem where TGeometry : GeometryObject
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
        RecalculateBoundingBox();
    }

    /// <summary>
    /// Recalculate bounding box.
    /// </summary>
    protected void RecalculateBoundingBox()
    {
        var boundingBox = DomainObject.CalculateBoundingBox();
        BoundingBox = boundingBox.ToSKRect();
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