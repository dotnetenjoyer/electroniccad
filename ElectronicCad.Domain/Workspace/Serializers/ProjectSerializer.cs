using ElectronicCad.Domain.Storage;

namespace ElectronicCad.Domain.Workspace.Serializers;

/// <summary>
/// Project serializer.
/// </summary>
public class ProjectSerializer : IEntitySerializer<Project>
{
    /// <inheritdoc />
    public Project Deserialize(StorageDictionary dictionary)
    {
        var project = new Project();
        project.Name = dictionary.Parse<string>("name");
        project.CreatedAt = dictionary.Parse<DateTime>("createdAt");
        return project;
    }

    /// <inheritdoc />
    public void Serialize(StorageDictionary dictionary, Project project)
    {
        dictionary["name"] = project.Name;
        dictionary["createdAt"] = project.CreatedAt;
    }
}