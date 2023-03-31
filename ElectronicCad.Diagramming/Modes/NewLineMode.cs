using System.Windows.Input;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Modes;

public class NewLineMode : BaseDiagramMode
{
    private Line? tempLine;

    /// <inheritdoc />
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var position = args.GetPosition(Diagram);

        if (tempLine == null)
        {
            var firstPoint = new Point(position.X, position.Y);
            var secondPoint = new Point(position.X, position.Y);
            tempLine = new Line(firstPoint, secondPoint);
            Diagram.DomainDiagram.AddGeometry(tempLine);
        }
        else
        {
            using var scope = Diagram.DomainDiagram.StartModification();
            tempLine.UpdateControlPoint(Line.SecondPointIndex, position.X, position.Y);
            tempLine = null;
        }
    }

    /// <inheritdoc/>
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        var position = args.GetPosition(Diagram);

        if (tempLine != null)
        {
            using var scope = Diagram.DomainDiagram.StartModification();
            tempLine.UpdateControlPoint(Line.SecondPointIndex, position.X, position.Y);
        }
    }

    /// <inheritdoc />
    protected override void Cancel()
    {
        if (tempLine != null)
        {
            Diagram.DomainDiagram.RemoveGeometry(tempLine);
            tempLine = null;
        }
    }
}