using System.Linq;
using System.Windows.Input;

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
        var nodes = Diagram.AllNodes.Where(x => x.CheckHit(position));

        if (nodes.Any())
        {
            Diagram.Cursor = Cursors.Hand;
        }
        else
        {
            Diagram.Cursor = Cursors.Arrow;
        }
        
    }
}