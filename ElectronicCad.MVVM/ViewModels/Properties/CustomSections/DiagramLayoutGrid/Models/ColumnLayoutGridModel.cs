using ElectronicCad.Domain.Geometry.LayoutGrids;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid.Models;

/// <summary>
/// Column layout grid view model.
/// </summary>
public class ColumnLayoutGridModel : LayoutGridModel
{
    /// <inheritdoc cref="ColumnLayoutGrid.Count"/>
    public int Count 
    { 
        get => count; 
        set => SetProperty(ref count, value);
    }

    private int count;

    /// <inheritdoc cref="ColumnLayoutGrid.Width"/>
    public double Width 
    { 
        get => width; 
        set => SetProperty(ref width, value); 
    }

    private double width;

    /// <inheritdoc cref="ColumnLayoutGrid.Offset"/>
    public double Offset 
    { 
        get => offset; 
        set => SetProperty(ref offset, value);
    }

    private double offset;
}
