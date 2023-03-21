using ElectronicCad.Infrastructure.Abstractions.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicCad.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Recnet projects service interface.
/// </summary>
public interface IRecentProjectsService
{
    /// <summary>
    /// Returns recent open projects.
    /// </summary>
    Task<IEnumerable<LocalProject>> GetRecentProjects();

    /// <summary>
    /// Add informaiton about recent project.
    /// </summary>
    Task AddRecentProjectInfo(LocalProject localProject);

    /// <summary>
    /// Update information about recent project.
    /// </summary>
    Task UpdateRecentProjectInfo(LocalProject localProject);
}
