using AutoMapper;
using ElectronicCad.UseCases.Projects.CreateProject;
using CreateProjectDomainCommand = ElectronicCad.Domain.Workspace.Commands.CreateProjectCommand;

namespace ElectronicCad.UseCases.Projects;

/// <summary>
/// Projects automapper profile.
/// </summary>
public class ProjectsProfile : Profile
{
    public ProjectsProfile()
    {
        CreateMap<CreateProjectCommand, CreateProjectDomainCommand>();
    }
}