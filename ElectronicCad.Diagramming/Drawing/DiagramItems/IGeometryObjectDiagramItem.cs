using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Describes diagram item associated with the geometry object.
/// </summary>
internal interface IGeometryObjectDiagramItem
{
    /// <summary>
    /// Associated geometry object.
    /// </summary>
    GeometryObject GeometryObject { get; }

    /// <summary>
    /// Updates view state from domain object.
    /// </summary>
    void UpdateViewState();
}