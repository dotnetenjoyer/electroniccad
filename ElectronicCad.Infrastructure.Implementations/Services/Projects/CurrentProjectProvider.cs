
using ElectronicCad.Domain.Workspace;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects;

namespace ElectronicCad.Infrastructure.Implementations.Services.Projects;

/// <summary>
/// Implementation of <see cref="IOpenProjectProvider"/>.
/// </summary>
public class OpentProjectProvider : IOpenProjectProvider
{
    private Project currentProject;

    /// <summary>
    /// Constructor.
    /// </summary>
    public OpentProjectProvider()
    {
        currentProject = Project.Create(new Domain.Workspace.Commands.CreateProjectCommand
        {
            Name = "Temp project"
        });
    }

    public Project GetOpenProject()
    {
        return currentProject;
    }
}