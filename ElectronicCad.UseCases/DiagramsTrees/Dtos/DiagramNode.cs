using System.ComponentModel;

namespace ElectronicCad.UseCases.DiagramsTrees.Dtos;

/// <summary>
/// Node of the diagrams tree.
/// </summary>
public class DiagramTreeNode
{
    /// <summary>
    /// Related diagram node.
    /// </summary>
    public INotifyPropertyChanged DomainObject { get; init; }

    /// <summary>
    /// Nested nodes.
    /// </summary>
    public IEnumerable<DiagramTreeNode> Nodes { get; set; }
}