using ElectronicCad.Domain.Workspace;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects.Diagrams;

namespace ElectronicCad.Infrastructure.Implementations.Services.Projects.Diagrams;

/// <summary>
/// Opens a diagram in the current project.
/// </summary>
public class ProjectDiagramOpener : IOpenDiagramProvider, IDiagramOpener
{
    private readonly IOpenProjectProvider openProjectProvider;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="openProjectProvider"></param>
    public ProjectDiagramOpener(IOpenProjectProvider openProjectProvider)
    {
        this.openProjectProvider = openProjectProvider;

        var project = openProjectProvider.GetOpenProject();
        Diagram = project.Diagrams.First();
    }

    /// <inheritdoc />
    public Diagram Diagram { get; private set; }

    /// <inheritdoc />
    public event EventHandler OpenDiagramChanged;

    /// <inheritdoc />
    public void OpenDiagram(Guid diagramId)
    {
        var project = openProjectProvider.GetOpenProject();
        var diagramToOpen = project.Diagrams.First(x => x.Id == diagramId);
        
        Diagram = diagramToOpen;
        OpenDiagramChanged?.Invoke(this, EventArgs.Empty);
    }
}