using ElectronicCad.Infrastructure.Abstractions.Services;

namespace ElectronicCad.Infrastructure.Implementations.Services;

/// <summary>
/// Selection service implementation.
/// </summary>
public class SelectionService : ISelectionService
{
    /// <inheritdoc />
    public IReadOnlyCollection<object>? SelectedObjects { get; private set; }
    
    /// <inheritdoc />
    public event EventHandler SelectionChanged;
    
    /// <inheritdoc />
    public void Select(IEnumerable<object> selectedObject)
    {
        SelectedObjects = selectedObject.ToList();
        SelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    public void ClearSelection()
    {
        SelectedObjects = null;
        SelectionChanged?.Invoke(this, EventArgs.Empty);
    }
}