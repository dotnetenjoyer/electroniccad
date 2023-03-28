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
    /// The event fires on geometry changes within diagram.
    /// </summary>
    public event EventHandler<GeometryChangedEventArgs> GeometryChanged;

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
    /// Add layer to diagram.
    /// </summary>
    /// <param name="name">Layer name.</param>
    public void AddLayer(string name)
    {
        var layer = new Layer(name);
        layer.GeometryChanged += HandleLayerGeometryChanged;

        _layers.Add(layer);
        ActiveLayer = layer;
    }

    /// <summary>
    /// Remove layer from diagram.
    /// </summary>
    /// <param name="layer"></param>
    public void RemoveLayer(Layer layer)
    {
        layer.GeometryChanged -= HandleLayerGeometryChanged;
        _layers.Remove(layer);
    }

    private void HandleLayerGeometryChanged(object? sender, GeometryChangedEventArgs eventArgs)
    {
        GeometryChanged?.Invoke(this, eventArgs);
    }

    public void AddGeometry(GeometryObject geometry, Layer? layer = null)
    {   
        if(!_layers.Contains(layer))
        {
            throw new Exception("This layer does not belong to the chart.");
        }
        
        layer ??= ActiveLayer;
        layer.AddGeometry(geometry);
    }

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
        if(ModificationScope != null)
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
            _layers.ForEach(_ => _.GeometryChanged -= HandleLayerGeometryChanged);
        }

        _disposed = true;
    }

    #endregion
}
