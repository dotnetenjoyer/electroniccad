using ElectronicCad.Domain.Geometry.LayoutGrids;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid.Models;

/// <summary>
/// Row layout grid view model.
/// </summary>
public class RowLayoutGridModel : LayoutGridModel
{
    /// <inheritdoc cref="RowLayoutGrid.Count"/>
    public int Count 
    { 
        get => count; 
        set => SetProperty(ref count, value); 
    }
    
    private int count;

    /// <inheritdoc cref="RowLayoutGrid.Height"/>
    public double Height 
    {
        get => height; 
        set => SetProperty(ref height, value); 
    }

    private double height;

    /// <inheritdoc cref="RowLayoutGrid.Offset"/>
    public double Offset 
    { 
        get => offset; 
        set => SetProperty(ref offset, value);
    }

    private double offset;
}
