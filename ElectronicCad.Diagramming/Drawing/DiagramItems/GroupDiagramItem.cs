using System.Linq;
using System.Collections.Generic;
using SkiaSharp;
using ElectronicCad.Diagramming.Utils;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Group of the diagram items.
/// </summary>
internal class GroupDiagramItem : DiagramItem, IDiagramItemContainer
{
    /// <inheritdoc />
    public IEnumerable<DiagramItem> Children => children;

    private readonly List<DiagramItem> children = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public GroupDiagramItem()
    {
        BoundingBox = SKRect.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="children">Children.</param>
    public GroupDiagramItem(IEnumerable<DiagramItem> children)
    {
        this.children.AddRange(children);
        RecalculateBoundingBox();
    }

    /// <summary>
    /// Recalculates the group bounding box based on the children bounds.
    /// </summary>
    public virtual void RecalculateBoundingBox()
    {
        BoundingBox = SkiaRectangleUtils
            .CalculateScribedRectangle(Children.Select(x => x.BoundingBox));
    }

    /// <summary>
    /// Adds the diagram item to the group. 
    /// </summary>
    /// <param name="diagramItem">Diagram item to add.</param>
    public void AddChild(DiagramItem diagramItem)
    {
        children.Add(diagramItem);
        RecalculateBoundingBox();
    }

    /// <summary>
    /// Adds the diagram items to the group.
    /// </summary>
    /// <param name="items">DIagram items to add.</param>
    public void AddChild(IEnumerable<DiagramItem> items)
    {
        children.AddRange(items);
        RecalculateBoundingBox();
    }

    /// <summary>
    /// Removes the diagram item from the group.
    /// </summary>
    /// <param name="diagramItem">Diagram item to remove.</param>
    public void RemoveChild(DiagramItem diagramItem)
    {
        children.Remove(diagramItem);
        RecalculateBoundingBox();
    }

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext context)
    {
        foreach (var child in Children)
        {
            child.Draw(context);
        }
    }

    /// <inheritdoc />
    public override bool CheckShapeHit(ref SKPoint point)
    {
        foreach (var child in Children)
        {
            if (child.CheckShapeHit(ref point))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseDown(MouseParameters mouse)
    {
        foreach (var child in Children)
        {
            if (child.HandleDiagramMouseDown(mouse))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseUp(MouseParameters mouse)
    {
        foreach (var child in Children)
        {
            if (child.HandleDiagramMouseUp(mouse))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseMove(MovingMouseParameters mouse)
    {
        foreach (var child in Children)
        {
            if (child.HandleDiagramMouseMove(mouse))
            {
                return true;
            }
        }

        return false;
    }
}
