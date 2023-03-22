using System.Diagnostics;
using MediatR;

namespace ElectronicCad.UseCases.Projects.CreateProject;

/// <summary>
/// Create new project command handler.
/// </summary>
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
{
    /// <inheritdoc/>
    public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        Debug.WriteLine("Create new project");
        return new Unit();
    }
}