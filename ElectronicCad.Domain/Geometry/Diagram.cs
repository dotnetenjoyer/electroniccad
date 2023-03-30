using System.Linq.Expressions;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Diagram.
/// </summary>
public class Diagram : IDisposable
{
    /// <summary>
    /// Diagram id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Diagram width.
    /// </summary>
    public double Width { get; set; } = 1400;

    /// <summary>
    /// Diagram height.
    /// </summary>
    public double Height { get; set; } = 1000;

    /// <summary>
    /// The event fires on geometry added within diagram.
    /// </summary>
    public event EventHandler<GeometryObject> GeometryAdded;

    /// <summary>
    /// The event fires on geometry removed  withing diagram.
    /// </summary>
    public event EventHandler<GeometryObject> GeometryRemoved;

    /// <summary>
    /// Diagram active layer.
    /// </summary>
    public Layer ActiveLayer { get; private set; }

    /// <summary>
    /// Collection of layers.
    /// </summary>
    public IEnumerable<Layer> Layers => _layers;

    private List<Layer> _layers = new();
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public Diagram()
    {
        AddLayer("Default");
    }

    /// <summary>
    /// Notify diagram to add layer geometry
    /// </summary>
    /// <param name="geometryObject">Geometry that has been added.</param>
    internal void HandleLayerGeometryAdd(GeometryObject geometryObject)
    {
        GeometryAdded?.Invoke(this, geometryObject);
    }

    /// <summary>
    /// Notify diagram to add layer geometry
    /// </summary>
    /// <param name="geometryObject">Geometry that has been removed.</param>
    internal void HandleLayerGeometryRemove(GeometryObject geometryObject)
    {
        GeometryRemoved?.Invoke(this, geometryObject);
    }

    /// <summary>
    /// Add layer to diagram.
    /// </summary>
    /// <param name="name">Layer name.</param>
    public Layer AddLayer(string name)
    {
        var layer = new Layer(name, this);
        _layers.Add(layer);
        ActiveLayer = layer;
        return layer;
    }

    /// <summary>
    /// Remove layer from diagram.
    /// </summary>
    /// <param name="layer"></param>
    public void RemoveLayer(Layer layer)
    {
        _layers.Remove(layer);
    }

    public void AddGeometry(GeometryObject geometry, Layer? layer = null)
    {
        layer ??= ActiveLayer;

        if (!_layers.Contains(layer))
        {
            throw new Exception("This layer does not belong to the chart.");
        }
        
        layer.AddGeometry(geometry);
        IncrementVersion();
    }

    /// <summary>
    /// The event fires when diagram geometry updates.
    /// </summary>
    public event EventHandler VersionChanged;

    /// <summary>
    /// Current version of the diagram.
    /// </summary>
    internal int Version { get; private set; }

    /// <summary>
    /// Increments current version.
    /// </summary>
    public void IncrementVersion()
    {
        Version++;
        VersionChanged?.Invoke(this, EventArgs.Empty);
    }

    public ModificationScope ModificationScope { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ModificationScope StartModification()
    {
        if (ModificationScope != null)
        {
            return ModificationScope;
        }

        ModificationScope = new ModificationScope(this);
        return ModificationScope;
    }

    #region Dispose

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private bool _disposed;

    /// <summary>
    /// Implementation of disposable pattern.
    /// </summary>
    /// <param name="isDisposing"></param>
    protected void Dispose(bool isDisposing)
    {
        if (_disposed)
        {
            return;
        }

        if (isDisposing)
        {
        
        }

        _disposed = true;
    }

    #endregion
}
