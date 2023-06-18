using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.UseCases.DiagramsTrees.Dtos;

/// <summary>
/// Represents the geometry object in the diagram tree.
/// </summary>
public class GeometryDiagramTreeNode : DiagramTreeNode<GeometryObject>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="geometryObject">Geometry object.</param>
    public GeometryDiagramTreeNode(GeometryObject geometryObject, IEnumerable<TreeNode>? nodes = null) : base(geometryObject)
    {
        Nodes = nodes ?? Array.Empty<TreeNode>();
    }
}