using ElectronicCad.Diagramming.Drawing.Modes;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicCad.Diagramming.Controls;

internal partial class UpperToolbar : UserControl
{
    /// <summary>
    /// Raised when diagram mode changed.
    /// </summary>
    public event EventHandler<DiagramMode>? DiagramModeChanged;

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
        var toolbar = obj as UpperToolbar;
        toolbar!.DiagramModeChanged?.Invoke(toolbar, (DiagramMode)eventArgs.NewValue);
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
    /// Command to add new image.
    /// </summary>
    public RelayCommand AddNewImageCommand
    {
        get => (RelayCommand)GetValue(AddImageCommandProperty);
        set => SetValue(AddImageCommandProperty, value);
    }

    private static readonly DependencyProperty AddImageCommandProperty = DependencyProperty
        .Register(nameof(AddNewImageCommand),
            typeof(RelayCommand),
            typeof(UpperToolbar),
            new PropertyMetadata());

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