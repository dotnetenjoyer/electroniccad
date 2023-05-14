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
    public double CenterX 
    { 
        get => centerX;
        set => SetProperty(ref centerX, value);
    }

    private double centerX;

    /// <summary>
    /// Position of center y.
    /// </summary>
    public double CenterY 
    { 
        get => centerY; 
        set => SetProperty(ref centerY, value);
    }

    private double centerY;

    /// <summary>
    /// Width.
    /// </summary>
    public double Width 
    { 
        get => width; 
        set => SetProperty(ref width, value); 
    }

    private double width;

    /// <summary>
    /// Height.
    /// </summary>
    public double Height 
    { 
        get => height;
        set => SetProperty(ref height, value);
    }

    private double height;
}