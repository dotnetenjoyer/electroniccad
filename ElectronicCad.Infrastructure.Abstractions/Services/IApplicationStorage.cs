namespace ElectronicCad.Infrastructure.Abstractions.Services;

/// <summary>
/// Local application data storage
/// </summary>
public interface IApplicationLocalStorage
{
    /// <summary>
    /// Saves a file in the application local storage.
    /// </summary>
    /// <param name="pathToFile">Path to a file.</param>
    /// <returns>Path to the saved file in the local storage.</returns>
    string SaveFile(string pathToFile);
}