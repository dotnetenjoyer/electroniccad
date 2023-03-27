using MediatR;
using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using ElectronicCad.Domain.Workspace;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using CreateProjectDomainCommand = ElectronicCad.Domain.Workspace.Commands.CreateProjectCommand;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Project;
using ElectronicCad.Infrastructure.Abstractions.Models.Projects;

namespace ElectronicCad.UseCases.Projects.CreateProject;

/// <summary>
/// Create new project command handler.
/// </summary>
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
{
    private readonly IFolderPicker _folderPicker;
    private readonly IProjectSaver _projectSaver;
    private readonly IRecentProjectsService _recentProjectsService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateProjectCommandHandler(IFolderPicker folderPicker, IProjectSaver projectSaver, 
        IRecentProjectsService recentProjectService)
    {
        _folderPicker = folderPicker;
        _projectSaver = projectSaver;
        _recentProjectsService = recentProjectService;
    }
    
    /// <inheritdoc/>
    public async Task<Unit> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = CreateProject(command);
        var projectFolderPath = GetProjectFolderPath(command.ProjectFolderName);
        await _projectSaver.Save(project, projectFolderPath, cancellationToken);

        var localProject = new LocalProject
        {
            Name = command.ProjectName,
            Path = projectFolderPath,
            LastAccessTime = DateTime.Now
        };
        await _recentProjectsService.AddRecentProject(localProject, cancellationToken);

        return new Unit();
    }

    private Project CreateProject(CreateProjectCommand command)
    {
        var createCommand = new CreateProjectDomainCommand { Name = command.ProjectName };
        var project = Project.Create(createCommand);
        return project;
    }

    private string GetProjectFolderPath(string projectFolderName)
    {
        var projectLocation = _folderPicker.PickFolder("Select project folder");
        var projectFolder = Path.Combine(projectLocation, projectFolderName);

        if (Directory.Exists(projectFolder) && Directory.GetFiles(projectFolder).Any())
        {
            throw new Exception($"Cannot create {projectFolder}, because a directory with the " +
                                $"same name already exits and contains files.");
        }

        return projectFolder;
    }
}