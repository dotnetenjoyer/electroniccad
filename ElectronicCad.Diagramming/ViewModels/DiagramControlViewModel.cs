using System;
using ElectronicCad.Diagramming.Modes;
using ElectronicCad.MVVM.Common;

namespace ElectronicCad.Diagramming.ViewModels;

/// <summary>
/// Diagram control view model.
/// </summary>
public class DiagramControlViewModel : ViewModel
{
    /// <summary>
    /// Upper toolbar view model.
    /// </summary>
    public UpperToolbarViewModel UpperToolbar { get; }

    /// <summary>
    /// Raised when diagram mode changed.
    /// </summary>
    public event EventHandler<DiagramMode>? DiagramModeChanged;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public DiagramControlViewModel()
    {
        UpperToolbar = new();
        UpperToolbar.DiagramModeChanged += HandleDiagramModeChange;
    }

    private void HandleDiagramModeChange(object? sender, DiagramMode newValue)
    {
        DiagramModeChanged?.Invoke(sender, newValue);
    }
    
    /// <inheritdoc />
    protected override void Dispose(bool isDisposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (isDisposing)
        {
            UpperToolbar.DiagramModeChanged -= HandleDiagramModeChange;
        }
        
        base.Dispose(isDisposing);
    }
}