namespace ElectronicCad.Domain.Geometry;

public class ModificationScope : IDisposable
{
    public Diagram Diagram { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="diagram">Diagram.</param>
    public ModificationScope(Diagram diagram)
    {
        Diagram = diagram;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Diagram.IncrementVersion();
    }
}