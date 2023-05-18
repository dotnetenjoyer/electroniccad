using ElectronicCad.Domain.Workspace;

namespace ElectronicCad.Infrastructure.Abstractions.Services.Projects.Diagrams;

/// <summary>
/// Provides a current open diagram and an event about its change.
/// </summary>
public interface IOpenDiagramProvider
{
    /// <summary>
    /// The current open diagram.
    /// </summary>
    Diagram Diagram { get; }

    /// <summary>
    /// Raises when opens a new diagram.
    /// </summary>
    event EventHandler OpenDiagramChanged;
}