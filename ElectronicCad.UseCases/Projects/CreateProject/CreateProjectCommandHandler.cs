using MediatR;
using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using ElectronicCad.Domain.Workspace;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using CreateProjectDomainCommand = ElectronicCad.Domain.Workspace.Commands.CreateProjectCommand;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.Infrastructure.Abstractions.Models.Projects;
using AutoMapper;

namespace ElectronicCad.UseCases.Projects.CreateProject;

/// <summary>
/// Create new project command handler.
/// </summary>
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
{
    private readonly IFolderPicker folderPicker;
    private readonly IProjectSaver projectSaver;
    private readonly IRecentProjectsService recentProjectsService;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateProjectCommandHandler(IFolderPicker folderPicker, IProjectSaver projectSaver, 
        IRecentProjectsService recentProjectService, IMapper mapper)
    {
        this.folderPicker = folderPicker;
        this.projectSaver = projectSaver;
        this.recentProjectsService = recentProjectService;
        this.mapper = mapper;
    }
    
    /// <inheritdoc/>
    public async Task<Unit> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = CreateProject(command);
        var projectFileName = GetProjectFolderPath(command.FileName);
        await projectSaver.Save(project, projectFileName, cancellationToken);
            
        var localProject = new LocalProject
        {
            Name = command.Name,
            Description = command.Description,
            Customer = command.Customer,
            Path = projectFileName,
            LastAccessTime = DateTime.Now
        };

        await recentProjectsService.AddRecentProject(localProject, cancellationToken);

        return new Unit();
    }

    private Project CreateProject(CreateProjectCommand command)
    {
        var createCommand = mapper.Map<CreateProjectDomainCommand>(command);
        var project = Project.Create(createCommand);
        return project;
    }

    private string GetProjectFolderPath(string projectFileName)
    {
        var projectLocation = folderPicker.PickFolder("Select project folder");
        var projectFile = Path.Combine(projectLocation, projectFileName);

        if (Directory.Exists(projectFile) && Directory.GetFiles(projectFile).Any())
        {
            throw new Exception($"Cannot create {projectFile}, because a directory with the " +
                                $"same name already exits and contains files.");
        }

        return projectFile;
    }
}