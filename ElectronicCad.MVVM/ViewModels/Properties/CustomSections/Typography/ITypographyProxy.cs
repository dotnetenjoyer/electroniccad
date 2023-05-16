using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Typography;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Typography;

/// <summary>
/// Typography custom section proxy.
/// </summary>
public interface ITypographyProxy : IProxy
{
    /// <summary>
    /// Text content.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Font size.
    /// </summary>
    public double FontSize { get; set; }

    /// <summary>
    /// Font weight.
    /// </summary>
    public FontWeight FontWeight { get; set; }

    /// <summary>
    /// Font family.
    /// </summary>
    public string FontFamily { get; set; }

    /// <summary>
    /// Foreground text color.
    /// </summary>
    public Color ForegroundColor { get; set; }
}
