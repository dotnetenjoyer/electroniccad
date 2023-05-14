using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;
using System.Windows.Input;

namespace ElectronicCad.Diagramming.Modes;

/// <summary>
/// Contains basic behavior for creating a new geometry object, such as a circle or a rectangle.
/// </summary>
/// <typeparam name="TShape">Type of creating geometry object.</typeparam>
internal abstract class ShapeCreationMode<TShape> : BaseDiagramMode where TShape : GeometryObject
{
    /// <summary>
    /// Indicates whether the creation was started.
    /// </summary>
    protected bool IsCreationStart { get; private set; }

    /// <summary>
    /// Temporary creation element.
    /// </summary>
    protected TShape? TemporaryElement { get; private set; }

    /// <inheritdoc />
    protected override void ProcessPrimaryButtonDown(MouseButtonEventArgs args)
    {
        var position = Diagram.GetPosition(args).ToDomainPoint();

        if (!IsCreationStart)
        {
            StartCreation(position);
        }
        else
        {
            CompleteCreation();
        }
    }

    /// <summary>
    /// Creates a temporary element.
    /// </summary>
    /// <returns>Temporary element.</returns>
    protected abstract TShape CreateTemporaryElement(Point centerPoint);

    /// <summary>
    /// Creates a actual element to be added to the diagram.
    /// </summary>
    /// <returns>Actual element.</returns>
    protected abstract TShape CreateActualElement();

    private void StartCreation(Point centerPoint)
    {
        IsCreationStart = true;

        TemporaryElement = CreateTemporaryElement(centerPoint);
        Diagram.DomainDiagram.AddGeometry(TemporaryElement);
    }

    private void CompleteCreation()
    {
        var actualElement = CreateActualElement();

        Diagram.DomainDiagram.RemoveGeometry(TemporaryElement!);
        Diagram.DomainDiagram.AddGeometry(actualElement);

        IsCreationStart = false;
        TemporaryElement = null;
    }

    /// <inheritdoc />
    protected override void Cancel()
    {
        if (IsCreationStart)
        {
            Diagram.DomainDiagram.RemoveGeometry(TemporaryElement!);
            IsCreationStart = false;
        }
    }
}
