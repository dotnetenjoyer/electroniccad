namespace ElectronicCad.Infrastructure.Abstractions.Services.Projects.Diagrams;

/// <summary>
/// Opens a diagram in the current project.
/// </summary>
public interface IDiagramOpener
{
    /// <summary>
    /// Opens the specified diagram.
    /// </summary>
    /// <param name="diagramId">Diagram id.</param>
    void OpenDiagram(Guid diagramId);
}
