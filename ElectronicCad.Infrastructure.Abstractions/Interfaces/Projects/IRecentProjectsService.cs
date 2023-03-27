using ElectronicCad.Infrastructure.Abstractions.Models.Projects;

namespace ElectronicCad.Infrastructure.Abstractions.Interfaces.Project;

/// <summary>
/// Recnet projects service interface.
/// </summary>
public interface IRecentProjectsService
{
    /// <summary>
    /// Returns recent open projects.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Recent projects.</returns>
    Task<IEnumerable<LocalProject>> GetRecentProjects(CancellationToken cancellationToken);

    /// <summary>
    /// Add informaiton about recent project.
    /// </summary>
    /// <param name="localProject">Local projects.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddRecentProject(LocalProject localProject, CancellationToken cancellationToken);

    /// <summary>
    /// Update information about recent project.
    /// </summary>
    /// <param name="localProject">Local projects.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateRecentProject(LocalProject localProject, CancellationToken cancellationToken);
}
