using System.Diagnostics;
using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using MediatR;

namespace ElectronicCad.UseCases.Projects.CreateProject;

/// <summary>
/// Create new project command handler.
/// </summary>
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
{
    private readonly IFolderPicker _folderPicker;

    /// <summary>
    /// Constructor.
    /// </summary>
    public CreateProjectCommandHandler(IFolderPicker folderPicker)
    {
        _folderPicker = folderPicker;
    }
    
    /// <inheritdoc/>
    public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var projectLocation = _folderPicker.PickFolder("Select project folder");
        var projectFolder = Path.Combine(projectLocation, request.ProjectFolderName);

        if (Directory.Exists(projectFolder) && Directory.GetFiles(projectFolder).Any())
        {
            throw new Exception($"Cannot create {projectFolder}, because a directory with the " +
                                $"same name already exits and contains files.");
        }

        // create project 
        // add created project to recent.

        Directory.CreateDirectory(projectFolder);
        
        return new Unit();
    }
}