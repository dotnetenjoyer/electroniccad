using Microsoft.Toolkit.Mvvm.ComponentModel;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Layouts;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout.Models;

/// <summary>
/// Layout view model.
/// </summary>
public abstract class LayoutModel : ObservableObject
{
    /// <inheritdoc cref="Layout.Id">
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <inheritdoc cref="Layout.Color">
    public Color Color
    {
        get => color;
        set => SetProperty(ref color, value);
    }

    private Color color = Layout.DefaultColor;

    /// <inheritdoc cref="Layout.IsVisible">
    public bool IsVisible
    {
        get => isVisible;
        set => SetProperty(ref isVisible, value);
    }

    private bool isVisible = true;
}
    