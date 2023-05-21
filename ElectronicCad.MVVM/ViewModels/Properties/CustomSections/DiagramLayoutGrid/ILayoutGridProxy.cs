using ElectronicCad.Domain.Geometry.LayoutGrids;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;

/// <summary>
/// Describe proxy members to the layout grid custom section.
/// </summary>
public interface ILayoutGridProxy : IProxy
{
    /// <summary>
    /// Collection of the diagram layout grids.
    /// </summary>
    public IEnumerable<LayoutGrid> LayoutGrids { get; set; }

    /// <inheritdoc cref="Domain.Geometry.Diagram.AddLayoutGrid(LayoutGrid)"/>
    void AddLayoutGrid(LayoutGrid layoutGrid);

    /// <inheritdoc cref="Domain.Geometry.Diagram.UpdateLayoutGrid(LayoutGrid)"/>
    void UpdateLayoutGrid(LayoutGrid layoutGrid);

    /// <inheritdoc cref="Domain.Geometry.Diagram.RemoveLayoutGrid(LayoutGrid)"/>
    void RemoveLayoutGrid(LayoutGrid layoutGrid);
}