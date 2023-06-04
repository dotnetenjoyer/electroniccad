using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;

/// <summary>
/// Contains set of properties for shape custom section.
/// </summary>
public interface IShapeProxy : IPropertiesProxy
{
    /// <summary>
    /// Geometry object fill color.
    /// </summary>
    public Color FillColor { get; set; }

    /// <summary>
    /// Geometry object storke color.
    /// </summary>
    public Color StrokeColor { get; set; }

    /// <summary>
    /// Determines the geometry object stoke width.
    /// </summary>
    public double StrokeWidth { get; set; }
}