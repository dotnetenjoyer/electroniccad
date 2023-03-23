namespace ElectronicCad.Domain.Woorkbook;

/// <summary>
/// Project.
/// </summary>
public class Project
{
    /// <summary>
    /// Project id.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Project name.
    /// </summary>
    public string Name { get;  }

    /// <summary>
    /// Project diagrams.
    /// </summary>
    public IEnumerable<Diagram> Diagrams => _diagrams;

    private readonly List<Diagram> _diagrams;

    /// <summary>
    /// Constructor.
    /// </summary>
    public Project()
    {
        _diagrams = new List<Diagram>();
    }
}