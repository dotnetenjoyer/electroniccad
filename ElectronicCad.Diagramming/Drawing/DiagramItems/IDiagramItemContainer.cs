using System.Collections.Generic;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Container for diagram elements.
/// </summary>
internal interface IDiagramItemContainer
{
    /// <summary>
    /// Children diagram items.
    /// </summary>
    IEnumerable<DiagramItem> Children { get; }

    /// <summary>
    /// Adds the diagram items to the children collection.
    /// </summary>
    /// <param name="items">Diagram items to add.</param>
    void AddChildren(IEnumerable<DiagramItem> items);

    /// <summary>
    /// Removes the specified children from the children collection.
    /// </summary>
    /// <param name="children">Children to remove.</param>
    void RemoveChildren(IEnumerable<DiagramItem> children);
}