using ElectronicCad.Domain.Storage;
using ElectronicCad.Domain.Workspace;
using ElectronicCad.Domain.Workspace.Serializers;
using ElectronicCad.Infrastructure.Abstractions.Interfaces.Projects;
using ElectronicCad.Infrastructure.Implementations.Services.Storages;

namespace ElectronicCad.Infrastructure.Implementations.Services.Projects;

/// <summary>
/// Project saver implementation.
/// </summary>
public class ProjectSaver : IProjectSaver
{
    /// <inheritdoc />
    public async Task Save(Project project, string folderPath, CancellationToken cancellationToken)
    {
        var projectSerializer = new ProjectSerializer();
        var storageDictionary = new StorageDictionary();
        projectSerializer.Serialize(storageDictionary, project);
        
        IStorage storage = new FolderYamlStorage(folderPath);
        await storage.PutDictionaryAsync(storageDictionary, "index", cancellationToken);
    }
}
