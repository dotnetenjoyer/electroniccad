using System.Windows.Input;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Modes;

public class NewLineMode : BaseDiagramMode
{
    private Line? temporaryLine;

    /// <inheritdoc />
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var position = Diagram.GetPosition(args);

        if (temporaryLine == null)
        {
            var firstPoint = position.ToDomainPoint();
            var secondPoint = position.ToDomainPoint();
            temporaryLine = new Line(firstPoint, secondPoint)
            {
                IsTemporary = true
            };

            Diagram.DomainDiagram.AddGeometry(temporaryLine);
        }
        else
        {
            using var scope = Diagram.DomainDiagram.StartModification();
            temporaryLine.UpdateControlPoint(Line.SecondPointIndex, position.X, position.Y);
            temporaryLine.IsTemporary = false;
            temporaryLine = null;
        }
    }

    /// <inheritdoc/>
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        if(temporaryLine == null)
        {
            return;
        }

        var position = Diagram.GetPosition(args);
        using var scope = Diagram.DomainDiagram.StartModification();
        temporaryLine.UpdateControlPoint(Line.SecondPointIndex, position.X, position.Y);
    }

    /// <inheritdoc />
    protected override void Cancel()
    {
        if (temporaryLine != null)
        {
            Diagram.DomainDiagram.RemoveGeometry(temporaryLine);
            temporaryLine = null;
        }
    }
}