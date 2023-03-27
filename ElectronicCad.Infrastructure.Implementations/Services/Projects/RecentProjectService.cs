using System.Text;
using Newtonsoft.Json;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Project;
using ElectronicCad.Infrastructure.Abstractions.Models.Projects;

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
    public async Task<IEnumerable<LocalProject>> GetRecentProjects(CancellationToken cancellationToken)
    {
        var recentProjects = await GetRecentProjectsInternal(cancellationToken);
        return recentProjects;
    }

    /// <inheritdoc/>
    public async Task AddRecentProject(LocalProject project, CancellationToken cancellationToken)
    {
        var recentProjects = await GetRecentProjectsInternal(cancellationToken);
        recentProjects.Add(project);

        await SaveRecentProjects(recentProjects, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateRecentProject(LocalProject project, CancellationToken cancellationToken)
    {
        if (project.Id == Guid.Empty)
        {
            throw new Exception();
        }

        var recentProjects = await GetRecentProjectsInternal(cancellationToken);
        var index = recentProjects.FindIndex(_ => _.Id == project.Id);
        if (index == 0)
        {
            throw new Exception();
        }

        recentProjects[index] = project;
        await SaveRecentProjects(recentProjects, cancellationToken);
    }

    private async Task<List<LocalProject>> GetRecentProjectsInternal(CancellationToken cancellationToken)
    {
        var filePath = GetDataFilePath();
        if(!File.Exists(filePath))
        {
            return new List<LocalProject>();
        }

        var content = await File.ReadAllTextAsync(filePath, cancellationToken);
        return JsonConvert.DeserializeObject<List<LocalProject>>(content) ?? new List<LocalProject>();
    }

    private string GetDataFilePath()
    {
        return Path.Combine(_applicationDataFolderPath, "recentProjects.json");
    }

    private async Task SaveRecentProjects(IEnumerable<LocalProject> projects, CancellationToken cancellationToken)
    {
        var filePath = GetDataFilePath();
        await using var fileStream = File.Open(filePath, FileMode.OpenOrCreate);
        
        var json = JsonConvert.SerializeObject(projects, Formatting.Indented);
        var fileContent = Encoding.UTF8.GetBytes(json);
        
        await fileStream.WriteAsync(fileContent, cancellationToken);
    }
}
