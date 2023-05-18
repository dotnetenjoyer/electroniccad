using ElectronicCad.Domain.Workspace;

namespace ElectronicCad.Infrastructure.Abstractions.Services.Projects;

/// <summary>
/// Provides the curret open project.
/// </summary>
public interface IOpenProjectProvider
{
    /// <summary>
    /// Returns the current open project.
    /// </summary>
    /// <returns>Current open project.</returns>
    Project GetOpenProject();
}
