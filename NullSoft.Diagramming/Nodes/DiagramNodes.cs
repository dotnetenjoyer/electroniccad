using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace NullSoft.Diagramming.Nodes;

/// <summary>
/// Diagram nodes collection.
/// </summary>
public class DiagramNodes : IList<DiagramNode>, INotifyCollectionChanged
{
    private readonly List<DiagramNode> _diagramNodes = new();
    
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
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
    }

    /// <inheritdoc/>
    public void Clear()
    {
        _diagramNodes.Clear();
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
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
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
    }

    /// <inheritdoc/>
    public bool Remove(DiagramNode item)
    {
        return _diagramNodes.Remove(item);
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
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        _diagramNodes.RemoveAt(index);
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
    }

    /// <inheritdoc/>
    public DiagramNode this[int index]
    {
        get => _diagramNodes[index];
        set
        {
            _diagramNodes[index] = value;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
        }
}

    /// <inheritdoc/>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
}