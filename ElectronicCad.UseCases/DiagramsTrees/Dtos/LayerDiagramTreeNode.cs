using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.UseCases.DiagramsTrees.Dtos;

/// <summary>
/// Represent the layer in the diagram tree.
/// </summary>
public class LayerDiagramTreeNode : DiagramTreeNode<Layer>
{
    /// <inheritdoc />
    public override string Name => DiagramObject.Name;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="layer">Layer.</param>
    public LayerDiagramTreeNode(Layer layer, IEnumerable<TreeNode> nodes) : base(layer)
    {
        Nodes = nodes;
    }
}
