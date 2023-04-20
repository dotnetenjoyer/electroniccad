using System.ComponentModel;

namespace ElectronicCad.UseCases.DiagramsTrees.Dtos;

/// <summary>
/// Node of the diagrams tree.
/// </summary>
public abstract class DiagramTreeNode
{
    /// <summary>
    /// Tree node name.
    /// </summary>
    public virtual string Name { get; set; }

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
    public INotifyPropertyChanged DomainObject { get; init; }

    /// <summary>
    /// Nested nodes.
    /// </summary>
    public IEnumerable<DiagramTreeNode> Nodes { get; init; }
}

/// <summary>
/// Diagram tree node with typed domain object.
/// </summary>
/// <typeparam name="TDomainObject">Type of domain object.</typeparam>
public abstract class DiagramTreeNode<TDomainObject> : DiagramTreeNode where TDomainObject : INotifyPropertyChanged
{
    /// <summary>
    /// Typed domain object.
    /// </summary>
    public TDomainObject TypedDomainObject { get; init; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="typedDomainObject">Typed domain object.</param>
    public DiagramTreeNode(TDomainObject typedDomainObject)
    {
        TypedDomainObject = typedDomainObject;
        DomainObject = typedDomainObject;
    }
}
