using System.Windows;
using System.Windows.Media;
using SkiaSharp;
using SkiaSharp.Views.WPF;

namespace ElectronicCad.Diagramming.Drawing;

/// <summary>
/// Diagram drawing colors.
/// </summary>
internal class DrawingColors
{
    /// <summary>
    /// Primary foreground color.
    /// </summary>
    public SKColor PrimaryForeground { get; private set; }

    /// <summary>
    /// Workspace background color.
    /// </summary>
    public SKColor WorkspaceBackground { get; private set; }

    /// <summary>
    /// Primary color.
    /// </summary>
    public SKColor Primary { get; private set; }

    /// <summary>
    /// Initialize.
    /// </summary>
    public void Initialize(FrameworkElement element)
    {
        var workspaceBackground = (Color)element.FindResource("DiagramWorkspaceBackground");
        WorkspaceBackground = workspaceBackground.ToSKColor();

        var primaryForeground = (Color)element.FindResource("DiagramPrimaryForeground");
        PrimaryForeground = primaryForeground.ToSKColor();

        var primary = (Color)element.FindResource("Primary");
        Primary = primary.ToSKColor();
    }
}