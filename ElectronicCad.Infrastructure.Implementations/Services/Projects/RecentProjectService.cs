using ElectronicCad.Infrastructure.Abstractions.Interfaces;
using ElectronicCad.Infrastructure.Abstractions.Models.Projects;
using Newtonsoft.Json;

namespace ElectronicCad.Infrastructure.Implementations.Services.Projects;

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
        var recentProjects = await GetRecentProjectsInternal();
        return recentProjects;
    }

    private async Task<List<LocalProject>> GetRecentProjectsInternal()
    {
        var content = await File.ReadAllTextAsync(GetFilePath());
        return JsonConvert.DeserializeObject<List<LocalProject>>(content) ?? new List<LocalProject>();
    }

    /// <inheritdoc/>
    public async Task AddRecentProjectInfo(LocalProject project)
    {
        var recentProjects = await GetRecentProjectsInternal();
        recentProjects.Add(project);

        await RefreshRecentProjects(recentProjects);
    }

    /// <inheritdoc/>
    public async Task UpdateRecentProjectInfo(LocalProject project)
    {
        if (project.Id == Guid.Empty)
        {
            throw new Exception();
        }


        var recentProjects = await GetRecentProjectsInternal();

        var index = recentProjects.FindIndex(_ => _.Id == project.Id);
        if (index == 0)
        {
            throw new Exception();
        }

        recentProjects[index] = project;


        await RefreshRecentProjects(recentProjects);

    }

    private string GetFilePath()
    {
        return Path.Combine(_applicationDataFolderPath, "recentProjects.json");
    }

    private async Task RefreshRecentProjects(IEnumerable<LocalProject> projects)
    {
        var newContent = JsonConvert.SerializeObject(projects);
        await File.WriteAllTextAsync(GetFilePath(), newContent);
    }
}
