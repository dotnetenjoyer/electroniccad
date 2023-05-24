using ElectronicCad.Domain.Common;
using ElectronicCad.Domain.Geometry.LayoutGrids;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Diagram.
/// </summary>
public class Diagram : VersionableBase, IDisposable
{
    /// <summary>
    /// Diagram id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Diagram width.
    /// </summary>
    public float Width { get; set; } = 850;

    /// <summary>
    /// Diagram height.
    /// </summary>
    public float Height { get; set; } = 600;

    /// <summary>
    /// Constructor.
    /// </summary>
    public Diagram()
    {
        Id = Guid.NewGuid();
        AddLayer("Default");
    }

    #region Layer

    /// <summary>
    /// Diagram active layer.
    /// </summary>
    public Layer ActiveLayer { get; private set; }

    /// <summary>
    /// Collection of layers.
    /// </summary>
    public IEnumerable<Layer> Layers => layers;

    private readonly List<Layer> layers = new();

    /// <summary>
    /// Add layer to diagram.
    /// </summary>
    /// <param name="name">Layer name.</param>
    public Layer AddLayer(string name)
    {
        var layer = new Layer(name, this);
        layers.Add(layer);
        ActiveLayer = layer;
        return layer;
    }

    /// <summary>
    /// Remove layer from diagram.
    /// </summary>
    /// <param name="layer"></param>
    public void RemoveLayer(Layer layer)
    {
        layers.Remove(layer);
    }

    /// <summary>
    /// All diagram geometry objects.
    /// </summary>
    public IEnumerable<GeometryObject> GeometryObjects 
        => layers.SelectMany(layer => layer.GeometryObjects);

    /// <summary>
    /// Add a geometry object to the specified layer, 
    /// if the layer is null, then the geometry will be added to the active layer.
    /// </summary>
    /// <param name="geometry">Geometry object to add.</param>
    /// <param name="layer">Layer.</param>
    public void AddGeometry(GeometryObject geometry, Layer? layer = null)
    {
        layer ??= ActiveLayer;

        if (!layers.Contains(layer))
        {
            throw new Exception("This layer does not belong to the chart.");
        }

        layer.AddGeometry(geometry);
    }

    /// <summary>
    /// Clone specified geometry object.
    /// </summary>
    /// <param name="geometry">Geometry object.</param>
    public void CloneGeometry(GeometryObject geometryObject)
    {
        var geometryObjectType = geometryObject.GetType();
        var cloneConstructor = geometryObjectType.GetConstructors()
            .FirstOrDefault(constructor =>
            {
                var parameters = constructor.GetParameters();

                if (parameters.Count() != 1)
                {
                    return false;
                }

                var firstParameter = parameters.First();
                return firstParameter.ParameterType == geometryObjectType && parameters.Count() == 1;
            });

        if (cloneConstructor != null && geometryObject.Layer != null)
        {
            var clone = (GeometryObject)cloneConstructor.Invoke(new object[] { geometryObject });
            AddGeometry(clone);
        }
    }

    /// <summary>
    /// Removes geometry object from diagram.
    /// </summary>
    /// <param name="geometry">Geometry object that will be deleted.</param>
    public void RemoveGeometry(GeometryObject geometry)
    {
        var layer = layers.FirstOrDefault(layer => layer.GeometryObjects.Contains(geometry));
        if(layer != null)
        {
            layer.RemoveGeometry(geometry);
        }
    }

    #endregion

    #region DiagramChangingNotification

    /// <summary>
    /// The event fires on geometry added within diagram.
    /// </summary>
    public event EventHandler<GeometryObject> GeometryAdded;

    /// <summary>
    /// The event fires on geometry removed  withing diagram.
    /// </summary>
    public event EventHandler<GeometryObject> GeometryRemoved;

    /// <summary>
    /// The event fires when diagram geometry modified.
    /// </summary>
    public event EventHandler<IEnumerable<GeometryObject>> GeometryModified;

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
    /// Notify diagram about geometry modification.
    /// </summary>
    internal void HandleGeometryModification(IEnumerable<GeometryObject> modifiedGeometryObjects)
    {
        GeometryModified.Invoke(this, modifiedGeometryObjects);
    }

    internal DiagramModificationScope? ModificationScope { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DiagramModificationScope StartModificationScope()
    {
        if (ModificationScope != null)
        {
            return ModificationScope;
        }

        ModificationScope = new DiagramModificationScope(this);
        return ModificationScope;
    }

    #endregion

    #region LayoutGrid

    /// <summary>
    /// Calls when layout grids changes.
    /// </summary>
    public event EventHandler LayoutGridsUpdated;

    /// <summary>
    /// Diagram layout grids.
    /// </summary>
    public IEnumerable<LayoutGrid> LayoutGrids => layoutGrids;

    private List<LayoutGrid> layoutGrids = new();

    /// <summary>
    /// Add a new layout grid.
    /// </summary>
    /// <param name="layoutGrid">New layout grid.</param>
    public void AddLayoutGrid(LayoutGrid layoutGrid)
    {
        ValidateModification();
       
        layoutGrids.Add(layoutGrid);
        LayoutGridsUpdated?.Invoke(this, EventArgs.Empty);

        IncrementVersion();
    }

    /// <summary>
    /// Updates a layout grid.
    /// </summary>
    /// <param name="layoutGrid">Updated layout grid.</param>
    public void UpdateLayoutGrid(LayoutGrid layoutGrid)
    {
        var index = layoutGrids.FindIndex(l => l.Id == layoutGrid.Id);
        
        if (index < 0)
        {
            return;
        }

        ValidateModification();

        layoutGrids[index] = layoutGrid;
        LayoutGridsUpdated?.Invoke(this, EventArgs.Empty);

        IncrementVersion();
    }

    /// <summary>
    /// Remove a layout grid.
    /// </summary>
    /// <param name="layoutGrid">Layout grid.</param>
    public void RemoveLayoutGrid(LayoutGrid layoutGrid)
    {
        ValidateModification();
        
        layoutGrids.Remove(layoutGrid);
        LayoutGridsUpdated?.Invoke(this, EventArgs.Empty);

        IncrementVersion();
    }

    #endregion

    #region Group
    
    public void GroupGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
    {

    }
    
    #endregion

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
