using System.Windows.Input;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// The diagram mode to create line.
/// </summary>
public class LineCreationMode : BaseDiagramMode
{
    private bool isCreationStart;
    private Line? temporaryLine;

    /// <inheritdoc />
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var position = Diagram.GetPosition(args).ToDomainPoint();

        if (!isCreationStart)
        {
            StartCreation(position);
        }
        else
        {
            CompleteCreation(position);
        }
    }

    private void StartCreation(Point position)
    {
        isCreationStart = true;

        var points = new[] { position, position };
        temporaryLine = new Line(points, isTemporary: true);
        Diagram.DomainDiagram.AddGeometry(temporaryLine);
    }

    private void CompleteCreation(Point position)
    {
        var points = new[] { temporaryLine!.ControlPoints[Line.FirstPointIndex], position };
        var line = new Line(points);

        Diagram.DomainDiagram.RemoveGeometry(temporaryLine);
        Diagram.DomainDiagram.AddGeometry(line);

        isCreationStart = false;
        temporaryLine = null;
    }


    /// <inheritdoc/>
    protected override void ProcessMouseMove(MouseEventArgs args)
    {
        if (isCreationStart && temporaryLine != null)
        {
            var position = Diagram.GetPosition(args).ToDomainPoint();

            using var scope = temporaryLine.StartDiagramModifcation();
            temporaryLine.StartModification();
            temporaryLine.SetControlPoint(Line.SecondPointIndex, position.X, position.Y);
            temporaryLine.CompleteModification();
        }
    }

    /// <inheritdoc />
    protected override void Cancel()
    {
        if (isCreationStart)
        {
            Diagram.DomainDiagram.RemoveGeometry(temporaryLine!);
            isCreationStart = false;
            temporaryLine = null;
        }
    }
}