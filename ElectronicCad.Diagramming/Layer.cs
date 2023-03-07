using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using ElectronicCad.Diagramming.Nodes;

namespace ElectronicCad.Diagramming;

/// <summary>
/// Diagram layer.
/// </summary>
public class Layer : IDisposable
{
    /// <summary>
    /// Layer index.
    /// </summary>
    public int Index { get; }
    
    /// <summary>
    /// Raise when items changed.
    /// </summary>
    public event EventHandler ItemsChanged;

    /// <summary>
    /// Diagram items.
    /// </summary>
    public IEnumerable<DiagramItem> DiagramItems => _diagramItems;
    
    private readonly ObservableCollection<DiagramItem> _diagramItems;

    /// <summary>
    /// Constructor.
    /// </summary>
    public Layer(int index)
    {
        Index = index;
        _diagramItems = new();
        _diagramItems.CollectionChanged += HandleItemsChanged;
    }

    private void HandleItemsChanged(object? sender, EventArgs e)
    {
        ItemsChanged?.Invoke(this, e);
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

    /// <inheritdoc/>
    public void Dispose()
    {
        _diagramItems.CollectionChanged -= HandleItemsChanged;
    }
}