namespace ElectronicCad.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// File picker abstraction.
/// </summary>
public interface IFilePicker
{
    /// <summary>
    /// Picks file with dialog.
    /// </summary>
    /// <returns>Path to selected file.</returns>
    string PickFile();
}