using System.Drawing;
using System.Linq;
using System.Windows.Input;
using ElectronicCad.Diagramming.Nodes;
using SkiaSharp;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// Selection diagram mode.
/// </summary>
public class SelectionMode : BaseDiagramMode
{
    /// <inheritdoc/>
    public override Cursor Cursor => Cursors.Arrow;

    /// <inheritdoc/>
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        if (GetHitItem(args, out var hitItem))
        {
            Diagram.Cursor = Cursors.Hand;
        }
        else
        {
            Diagram.Cursor = Cursors.Arrow;
        }
    }

    /// <inheritdoc/>
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        base.ProcessPrimaryButtonUp(args);
        
        var selectionFrame = GetSelectionFrame();

        if (GetHitItem(args, out var hitItem))
        {
            selectionFrame.BoundingBox = hitItem!.BoundingBox;
            selectionFrame.IsVisible = true;
        }
        else
        {
            selectionFrame.IsVisible = false;
        }
        
        Diagram.RedrawDiagram();
    }

    /// <inheritdoc/>
    public override void Finalize()
    {
        base.Finalize();
        
        var selectionFrame = GetSelectionFrame();
        selectionFrame.IsVisible = false;
        Diagram.RedrawDiagram();
    }

    private bool GetHitItem(MouseEventArgs args, out DiagramItem? diagramItem)
    {
        var position = args.GetPosition(Diagram);
       
        var boundsHitItems = Diagram.DiagramItems
            .Where(_ => !_.IsAuxiliary)
            .Where(_ => _.CheckBoundsHit(position))
            .ToList();

        diagramItem = boundsHitItems.FirstOrDefault(_ => _.CheckHit(position));
        
        return diagramItem != null;
    }

    private SelectionFrameDiagramItem GetSelectionFrame()
    {
        var selectionFrame = Diagram.DiagramItems
            .OfType<SelectionFrameDiagramItem>()
            .FirstOrDefault();

        if (selectionFrame == null)
        {
            selectionFrame = new SelectionFrameDiagramItem();
            Diagram.AddItem(selectionFrame);
        }

        return selectionFrame;
    }
}