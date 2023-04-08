using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.UseCases.DiagramsTrees.Dtos;

/// <summary>
/// Represents geometry object in diagram tree.
/// </summary>
public class GeometryDiagramTreeNode : DiagramTreeNode<GeometryObject>
{
    /// <inheritdoc />
    public override string Name => TypedDomainObject.Name;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    public GeometryDiagramTreeNode(GeometryObject geometryObject) : base(geometryObject)
    {
    }
}