using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection;

/// <summary>
/// Proxy for size custom section.
/// </summary>
public interface ISizeProxy : IPropertiesProxy
{
    /// <summary>
    /// Size.
    /// </summary>
    public Size Size { get; set; }
}