using System.Windows;
using System.Windows.Media;
using SkiaSharp;
using SkiaSharp.Views.WPF;

namespace ElectronicCad.Diagramming.Utils;

/// <summary>
/// Skia colors provider.
/// </summary>
internal static class Colors
{
    /// <summary>
    /// Primary color.
    /// </summary>
    public static SKColor Primary { get; private set; }

    /// <summary>
    /// Secondary background color.
    /// </summary>
    public static SKColor SecondaryBackground { get; private set; }
    
    /// <summary>
    /// Primary foreground color.
    /// </summary>
    public static SKColor Foreground { get; private set; }
    
    /// <summary>
    /// Initialize.
    /// </summary>
    public static void Initialize(FrameworkElement element)
    {
        var primary = (Color)element.FindResource("Primary");
        Primary = primary.ToSKColor();
        
        var secondaryBackground = (Color)element.FindResource("SecondaryBackground");
        SecondaryBackground = secondaryBackground.ToSKColor();

        var foreground = (Color)element.FindResource("PrimaryForeground");
        Foreground = foreground.ToSKColor();
    }
}