namespace ElectronicCad.Domain.Geometry;

public class ModificationScope : IDisposable
{
    public Diagram Diagram { get; private set; }

    public ModificationScope(Diagram diagram)
    {
        Diagram = diagram;
    }

    public void Dispose()
    {
        Diagram.IncrementVersion();
    }
}