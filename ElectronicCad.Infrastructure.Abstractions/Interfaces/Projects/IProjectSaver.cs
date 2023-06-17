using ElectronicCad.Domain.Workspace;

namespace ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;

/// <summary>
/// Project saver abstraction.
/// </summary>
public interface IProjectSaver
{
    /// <summary>
    /// Save project to local file space.
    /// </summary>
    /// <param name="project">Project to save.</param>
    /// <param name="folderPath">Location of project.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task Save(Project project, string folderPath, CancellationToken cancellationToken);
}
