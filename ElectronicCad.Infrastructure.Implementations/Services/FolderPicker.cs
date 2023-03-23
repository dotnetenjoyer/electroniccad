using ElectronicCad.Infrastructure.Abstractions.Interfaces;

namespace ElectronicCad.Infrastructure.Implementations.Services;

/// <summary>
/// Folder picker implementation.
/// </summary>
public class FolderPicker : IFolderPicker
{
    /// <inheritdoc/>
    public string PickFolder(string description = null)
    {
        var dialog = new FolderBrowserDialog();
        dialog.Description = description ?? "Select folder."; 

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            throw new TaskCanceledException();
        }

        return dialog.SelectedPath;
    }
}