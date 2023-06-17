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
        project.CreatedAt = dictionary.Parse<DateTime>("createdAt");
        project.Name = dictionary.Parse<string>("name");
        project.Description = dictionary.Parse<string>("description");
        project.Customer = dictionary.Parse<string>("customer");
        project.CustomerContact = dictionary.Parse<string>("customerContact");
        return project;
    }

    /// <inheritdoc />
    public void Serialize(StorageDictionary dictionary, Project project)
    {
        dictionary["createdAt"] = project.CreatedAt;
        dictionary["name"] = project.Name;
        dictionary["description"] = project.Description;
        dictionary["customer"] = project.Customer;
        dictionary["customerContact"] = project.CustomerContact;
    }
}