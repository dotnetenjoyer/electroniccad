using ElectronicCad.Infrastructure.Abstractions.Models.Projects;
using ElectronicCad.MVVM.Common;
using ElectronicCad.UseCases.Projects.RecentProjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicCad.MVVM.ViewModels.Welcome
{
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
            set 
            {
                _recentProjects = value;
                OnPropertyChanged();
            }
        }

        private IEnumerable<LocalProject> _recentProjects;

        /// <inheritdoc/>
        public async override Task LoadAsync()
        {
            var recentProjects = await _mediator.Send(new GetRecentProjectsQuery());
        }
    }
}
