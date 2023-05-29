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
    public Guid Id { get; }

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
    /// <param name="index">Layer index.</param>
    public Layer(int index)
    {
        Id = Guid.NewGuid();
        ZIndex = index;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="index">Layer index.</param>
    /// <param name="domainLayer">Domain layer.</param>
    public Layer(int index, DomainLayer domainLayer) : this(index)
    {
        DomainLayer = domainLayer;
    }

    /// <summary>
    /// Add diagram item.
    /// </summary>
    /// <param name="diagramItem">Diagram item.</param>
    public void AddChild(DiagramItem diagramItem)
    {
        diagramItem.ZIndex = GetMaxUserItemsZIndex() + 1;
        diagramItem.Parent = this;
        children.Add(diagramItem);
    }

    private int GetMaxUserItemsZIndex()
    {
        var userDiagramItems = children
            .Where(x => !x.IsAuxiliary);

        return userDiagramItems.Any()
            ? userDiagramItems.Max(x => x.ZIndex)
            : 0;
    }

    /// <summary>
    /// Removes the diagram item.
    /// </summary>
    /// <param name="diagramItem">Diagram item to remove.</param>
    public void RemoveChild(DiagramItem diagramItem)
    {
        children.Remove(diagramItem);
    }

    public IEnumerable<DiagramItem> GetFlatChildList()
    {
        foreach(var child in Children)
        {
            foreach (var item in GetAllItems(child))
            {
               yield return item;
            }
        }
        
        IEnumerable<DiagramItem> GetAllItems(DiagramItem item)
        {
            if (item is IDiagramItemContainer container)
            {
                foreach(var child in container.Children)
                {
                    foreach (var nestedItem in GetAllItems(child))
                    {
                        yield return nestedItem;
                    }
                }
            }

            yield return item;
        }
    }
}
