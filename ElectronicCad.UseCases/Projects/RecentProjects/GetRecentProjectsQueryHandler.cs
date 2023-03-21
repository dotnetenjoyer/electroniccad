using System.Diagnostics;
using MediatR;

namespace ElectronicCad.UseCases.Projects.RecentProjects;

/// <summary>
/// Get recent projects query handler.
/// </summary>
public class GetRecentProjectsQueryHandler : IRequestHandler<GetRecentProjectsQuery>
{
    /// <inheritdoc/>
    public async Task<Unit> Handle(GetRecentProjectsQuery request, CancellationToken cancellationToken)
    {
        Debug.WriteLine("Get recent projects.");
        return new Unit();
    }
}