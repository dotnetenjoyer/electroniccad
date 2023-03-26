using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using ElectronicCad.Domain.Storage;

namespace ElectronicCad.Infrastructure.Implementations.Services.Storages;

/// <summary>
/// Folder ymal storage
/// </summary>
public class FolderYamlStorage : IStorage
{
    private readonly string _folderPath;
    private const string YamlExtension = ".yaml";

    /// <summary>
    /// Constructor.
    /// </summary>
    public FolderYamlStorage(string folderPath)
    {
        _folderPath = folderPath;
    }

    /// <inheritdoc />
    public async Task<StorageDictionary> GetDictionaryAsync(string @ref, CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(_folderPath, @ref);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File {filePath} not foudn");
        }

        var yaml = await File.ReadAllTextAsync(filePath, cancellationToken);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var storageDictionary = deserializer.Deserialize<StorageDictionary>(yaml);
        return storageDictionary;
    }

    /// <inheritdoc />
    public async Task PutDictionaryAsync(StorageDictionary dictionary, string @ref, CancellationToken cancellationToken)
    {
        if (!Directory.Exists(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);
        }

        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var yaml = serializer.Serialize(dictionary);
        var filePath = Path.Combine(_folderPath, Path.ChangeExtension(@ref, YamlExtension));

        using var fileStream = File.Open(filePath, FileMode.CreateNew);
        var content = Encoding.UTF8.GetBytes(yaml);
        fileStream.Write(content);
    }
}