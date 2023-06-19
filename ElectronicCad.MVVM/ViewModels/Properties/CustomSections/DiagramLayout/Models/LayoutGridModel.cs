using ElectronicCad.Domain.Geometry.Layouts;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout.Models;

/// <summary>
/// Layout grid view model
/// </summary>
public class LayoutGridModel : LayoutModel
{
    /// <inheritdoc cref="LayoutGrid.Size"/>
    public double Size 
    { 
        get => size; 
        set => SetProperty(ref size, value); 
    }

    private double size;
}