using MediatR;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.Infrastructure.Abstractions.Services.Projects;

namespace ElectronicCad.UseCases.Projects.SaveProject;

/// <summary>
/// Handler for <see cref="SaveProjectCommand"/>.
/// </summary>
public class SaveProjetFolderCommandHandler : IRequestHandler<SaveProjectCommand>
{
    private readonly IProjectSaver projectSaver;
    private readonly IOpenProjectProvider openProjectProvider;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SaveProjetFolderCommandHandler(IProjectSaver projectSaver, IOpenProjectProvider openProjectProvider)
    {
        this.projectSaver = projectSaver;
        this.openProjectProvider = openProjectProvider;
    }

    /// <inhertidoc />
    public async Task<Unit> Handle(SaveProjectCommand command, CancellationToken cancellationToken)
    {
        var openProject = openProjectProvider.GetOpenProject();
        await projectSaver.Save(openProject, string.Empty, cancellationToken);
        return Unit.Value;
    }
}
