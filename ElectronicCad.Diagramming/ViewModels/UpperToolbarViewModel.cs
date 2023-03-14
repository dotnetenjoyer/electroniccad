using System;
using ElectronicCad.Diagramming.Modes;
using ElectronicCad.MVVM.Common;
using Microsoft.Toolkit.Mvvm.Input;

namespace ElectronicCad.Diagramming.ViewModels;

/// <summary>
/// Upper toolbar view model.
/// </summary>
public class UpperToolbarViewModel : ViewModel
{
    /// <summary>
    /// Change mode command.
    /// </summary>
    public RelayCommand<DiagramMode> ChangeModeCommand { get; }

    /// <summary>
    /// Current diagram mode.
    /// </summary>
    public DiagramMode DiagramMode
    {
        get => _diagramMode;
        private set
        {
            _diagramMode = value;
            OnPropertyChanged();
            DiagramModeChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private DiagramMode _diagramMode;

    /// <summary>
    /// Raised when diagram mode changed.
    /// </summary>
    public event EventHandler? DiagramModeChanged;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public UpperToolbarViewModel()
    {
        ChangeModeCommand = new RelayCommand<DiagramMode>(ChangeMode);
        ChangeMode(DiagramMode.Selection);
    }

    private void ChangeMode(DiagramMode mode)
    {
        DiagramMode = mode;
    }
}