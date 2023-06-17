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
    /// Project description
    /// </summary>
    public string Description { get; internal set; }

    /// <summary>
    /// Customer
    /// </summary>
    public string Customer { get; internal set; }

    /// <summary>
    /// Customer contact.
    /// </summary>
    public string CustomerContact { get; internal set; }

    /// <summary>
    /// Project diagrams.
    /// </summary>
    public IEnumerable<Diagram> Diagrams => diagrams;

    private readonly List<Diagram> diagrams;

    /// <summary>
    /// Constructor.
    /// </summary>
    internal Project()
    {
        diagrams = new List<Diagram>();

        var geometryDiagram = new GeometryDiagram();
        var diagram = new ProjectDiagram("Холст", geometryDiagram);
        diagrams.Add(diagram);
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
            Description = command.Description,
            CreatedAt = DateTime.Now,
            Customer = command.Customer,
            CustomerContact = command.CustomerContact,
        };

        return project;
    }
}