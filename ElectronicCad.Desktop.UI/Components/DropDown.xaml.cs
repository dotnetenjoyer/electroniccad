using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ElectronicCad.Desktop.UI.Components;

/// <summary>
/// Interaction logic for DropDown.xaml
/// </summary>
public partial class DropDown : UserControl
{
    /// <inheritdoc cref="IsOpenProperty"/>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>
    /// Indicates whether drop down is open.
    /// </summary>
    public readonly static DependencyProperty IsOpenProperty =
        DependencyProperty.Register(
            nameof(IsOpen),
            typeof(bool),
            typeof(DropDown),
            new PropertyMetadata());

    /// <inheritdoc cref="PreviewTemplateProperty"/>
    public ControlTemplate PreviewTemplate
    {
        get => (ControlTemplate)GetValue(PreviewTemplateProperty);
        set => SetValue(PreviewTemplateProperty, value);
    } 

    /// <summary>
    /// Contains template for the dropdown preview.
    /// </summary>
    public readonly static DependencyProperty PreviewTemplateProperty =
        DependencyProperty.Register(
            nameof(PreviewTemplate),
            typeof(ControlTemplate),
            typeof(DropDown),
            new PropertyMetadata());

    /// <inheritdoc cref="PlacementProperty"/>
    public PlacementMode Placement
    {
        get => (PlacementMode)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    /// <summary>
    /// Specify drop-down placement position.
    /// </summary>
    public static readonly DependencyProperty PlacementProperty = 
        Popup.PlacementProperty.AddOwner(typeof(DropDown), new PropertyMetadata());

    /// <summary>
    /// Constructor.
    /// </summary>
    public DropDown()
    {
        InitializeComponent();
    }
}
