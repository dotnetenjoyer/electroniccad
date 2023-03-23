using MediatR;

namespace ElectronicCad.UseCases.Projects.CreateProject;

/// <summary>
/// Create new project command.
/// </summary>
public class CreateProjectCommand : IRequest
{
    /// <summary>
    /// Project name.
    /// </summary>
    public string ProjectName { get; set; }

    /// <summary>
    /// Project folder name.
    /// </summary>
    public string ProjectFolderName { get; set; }
}