using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicCad.Diagramming.Drawing.Items;

namespace ElectronicCad.Diagramming.Drawing;

/// <summary>
/// Diagram layer.
/// </summary>
internal class Layer
{
    /// <summary>
    /// Layer id.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Layer index.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Diagram items.
    /// </summary>
    public IEnumerable<DiagramItem> DiagramItems => _diagramItems;

    private readonly List<DiagramItem> _diagramItems = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public Layer(int index)
    {
        Id = Guid.NewGuid();
        Index = index;
    }

    /// <summary>
    /// Add diagram item.
    /// </summary>
    /// <param name="item">Diagram item.</param>
    public void AddItem(DiagramItem item)
    {
        item.Layer = this;
        item.ZIndex = _diagramItems.Any() ? _diagramItems.Max(x => x.ZIndex) + 1 : 0;
        _diagramItems.Add(item);
    }

    /// <summary>
    /// Remove diagram item.
    /// </summary>
    /// <param name="item">Diagram item to remove.</param>
    public void RemoveItem(DiagramItem item)
    {
        item.Layer = null;
        _diagramItems.Remove(item);
    }
}