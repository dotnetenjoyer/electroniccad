using System;
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
    public event EventHandler? DiagramModeChanged;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public DiagramControlViewModel()
    {
        UpperToolbar = new();
        UpperToolbar.DiagramModeChanged += HandleDiagramModeChanged;
    }

    private void HandleDiagramModeChanged(object? sender, EventArgs args)
    {
        DiagramModeChanged?.Invoke(sender, args);
    }
}