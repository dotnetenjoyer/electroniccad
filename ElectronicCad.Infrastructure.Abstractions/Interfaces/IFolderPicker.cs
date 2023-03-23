namespace ElectronicCad.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Folder picker abstraction.
/// </summary>
public interface IFolderPicker
{
    /// <summary>
    /// Return selected direcotry path.
    /// </summary>
    /// <param name="description">Folder picker dialog descirption.</param>
    /// <returns>Selected path.</returns>
    string PickFolder(string description = null);
}
