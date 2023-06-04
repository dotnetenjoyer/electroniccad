namespace ElectronicCad.UseCases.DiagramsTrees.Dtos;

/// <summary>
/// Containsn diagram trees.
/// </summary>
public class DiagramTrees
{
    /// <summary>
    /// Diagram trees.
    /// </summary>
    public IEnumerable<TreeNode> Diagrams { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DiagramTrees(IEnumerable<TreeNode> diagrams)
    {
        Diagrams = diagrams;
    }
}
