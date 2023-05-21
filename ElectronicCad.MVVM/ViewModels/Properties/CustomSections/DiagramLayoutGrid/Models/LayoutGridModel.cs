using Microsoft.Toolkit.Mvvm.ComponentModel;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.LayoutGrids;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid.Models;

/// <summary>
/// Layout grid view model.
/// </summary>
public abstract class LayoutGridModel : ObservableObject
{
    /// <inheritdoc cref="LayoutGrid.Id">
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <inheritdoc cref="LayoutGrid.Color">
    public Color Color
    {
        get => color;
        set => SetProperty(ref color, value);
    }

    private Color color = LayoutGrid.DefaultColor;

    /// <inheritdoc cref="LayoutGrid.IsVisible">
    public bool IsVisible
    {
        get => isVisible;
        set => SetProperty(ref isVisible, value);
    }

    private bool isVisible = true;
}
    