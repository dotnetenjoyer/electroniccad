using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;

/// <summary>
/// Set of properties for transformation custom section.
/// </summary>
public interface ITransformationProxy : IPropertiesProxy
{
    /// <summary>
    /// Center X coordinate.
    /// </summary>
    double CenterX { get; set; }

    /// <summary>
    /// Center Y coordinate.
    /// </summary>
    double CenterY { get; set; }

    /// <summary>
    /// Width.
    /// </summary>
    double Width { get; set; }

    /// <summary>
    /// Height.
    /// </summary>
    double Height { get; set; }
}