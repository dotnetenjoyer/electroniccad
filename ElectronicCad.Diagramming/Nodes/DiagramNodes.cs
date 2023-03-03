using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ElectronicCad.Diagramming.Nodes;

/// <summary>
/// Diagram nodes collection.
/// </summary>
public class DiagramNodes : IList<DiagramNode>
{
    private readonly List<DiagramNode> _diagramNodes = new();

    /// <summary>
    /// Diagram nodes changed event.
    /// </summary>
    public event EventHandler NodesChanged;
    
    /// <inheritdoc/>
    public IEnumerator<DiagramNode> GetEnumerator()
    {
        return _diagramNodes.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc/>
    public void Add(DiagramNode item)
    {
        _diagramNodes.Add(item);
        NodesChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    public void Clear()
    {
        _diagramNodes.Clear();
        NodesChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    public bool Contains(DiagramNode item)
    {
        return _diagramNodes.Contains(item);
    }

    /// <inheritdoc/>
    public void CopyTo(DiagramNode[] array, int arrayIndex)
    {
        _diagramNodes.CopyTo(array, arrayIndex);
        NodesChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    public bool Remove(DiagramNode item)
    {
        var result = _diagramNodes.Remove(item);

        if (result)
        {
            NodesChanged?.Invoke(this, EventArgs.Empty);
        }

        return result;
    }

    /// <inheritdoc/>
    public int Count => _diagramNodes.Count;
    
    /// <inheritdoc/>
    public bool IsReadOnly => false;
    
    /// <inheritdoc/>
    public int IndexOf(DiagramNode item)
    {
        return _diagramNodes.IndexOf(item);
    }

    /// <inheritdoc/>
    public void Insert(int index, DiagramNode item)
    {
        _diagramNodes.Insert(index, item);
        NodesChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        _diagramNodes.RemoveAt(index);
        NodesChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    public DiagramNode this[int index]
    {
        get => _diagramNodes[index];
        set
        {
            _diagramNodes[index] = value;
            NodesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}