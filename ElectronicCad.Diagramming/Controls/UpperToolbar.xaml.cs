using ElectronicCad.Diagramming.Modes;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicCad.Diagramming.Controls;

internal partial class UpperToolbar : UserControl
{
    /// <summary>
    /// Current diagram mode.
    /// </summary>
    public DiagramMode DiagramMode
    {
        get => (DiagramMode)GetValue(DiagramModeProperty);
        set => SetValue(DiagramModeProperty, value);
    }

    private static readonly DependencyProperty DiagramModeProperty = DependencyProperty
        .Register(nameof(DiagramMode),
            typeof(DiagramMode),
            typeof(UpperToolbar),
            new PropertyMetadata(HandelDiagramModeChanged));

    private static void HandelDiagramModeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs eventArgs)
    {
        if(obj is UpperToolbar toolbar)
        {
            toolbar.DiagramModeChanged?.Invoke(toolbar, (DiagramMode)eventArgs.NewValue);
        }
    }

    /// <summary>
    /// Command to change diagram mode.
    /// </summary>
    public RelayCommand<DiagramMode> ChangeModeCommand
    {
        get => (RelayCommand<DiagramMode>)GetValue(ChangeModeCommandProperty);
        set => SetValue(ChangeModeCommandProperty, value);
    }

    private static readonly DependencyProperty ChangeModeCommandProperty = DependencyProperty
        .Register(nameof(ChangeModeCommand),
            typeof(RelayCommand<DiagramMode>),
            typeof(UpperToolbar),
            new PropertyMetadata());

    /// <summary>
    /// Raised when diagram mode changed.
    /// </summary>
    internal event EventHandler<DiagramMode>? DiagramModeChanged;

    /// <summary>
    /// Constructor.
    /// </summary>
    public UpperToolbar()
    {
        InitializeComponent();

        ChangeModeCommand = new RelayCommand<DiagramMode>(ChangeMode);
        ChangeMode(DiagramMode.Selection);
    }

    private void ChangeMode(DiagramMode mode)
    {
        DiagramMode = mode;
    }
}