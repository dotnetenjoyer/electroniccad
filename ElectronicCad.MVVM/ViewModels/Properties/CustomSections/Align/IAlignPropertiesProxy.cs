
using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Align;

/// <summary>
/// Properties proxy for align custom section.
/// </summary>
public interface IAlignPropertiesProxy : IPropertiesProxy
{
    /// <summary>
    /// Collection of geometry objects.
    /// </summary>
    IEnumerable<GeometryObject> GeometryObjects { get; }
}
