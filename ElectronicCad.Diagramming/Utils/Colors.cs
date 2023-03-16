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
    /// Brand color.
    /// </summary>
    public static SKColor Brand { get; private set; }

    /// <summary>
    /// Secondary background color.
    /// </summary>
    public static SKColor SecondaryBackground { get; private set; }
    
    /// <summary>
    /// Initialize.
    /// </summary>
    public static void Initialize(FrameworkElement element)
    {
        var brandColor = (Color)element.FindResource("Brand");
        Brand = brandColor.ToSKColor();
        
        var secondaryBackgroundColor = (Color)element.FindResource("SecondaryBackground");
        SecondaryBackground = secondaryBackgroundColor.ToSKColor();
    }
}