using ElectronicCad.Infrastructure.Abstractions.Services;

namespace ElectronicCad.Infrastructure.Implementations.Services;

/// <summary>
/// Local application data storage implementation.
/// </summary>
public class ApplicationLocalStorage : IApplicationLocalStorage
{
    private readonly string applicationLocalPath;

    /// <summary>
    /// Consturctor.
    /// </summary>
    /// <param name="applicationLocalPath">Path to the local application folder.</param>
    public ApplicationLocalStorage(string applicationLocalPath)
    {
        this.applicationLocalPath = applicationLocalPath;
    }

    /// <inheritdoc />
    public string SaveFile(string pathToFile)
    {
        var fileInfo = new FileInfo(pathToFile);
        var newFileName = $"{Guid.NewGuid()}{fileInfo.Extension}";
        var newPathToFile = Path.Combine(applicationLocalPath, newFileName);
        File.Copy(pathToFile, newPathToFile);
        return newPathToFile;
    }
}