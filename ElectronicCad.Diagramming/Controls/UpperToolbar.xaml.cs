using ElectronicCad.Diagramming.Drawing.Modes;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicCad.Diagramming.Controls;

internal partial class UpperToolbar : UserControl
{
    /// <inheritdoc />
    public DiagramMode DiagramMode
    {
        get => (DiagramMode)GetValue(DiagramModeProperty);
        set => SetValue(DiagramModeProperty, value);
    }

    /// <summary>
    /// Indicates which diagram mode is currently active for related diagram.
    /// </summary>
    public static readonly DependencyProperty DiagramModeProperty = Diagram.DiagramModeProperty
        .AddOwner(typeof(UpperToolbar));

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
    }

    private void ChangeMode(DiagramMode mode)
    {
        DiagramMode = mode;
    }
}