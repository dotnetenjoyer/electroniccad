using ElectronicCad.Domain.Workspace.Commands;

using GeometryDiagram = ElectronicCad.Domain.Geometry.Diagram;

namespace ElectronicCad.Domain.Workspace;

/// <summary>
/// Project.
/// </summary>
public class Project
{
    /// <summary>
    /// Project name.
    /// </summary>
    public string Name { get; internal set; }

    /// <summary>
    /// Project creation date.
    /// </summary>
    public DateTime CreatedAt { get; internal set; }

    /// <summary>
    /// Project diagrams.
    /// </summary>
    public IEnumerable<Diagram> Diagrams => _diagrams;

    private readonly List<Diagram> _diagrams;

    /// <summary>
    /// Constructor.
    /// </summary>
    internal Project()
    {
        _diagrams = new List<Diagram>();

        var geometryDiagram = new GeometryDiagram();
        var diagram = new ProjectDiagram("Diagram", geometryDiagram);
        _diagrams.Add(diagram);
    }

    /// <summary>
    /// Create a new project.
    /// </summary>
    /// <returns></returns>
    public static Project Create(CreateProjectCommand command)
    {
        var project = new Project()
        {
            Name = command.Name,
            CreatedAt = DateTime.Now
        };

        return project;
    }
}