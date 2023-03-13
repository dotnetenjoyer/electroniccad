using System.Windows;
using System.Windows.Controls;

namespace ElectronicCad.Desktop.UI.Components;

/// <summary>
/// Icon control.
/// </summary>
public class Icon : Control
{
    /// <summary>
    /// Icon path.
    /// </summary>
    public string Path
    {
        get => (string)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }
    
    /// <summary>
    /// Path dependency property.
    /// </summary>
    public static DependencyProperty PathProperty = DependencyProperty.Register(
        nameof(Path),
        typeof(string), 
        typeof(Icon),
        new PropertyMetadata());
}