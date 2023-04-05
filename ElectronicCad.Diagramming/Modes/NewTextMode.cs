using ElectronicCad.Domain.Geometry;
using System.Windows.Input;

namespace ElectronicCad.Diagramming.Modes;

internal class NewTextMode : BaseDiagramMode
{
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var position = Diagram.GetPosition(args);

        var topLeft = new Point(position.X - 50, position.Y - 20);
        var topRigth = new Point(position.X + 50, position.Y - 20);

        var bottomRigth = new Point(position.X + 50, position.Y + 20);
        var bottomLeft = new Point(position.X - 50, position.Y + 20);

        var text = new Text(topLeft, topRigth, bottomRigth, bottomLeft);
        Diagram.DomainDiagram.AddGeometry(text);
    }
}