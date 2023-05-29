using System.ComponentModel;

namespace ElectronicCad.UseCases.DiagramsTrees.Dtos;

/// <summary>
/// Represent tree node.
/// </summary>
public abstract class TreeNode
{
    /// <summary>
    /// Tree node name.
    /// </summary>
    public virtual string Name { get; } = string.Empty;

    /// <summary>
    /// Indicates if current node expanded.
    /// </summary>
    public bool IsExpanded { get; set; } = true;

    /// <summary>
    /// Indicates if current node selected.
    /// </summary>
    public bool IsSelected { get; set; }

    /// <summary>
    /// Related diagram node.
    /// </summary>
    public object NodeObject { get; init; }

    /// <summary>
    /// Nested nodes.
    /// </summary>
    public IEnumerable<TreeNode>? Nodes { get; init; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="nodeObject">Node object.</param>
    public TreeNode(object nodeObject)
    {
        NodeObject = nodeObject;
    }
}

/// <summary>
/// Diagram tree node.
/// </summary>
/// <typeparam name="TDiagramObject">Diagram object.</typeparam>
public abstract class DiagramTreeNode<TDiagramObject> : TreeNode 
{
    /// <summary>
    /// Domain object.
    /// </summary>
    public TDiagramObject DiagramObject { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagramObject">Diagram object.</param>
    public DiagramTreeNode(TDiagramObject diagramObject) : base(diagramObject)
    {
        DiagramObject = diagramObject;
    }
}
