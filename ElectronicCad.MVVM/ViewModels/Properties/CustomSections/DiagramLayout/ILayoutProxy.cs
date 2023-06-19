using ElectronicCad.Domain.Geometry.Layouts;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayout;

/// <summary>
/// Describe proxy members to the layout custom section.
/// </summary>
public interface ILayoutProxy : IPropertiesProxy
{
    /// <summary>
    /// Collection of the diagram layout grids.
    /// </summary>
    public IEnumerable<Layout> Layouts { get; set; }

    /// <inheritdoc cref="Domain.Geometry.Diagram.AddLayout(Layout)"/>
    void AddLayout(Layout layout);

    /// <inheritdoc cref="Domain.Geometry.Diagram.UpdateLayout(Layout)"/>
    void UpdateLayout(Layout layout);

    /// <inheritdoc cref="Domain.Geometry.Diagram.RemoveLayout(Layout)"/>
    void RemoveLayout(Layout layout);
}