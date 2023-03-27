using MediatR;
using ElectronicCad.Infrastructure.Abstractions.Models.Projects;
using ElectronicCad.MVVM.Common;
using ElectronicCad.UseCases.Projects.GetRecentProjects;

namespace ElectronicCad.MVVM.ViewModels.Welcome;

/// <summary>
/// Recent projects view model.
/// </summary>
public class RecentProjectsViewModel : ViewModel
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public RecentProjectsViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Recent projects
    /// </summary>
    public IEnumerable<LocalProject> RecentProjects
    { 
        get => _recentProjects;
        private set => SetProperty(ref _recentProjects, value);
    }

    private IEnumerable<LocalProject> _recentProjects;

    /// <inheritdoc/>
    public async override Task LoadAsync()
    {
        var recentProjects = await _mediator.Send(new GetRecentProjectsQuery());
        RecentProjects = recentProjects;
    }
}
