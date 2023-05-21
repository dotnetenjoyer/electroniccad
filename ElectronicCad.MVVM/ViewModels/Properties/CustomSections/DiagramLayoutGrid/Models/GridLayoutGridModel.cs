using ElectronicCad.Domain.Geometry.LayoutGrids;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid.Models;

/// <summary>
/// Grid lyaout view model
/// </summary>
public class GridLayoutGridModel : LayoutGridModel
{
    /// <inheritdoc cref="GridLayoutGrid.Size"/>
    public double Size 
    { 
        get => size; 
        set => SetProperty(ref size, value); 
    }

    private double size;
}