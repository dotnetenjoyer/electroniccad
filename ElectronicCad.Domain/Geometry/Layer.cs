using ElectronicCad.Domain.Common;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Diagram layer.
/// </summary>
public class Layer : DomainObservableObject, IGeometryContainer, IHaveName
{
    /// <summary>
    /// Layer id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Layer name.
    /// </summary>
    public string Name 
    { 
        get => name; 
        internal set => SetProperty(ref name, value); 
    }

    private string name;
    
    /// <summary>
    /// Is locked.
    /// </summary>
    public bool IsLocked 
    { 
        get => isLocked; 
        set => SetProperty(ref isLocked, value);
    }

    private bool isLocked;

    /// <summary>
    /// Is visible.
    /// </summary>
    public bool IsVisible 
    {
        get => isVisible; 
        set => SetProperty(ref isVisible, value); 
    }

    private bool isVisible = true;

    /// <summary>
    /// Layer z-index.
    /// </summary>
    public int ZIndex 
    { 
        get => zIndex; 
        internal set => SetProperty(ref zIndex, value); 
    }

    private int zIndex;

    /// <summary>
    /// Is the layer is active.
    /// </summary>
    public bool IsActive => Diagram.ActiveLayer == this;

    /// <summary>
    /// Related diagram.
    /// </summary>
    public Diagram Diagram { get; init; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">Layer name.</param>
    /// <param name="name">Related diagram.</param>
    public Layer(string name, Diagram diagram)
    {
        Id = Guid.NewGuid();
        Name = name;
        Diagram = diagram;
    }

    #region IGeometryContainer

    /// <inheritdoc />
    public IEnumerable<GeometryObject> Children => children;

    private readonly List<GeometryObject> children = new();

    /// <inheritdoc />
    public void AddGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        foreach (var geometryObject in geometryObjects)
        {
            geometryObject.Layer = this;
            children.Add(geometryObject);
        }

        Diagram.RaiseGeometryAdded(geometryObjects);
    }

    /// <inheritdoc />
    public void RemoveGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        foreach (var geometryObject in geometryObjects)
        {
            geometryObject.Layer = null;
            children.Remove(geometryObject);
        }

        Diagram.RaiseGeometryRemoved(geometryObjects);
    }

    #endregion
}