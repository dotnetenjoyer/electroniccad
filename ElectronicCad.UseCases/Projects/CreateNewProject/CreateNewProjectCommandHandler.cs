using System.Diagnostics;
using MediatR;

namespace ElectronicCad.UseCases.Projects.CreateNewProject;

/// <summary>
/// Create new project command handler.
/// </summary>
public class CreateNewProjectCommandHandler : IRequestHandler<CreateNewProjectCommand>
{
    /// <inheritdoc/>
    public async Task<Unit> Handle(CreateNewProjectCommand request, CancellationToken cancellationToken)
    {
        Debug.WriteLine("Create new project");
        return new Unit();
    }
}