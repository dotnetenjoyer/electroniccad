using System.Linq;
using System.Windows.Input;
using ElectronicCad.Diagramming.Nodes;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// Selection diagram mode.
/// </summary>
public class SelectionMode : BaseDiagramMode
{
    /// <inheritdoc/>
    public override Cursor Cursor => Cursors.Arrow;

    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        var position = args.GetPosition(Diagram);
        if (Diagram.DiagramItems.Any(_ => _.CheckHit(position)))
        {
            Diagram.Cursor = Cursors.Hand;
        }
        else
        {
            Diagram.Cursor = Cursors.Arrow;
        }
    }

    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var position = args.GetPosition(Diagram);
        var selectedItem = Diagram.DiagramItems.FirstOrDefault(_ => _.CheckHit(position));

        if (selectedItem == null)
        {
            return;
        }        
        var selectionFrame = new SelectionFrameDiagramItem();
        selectionFrame.Bounds = selectedItem.Bounds; 
        Diagram.AddItem(selectionFrame);

        
        base.ProcessPrimaryButtonUp(args);
    }
}