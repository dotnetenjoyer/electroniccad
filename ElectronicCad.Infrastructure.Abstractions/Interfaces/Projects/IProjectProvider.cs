using ElectronicCad.Domain.Workspace;

namespace ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;

/// <summary>
/// Provides the curret open project.
/// </summary>
public interface ICurrentProjectProvider
{
    Project GetCurrentProject();
}
