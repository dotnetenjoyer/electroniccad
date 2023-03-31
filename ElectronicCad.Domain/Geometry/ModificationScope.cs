namespace ElectronicCad.Domain.Geometry;

public class ModificationScope : IDisposable
{
    /// <summary>
    /// The diagram that releated with scope
    /// </summary>
    internal Diagram Diagram { get; private set; }

    private readonly List<GeometryObject> modifiedGeometry = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Diagram.</param>
    public ModificationScope(Diagram diagram)
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
            Diagram.HandleGeometryModification();
        }
    }
}