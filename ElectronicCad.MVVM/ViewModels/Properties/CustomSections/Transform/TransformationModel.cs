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
    public float X 
    { 
        get => x;
        set => SetProperty(ref x, value);
    }

    private float x;

    /// <summary>
    /// Position of center y.
    /// </summary>
    public float Y 
    { 
        get => y; 
        set => SetProperty(ref y, value);
    }

    private float y;

    /// <summary>
    /// Width.
    /// </summary>
    public float Width 
    { 
        get => width; 
        set => SetProperty(ref width, value); 
    }

    private float width;

    /// <summary>
    /// Height.
    /// </summary>
    public float Height 
    { 
        get => height;
        set => SetProperty(ref height, value);
    }

    private float height;
}