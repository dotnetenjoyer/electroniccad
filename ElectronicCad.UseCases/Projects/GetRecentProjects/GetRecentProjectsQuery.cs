using ElectronicCad.Infrastructure.Abstractions.Models.Projects;
using MediatR;

namespace ElectronicCad.UseCases.Projects.GetRecentProjects;

/// <summary>
/// Get recent projects query.
/// </summary>
public class GetRecentProjectsQuery : IRequest<IReadOnlyList<LocalProject>>
{
    
}