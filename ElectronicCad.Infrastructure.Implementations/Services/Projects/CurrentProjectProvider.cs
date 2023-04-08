
using ElectronicCad.Domain.Workspace;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;

namespace ElectronicCad.Infrastructure.Implementations.Services.Projects;

/// <summary>
/// Provides the current opened project.
/// </summary>
public class CurrentProjectProvider : ICurrentProjectProvider
{
    private Project currentProject;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CurrentProjectProvider()
    {
        currentProject = Project.Create(new Domain.Workspace.Commands.CreateProjectCommand
        {
            Name = "Temp project"
        });
    }

    public Project GetCurrentProject()
    {
        return currentProject;
    }
}