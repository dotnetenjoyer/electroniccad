using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using ElectronicCad.Infrastructure.Abstractions.Models.Projects;
using Newtonsoft.Json;

namespace ElectronicCad.Infrastructure.Implementations.Services;

/// <summary>
/// Implementation of recent project service.
/// </summary>
public class RecentProjectService : IRecentProjectsService
{
    private readonly string _applicationDataFolderPath; 

    /// <summary>
    /// Constructor.
    /// </summary>
    public RecentProjectService(string applicationDataFolderPath)
    {
        _applicationDataFolderPath = applicationDataFolderPath;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<LocalProject>> GetRecentProjects()
    {
        var content = await File.ReadAllTextAsync(GetFilePath());
        var recentProjects = JsonConvert.DeserializeObject<IEnumerable<LocalProject>>(content);

        throw new NotImplementedException();
    }
    
    /// <inheritdoc/>
    public Task AddRecentProjectInfo(LocalProject localProject)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task UpdateRecentProjectInfo(LocalProject localProject)
    {
        throw new NotImplementedException();
    }

    private string GetFilePath()
    {
        return Path.Combine(_applicationDataFolderPath, "recentProjects.json");
    }
}
