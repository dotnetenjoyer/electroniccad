using System.Windows;

namespace ElectronicCad.Diagramming.Controls;

internal static class UpperToolbarAssist
{
    /// <summary>
    /// Is tool selected property.
    /// </summary>
    public static readonly DependencyProperty IsToolSelectedProperty =
        DependencyProperty.RegisterAttached(
            "IsToolSelected",
            typeof(bool),
            typeof(UpperToolbarAssist),
            new PropertyMetadata(false)
        );

    /// <summary>
    /// Get property value of is tool selected.
    /// </summary>
    public static bool GetIsToolSelected(DependencyObject element)
    {
        return (bool)element.GetValue(IsToolSelectedProperty);
    }

    /// <summary>
    /// Set property value for is tool selected.
    /// </summary>
    public static void SetIsToolSelected(DependencyObject element, bool value)
    {
        element.SetValue(IsToolSelectedProperty, value);
    }
}