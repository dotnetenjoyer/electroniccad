using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;

/// <summary>
/// Set of properties for transformation custom section.
/// </summary>
public interface ITransformationProxy : IProxy
{
    /// <summary>
    /// Center X coordinate.
    /// </summary>
    float X { get; set; }

    /// <summary>
    /// Center Y coordinate.
    /// </summary>
    float Y { get; set; }

    /// <summary>
    /// Width.
    /// </summary>
    float Width { get; set; }

    /// <summary>
    /// Height.
    /// </summary>
    float Height { get; set; }
}