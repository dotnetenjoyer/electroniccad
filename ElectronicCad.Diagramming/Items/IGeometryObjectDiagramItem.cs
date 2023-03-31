using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Describes diagram item associated with the geometry object.
/// </summary>
internal interface IGeometryObjectDiagramItem
{
    /// <summary>
    /// Associated geometry object.
    /// </summary>
    GeometryObject GeometryObject { get; }
}