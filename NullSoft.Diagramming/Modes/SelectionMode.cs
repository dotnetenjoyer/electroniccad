using System.Windows.Input;

namespace NullSoft.Diagramming.Modes;

/// <summary>
/// Selection diagram mode.
/// </summary>
public class SelectionMode : BaseDiagramMode
{
    /// <inheritdoc/>
    public override Cursor Cursor => Cursors.Arrow;
}