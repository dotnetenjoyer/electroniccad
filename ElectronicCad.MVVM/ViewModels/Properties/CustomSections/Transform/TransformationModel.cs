using ElectronicCad.MVVM.Utils;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Transformation;

/// <summary>
/// Contains transformation properties.
/// </summary>
public class TransformationModel : EditableModel
{
    /// <summary>
    /// Position of center x.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Position of center y.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// Width.
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    /// Height.
    /// </summary>
    public float Height { get; set; }
}