using SkiaSharp;
using System.Collections.Generic;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// Group of diagram items.
/// </summary>
internal class GroupDiagramItem : DiagramItem
{
    /// <summary>
    /// Collection of grouped items.
    /// </summary>
    protected readonly ICollection<DiagramItem> GroupedItems = new List<DiagramItem>();

    /// <inheritdoc />
    public override void Draw(SkiaDrawingContext context)
    {
        foreach (DiagramItem item in GroupedItems)
        {
            item.Draw(context);
        }
    }

    /// <inheritdoc />
    public override bool CheckShapeHit(ref SKPoint point)
    {
        foreach (var item in GroupedItems)
        {
            if (item.CheckShapeHit(ref point))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseDown(MouseParameters mouse)
    {
        foreach (var item in GroupedItems)
        {
            if (item.HandleDiagramMouseDown(mouse))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseUp(MouseParameters mouse)
    {
        foreach (var item in GroupedItems)
        {
            if (item.HandleDiagramMouseUp(mouse))
            {
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public override bool HandleDiagramMouseMove(MovingMouseParameters mouse)
    {
        foreach (var item in GroupedItems)
        {
            if (item.HandleDiagramMouseMove(mouse))
            {
                return true;
            }
        }

        return false;
    }
}