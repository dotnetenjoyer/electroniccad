using MediatR;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.Infrastructure.Abstractions.Models.Projects;

namespace ElectronicCad.UseCases.Projects.GetRecentProjects;

/// <summary>
/// Get recent projects query handler.
/// </summary>
public class GetRecentProjectsQueryHandler : IRequestHandler<GetRecentProjectsQuery, IReadOnlyList<LocalProject>>
{
    private readonly IRecentProjectsService _recentProjectsService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetRecentProjectsQueryHandler(IRecentProjectsService recentProjectsService)
    {
        _recentProjectsService = recentProjectsService;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<LocalProject>> Handle(GetRecentProjectsQuery request, CancellationToken cancellationToken)
    {
        var recentProjects = await _recentProjectsService.GetRecentProjects(cancellationToken);
        return recentProjects.ToList();
    }
}