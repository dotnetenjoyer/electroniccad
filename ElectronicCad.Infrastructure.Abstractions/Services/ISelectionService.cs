namespace ElectronicCad.Infrastructure.Abstractions.Services;

/// <summary>
/// Selection service.
/// </summary>
public interface ISelectionService
{
    /// <summary>
    /// Raise when selected items changed.
    /// </summary>
    event EventHandler SelectionChanged;

    /// <summary>
    /// Current selected items.
    /// </summary>
    IReadOnlyCollection<object> SelectedObjects { get; }

    /// <summary>
    /// Select new items.
    /// </summary>
    /// <param name="selectedObject">Objects to select.</param>
    void Select(object[] selectedObject);

    /// <summary>
    /// Clear selected objects.
    /// </summary>
    void ClearSelection();
}