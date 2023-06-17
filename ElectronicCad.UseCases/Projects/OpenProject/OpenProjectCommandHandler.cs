using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using MediatR;

namespace ElectronicCad.UseCases.Projects.OpenProject;

/// <summary>
/// Handle for <see cref="OpenProjectCommand"/>
/// </summary>
public class OpenProjectCommandHandler : IRequestHandler<OpenProjectCommand>
{
    private readonly IFilePicker filePicker;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public OpenProjectCommandHandler(IFilePicker filePicker)
    {
        this.filePicker = filePicker;
    }

    /// <inheritdoc />
    public Task<Unit> Handle(OpenProjectCommand request, CancellationToken cancellationToken)
    {
        var file = filePicker.PickFile();
        return Task.FromResult(new Unit());
    }
}