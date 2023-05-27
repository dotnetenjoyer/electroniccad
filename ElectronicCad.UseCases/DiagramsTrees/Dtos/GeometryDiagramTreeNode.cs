using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.UseCases.DiagramsTrees.Dtos;

public class LayerDiagramTreeNode : DiagramTreeNode<Layer>
{
    /// <inheritdoc />
    public override string Name => DomainObject.Name;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="layer">Layer.</param>
    public LayerDiagramTreeNode(Layer layer, IEnumerable<DiagramTreeNode> nodes) : base(layer)
    {
        Nodes = nodes;
    }
}

/// <summary>
/// Represents geometry object in diagram tree.
/// </summary>
public class GeometryDiagramTreeNode : DiagramTreeNode<GeometryObject>
{
    /// <inheritdoc />
    public override string Name => DomainObject.Name;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    public GeometryDiagramTreeNode(GeometryObject geometryObject, IEnumerable<DiagramTreeNode>? nodes = null) : base(geometryObject)
    {
        Nodes = nodes ?? Array.Empty<DiagramTreeNode>();
    }
}