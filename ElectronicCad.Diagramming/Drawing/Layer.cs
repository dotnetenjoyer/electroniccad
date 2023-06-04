using System;
using System.Collections.Generic;
using System.Linq;
using ElectronicCad.Diagramming.Drawing.Items;
using DomainLayer = ElectronicCad.Domain.Geometry.Layer;

namespace ElectronicCad.Diagramming.Drawing;

/// <summary>
/// Diagram layer.
/// </summary>
internal class Layer : IDiagramItemContainer
{
    /// <summary>
    /// Layer id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Related diagram.
    /// </summary>
    public Diagram Diagram { get; init; }

    /// <summary>
    /// Layer z index.
    /// </summary>
    public int ZIndex { get; }

    /// <summary>
    /// Indicate if the layer is system.
    /// </summary>
    public bool IsSystem => DomainLayer == null;

    /// <summary>
    /// Related domain layer, can be null.
    /// </summary>
    public DomainLayer? DomainLayer { get; private set; }

    /// <inheritdoc />
    public IEnumerable<DiagramItem> Children => children;

    private readonly List<DiagramItem> children = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Related diagram.</param>
    /// <param name="index">Layer index.</param>
    public Layer(Diagram diagram, int index)
    {
        Id = Guid.NewGuid();
        Diagram = diagram;
        ZIndex = index;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Related diagram.</param>
    /// <param name="index">Layer index.</param>
    /// <param name="domainLayer">Domain layer.</param>
    public Layer(Diagram diagram, int index, DomainLayer domainLayer) : this(diagram, index)
    {
        DomainLayer = domainLayer;
    }

    /// <inheritdoc />
    public void AddChildren(IEnumerable<DiagramItem> children)
    {
        foreach (var child in children)
        {
            child.ZIndex = GetMaxUserItemsZIndex() + 1;
            child.Layer = this;
            this.children.Add(child);
        }
    }

    private int GetMaxUserItemsZIndex()
    {
        var userDiagramItems = children
            .Where(x => !x.IsAuxiliary)
            .ToList();

        return userDiagramItems.Any()
            ? userDiagramItems.Max(x => x.ZIndex)
            : 0;
    }

    /// <inheritdoc />
    public void RemoveChildren(IEnumerable<DiagramItem> children)
    {
        foreach (var child in children)
        {
            var isRemoveSuccessed = this.children.Remove(child);
            if (isRemoveSuccessed)
            {
                child.Layer = null;
            }
        }
    }
}
