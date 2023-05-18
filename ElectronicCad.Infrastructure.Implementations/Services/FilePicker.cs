using ElectronicCad.Infrastructure.Abstractions.Interfaces;

namespace ElectronicCad.Infrastructure.Implementations.Services;

/// <summary>
/// File picker implementation.
/// </summary>
public class FilePicker : IFilePicker
{
    /// <inheritdoc />
    public string PickFile()
    {
        var dialog = new OpenFileDialog();

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            throw new TaskCanceledException();
        }

        return dialog.FileName;
    }
}