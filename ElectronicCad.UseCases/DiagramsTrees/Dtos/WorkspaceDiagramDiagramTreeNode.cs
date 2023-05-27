using WorkspaceDiagram = ElectronicCad.Domain.Workspace.Diagram;

namespace ElectronicCad.UseCases.DiagramsTrees.Dtos;

/// <summary>
/// Represents workspace diagram in the diagram tree.
/// </summary>
public class WorkspaceDiagramDiagramTreeNode : DiagramTreeNode<WorkspaceDiagram>
{
    /// <inheritdoc />
    public override string Name => DomainObject.Name;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Workspace diagram.</param>
    public WorkspaceDiagramDiagramTreeNode(WorkspaceDiagram diagram, IEnumerable<DiagramTreeNode> nodes) : base(diagram)
    {
        Nodes = nodes;
    }
}