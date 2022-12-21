using System;
using NullSoft.Diagramming.Nodes;

namespace NullSoft.Diagramming;

/// <summary>
/// Diagram layer.
/// </summary>
public class Layer : IDisposable
{
    /// <summary>
    /// Layer index.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Layer diagram nodes.
    /// </summary>
    public DiagramNodes Nodes { get; }

    /// <summary>
    /// Node event changes, raise when nodes changed.
    /// </summary>
    public event EventHandler NodesChange;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public Layer(int index)
    {
        Nodes = new();
        Index = index;
        Nodes.NodesChanged += HandleNodesChange;
    }

    private void HandleNodesChange(object? sender, EventArgs e)
    {
        NodesChange?.Invoke(this, e);
    }

    /// <summary>
    /// Add new node.
    /// </summary>
    /// <param name="node"></param>
    public void AddNode(DiagramNode node)
    {
        node.Layer = this;
        Nodes.Add(node);
    }

    /// <summary>
    /// Remove node.
    /// </summary>
    /// <param name="node"></param>
    public void RemoveNode(DiagramNode node)
    {
        node.Layer = null;
        Nodes.Remove(node);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Nodes.NodesChanged -= HandleNodesChange;
    }
}