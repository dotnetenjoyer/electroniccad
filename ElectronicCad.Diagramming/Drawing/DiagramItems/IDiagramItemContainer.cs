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
    /// Adds the diagram item to the children collection.
    /// </summary>
    /// <param name="item">Diagram item to add.</param>
    void AddChild(DiagramItem item);

    /// <summary>
    /// Removes the diagram item from the children collection.
    /// </summary>
    /// <param name="item">Diagram item to remove.</param>
    void RemoveChild(DiagramItem item);
}