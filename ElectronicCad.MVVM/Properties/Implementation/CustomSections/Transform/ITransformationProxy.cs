namespace ElectronicCad.MVVM.Properties.Implementation.CustomSections.Transformation;

/// <summary>
/// Set of properties for transformation custom section.
/// </summary>
public interface ITransformationProxy
{
    /// <summary>
    /// Center X coordinate.
    /// </summary>
    float X { get; }

    /// <summary>
    /// Center Y coordinate.
    /// </summary>
    float Y { get; }

    /// <summary>
    /// Width.
    /// </summary>
    float Width { get; }

    /// <summary>
    /// Height.
    /// </summary>
    float Height { get; }
}