using ElectronicCad.Domain.Geometry.Layouts;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout.Models;

/// <summary>
/// Layout column view model.
/// </summary>
public class LayoutColumnModel : LayoutModel
{
    /// <inheritdoc cref="LayoutColumn.Count"/>
    public int Count 
    { 
        get => count; 
        set => SetProperty(ref count, value);
    }

    private int count;

    /// <inheritdoc cref="LayoutColumn.Width"/>
    public double Width 
    { 
        get => width; 
        set => SetProperty(ref width, value); 
    }

    private double width;

    /// <inheritdoc cref="LayoutColumn.Offset"/>
    public double Offset 
    { 
        get => offset; 
        set => SetProperty(ref offset, value);
    }

    private double offset;

    /// <inheritdoc cref="LayoutColumn.Gutter"/>
    public double Gutter
    {
        get => gutter;
        set => SetProperty(ref gutter, value);
    }

    private double gutter;
}
