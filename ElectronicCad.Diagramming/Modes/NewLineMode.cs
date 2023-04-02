using System.Windows.Input;
using ElectronicCad.Diagramming.Extensions;
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
            var firstPoint = position.ToDomainPoint();
            var secondPoint = position.ToDomainPoint();
            tempLine = new Line(firstPoint, secondPoint);
            Diagram.DomainDiagram.AddGeometry(tempLine);
        }
        else
        {
            using var scope = Diagram.DomainDiagram.StartModification();
            tempLine.UpdateControlPoint(Line.SecondPointIndex, (float)position.X, (float)position.Y);
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
            tempLine.UpdateControlPoint(Line.SecondPointIndex, (float)position.X, (float)position.Y);
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