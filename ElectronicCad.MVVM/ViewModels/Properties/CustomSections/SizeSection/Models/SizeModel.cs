using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.SizeSection.Models;

/// <summary>
/// Size model.
/// </summary>
public class SizeModel : ObservableObject
{
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

    /// <summary>
    /// Constructor.
    /// </summary>
    public SizeModel()
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="width">Width.</param>
    /// <param name="height">Heihgt.</param>
    public SizeModel(double width, double height)
    {
        Width = width;
        Height = height;
    }
}
