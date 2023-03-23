using MediatR;

namespace ElectronicCad.UseCases.Projects.UpdateProject;

public class UpdateProjectCommand : IRequest
{

}

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    public Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
