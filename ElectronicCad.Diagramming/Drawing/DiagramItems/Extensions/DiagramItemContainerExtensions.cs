using ElectronicCad.Diagramming.Drawing.Items;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.Extensions;

/// <summary>
/// Extension methods for diagram item container.
/// </summary>
internal static class DiagramItemContainerExtensions
{
    /// <summary>
    /// Adds child to the diagram item container.
    /// </summary>
    /// <param name="container">Container.</param>
    /// <param name="child">Item to add.</param>
    public static void AddChild(this IDiagramItemContainer container, DiagramItem child)
    {
        container.AddChildren(new[] { child });
    }

    /// <summary>
    /// Removes child from the diagram item container.
    /// </summary>
    /// <param name="container">Container.</param>
    /// <param name="child">Item to remove.</param>
    public static void RemoveChild(this IDiagramItemContainer container, DiagramItem child)
    {
        container.RemoveChildren(new[] { child });
    }

    /// <summary>
    /// Returns flat collection of all child items.
    /// </summary>
    /// <param name="container">Diagram item container.</param>
    /// <returns>All items.</returns>
    public static IEnumerable<DiagramItem> GetFlatChildList(this IDiagramItemContainer container)
    {
        foreach(var child in container.Children)
        {
            foreach (var item in GetAllItems(child))
            {
                yield return item;
            }
        }

        IEnumerable<DiagramItem> GetAllItems(DiagramItem diagramItem)
        {
            if (diagramItem is IDiagramItemContainer container)
            {
                foreach (var child in container.Children)
                {
                    foreach(var item in GetAllItems(child))
                    {
                        yield return item;
                    }
                }
            }
        
            yield return diagramItem;
        }
    }
}