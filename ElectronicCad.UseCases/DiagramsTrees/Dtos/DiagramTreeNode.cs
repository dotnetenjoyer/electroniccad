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

    public override bool Equals(object? obj)
    {
        /// Hack: allow updates selected nodes
        /// Todo: figure out why TreeViewIte.Header not updates.
        if (obj is TreeNode anotherNode)
        {
            return Name == anotherNode.Name && IsExpanded == anotherNode.IsExpanded 
                && NodeObject == anotherNode.NodeObject;
        } 

        return base.Equals(obj);
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
