using ElectronicCad.Domain.Geometry.Layouts;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout.Models;

/// <summary>
/// Layout row view model.
/// </summary>
public class LayoutRowModel : LayoutModel
{
    /// <inheritdoc cref="LayoutRow.Count"/>
    public int Count 
    { 
        get => count; 
        set => SetProperty(ref count, value); 
    }
    
    private int count;

    /// <inheritdoc cref="LayoutRow.Height"/>
    public double Height 
    {
        get => height; 
        set => SetProperty(ref height, value); 
    }

    private double height;

    /// <inheritdoc cref="LayoutRow.Offset"/>
    public double Offset 
    { 
        get => offset; 
        set => SetProperty(ref offset, value);
    }

    private double offset;
    
    /// <inheritdoc cref="LayoutRow.Gutter"/>
    public double Gutter
    {
        get => gutter;
        set => SetProperty(ref gutter, value);
    }

    private double gutter;
}
