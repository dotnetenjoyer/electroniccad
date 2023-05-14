namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Diagram modification scope
/// </summary>
public class DiagramModificationScope : IDisposable
{
    /// <summary>
    /// The diagram that releated with scope
    /// </summary>
    internal Diagram Diagram { get; private set; }

    private readonly HashSet<GeometryObject> modifiedGeometry = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Diagram.</param>
    public DiagramModificationScope(Diagram diagram)
    {
        Diagram = diagram;
    }

    /// <summary>
    /// Marks a geometry object as modified.
    /// </summary>
    /// <param name="geometryObject">Modified geometry object.</param>
    internal void AddModifiedItem(GeometryObject geometryObject)
    {
        modifiedGeometry.Add(geometryObject);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (modifiedGeometry.Any())
        {
            CompleteObjectsModification();
            Diagram.HandleGeometryModification(modifiedGeometry);
        }
    }

    private void CompleteObjectsModification()
    {
        foreach (GeometryObject geometryObject in modifiedGeometry)
        {
            if (geometryObject.IsModificationStarted)
            {
                geometryObject.CompleteModification();
            }
        }
    }
}