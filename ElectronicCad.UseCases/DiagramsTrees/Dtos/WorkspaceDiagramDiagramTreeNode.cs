using WorkspaceDiagram = ElectronicCad.Domain.Workspace.Diagram;

namespace ElectronicCad.UseCases.DiagramsTrees.Dtos;

/// <summary>
/// Represents workspace diagram in the diagram tree.
/// </summary>
public class WorkspaceDiagramDiagramTreeNode : DiagramTreeNode<WorkspaceDiagram>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Workspace diagram.</param>
    public WorkspaceDiagramDiagramTreeNode(WorkspaceDiagram diagram, IEnumerable<TreeNode> nodes) : base(diagram)
    {
        Nodes = nodes;
    }
}