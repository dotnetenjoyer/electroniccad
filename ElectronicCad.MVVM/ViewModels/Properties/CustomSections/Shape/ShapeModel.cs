using Microsoft.Toolkit.Mvvm.ComponentModel;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Shape;

/// <summary>
/// The model that contains properties describing the geometric shape of an object.
/// </summary>
public class ShapeModel : ObservableObject
{
    /// <summary>
    /// Geometry object fill color.
    /// </summary>
    public Color FillColor
    {
        get => fillColor;
        set => SetProperty(ref fillColor, value);
    }

    private Color fillColor;

    /// <summary>
    /// Geometry object storke color.
    /// </summary>
    public Color StrokeColor 
    { 
        get => strokeColor; 
        set => SetProperty(ref strokeColor, value);
    }

    private Color strokeColor;

    /// <summary>
    /// Determines the geometry object stoke thickness.
    /// </summary>
    public double StrokeWidth
    {
        get => strokeWidth;
        set => SetProperty(ref strokeWidth, value);
    }

    private double strokeWidth;
}
