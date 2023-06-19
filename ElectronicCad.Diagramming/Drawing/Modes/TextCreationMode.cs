using System.Windows.Input;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Extensions;
using ElectronicCad.Diagramming.Extensions;

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
    internal const double InitialHeight = 30;

    /// <inheritdoc />
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var position = Diagram.CalculateDiagramPosition(args).ToDomainPoint();
        var text = new Text(position, InitialWidth, InitialHeight);
        Diagram.GeometryDiagram.AddGeometry(text);
    }
}