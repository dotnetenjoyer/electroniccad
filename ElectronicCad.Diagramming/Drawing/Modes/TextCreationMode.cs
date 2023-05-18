using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;
using System.Windows.Input;

namespace ElectronicCad.Diagramming.Drawing.Modes;

/// <summary>
/// The diagram mode to create text.
/// </summary>
internal class TextCreationMode : BaseDiagramMode
{
    /// <summary>
    /// Initial text width.
    /// </summary>
    internal const double InitialWidth = 100;

    /// <summary>
    /// Initial text height.
    /// </summary>
    internal const double InitialHeight = 25;

    /// <inheritdoc />
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var position = Diagram.GetPosition(args).ToDomainPoint();
        var text = new Text(position, InitialWidth, InitialHeight);
        Diagram.GeometryDiagram.AddGeometry(text);
    }
}