using ElectronicCad.Domain.Geometry.LayoutGrids;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Diagram.
/// </summary>
public class Diagram : VersionableBase, IGeometryContainer, IDisposable
{
    /// <summary>
    /// Diagram id.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Diagram size.
    /// </summary>
    public Size Size 
    { 
        get => size;
        set
        {
            ValidateModification();
            size = value;
        } 
    }

    private Size size;

    /// <summary>
    /// Constructor.
    /// </summary>
    public Diagram()
    {
        Id = Guid.NewGuid();
        size = new Size(850, 600);

        AddLayer("Default");
    }

    #region Geometry changes events.

    /// <summary>
    /// The event raises on geomfiresdded within diagram.
    /// </summary>
    public event EventHandler<IEnumerable<GeometryObject>> GeometryAdded;

    /// <summary>
    /// The event raises on geomfires moved  withing diagram.
    /// </summary>
    public event EventHandler<IEnumerable<GeometryObject>> GeometryRemoved;

    /// <summary>
    /// The event raises when diagram geometry modified.
    /// </summary>
    public event EventHandler<IEnumerable<GeometryObject>> GeometryModified;

    /// <summary>
    /// Raises the geometry added event with the specified arguments.
    /// </summary>
    /// <param name="geometryObjects">Geometry that has been added.</param>
    internal void RaiseGeometryAdded(IEnumerable<GeometryObject> geometryObjects)
    {
        GeometryAdded?.Invoke(this, geometryObjects);
    }

    /// <summary>
    /// Raises the geometry removed event with the specified arguments.
    /// </summary>
    /// <param name="geometryObjects">Geometry that has been removed.</param>
    internal void RaiseGeometryRemoved(IEnumerable<GeometryObject> geometryObjects)
    {
        GeometryRemoved?.Invoke(this, geometryObjects);
    }

    /// <summary>
    /// Raises the geometry modified event with the specified arguments.
    /// </summary>
    /// <param name="geometryObjects">Geometry that has been modified.</param>
    internal void RaiseGeometryModified(IEnumerable<GeometryObject> modifiedGeometryObjects)
    {
        GeometryModified.Invoke(this, modifiedGeometryObjects);
    }

    #endregion

    #region Layers.

    /// <inheritdoc />
    public IEnumerable<GeometryObject> Children
        => layers.SelectMany(layer => layer.Children);

    /// <inheritdoc />
    public IGeometryContainer? Parent => null;

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

    /// <inheritdoc />
    public void AddGeometry(GeometryObject geometryObject)
    {
        AddGeometry(new[] { geometryObject });
    }

    /// <inheritdoc />
    public void AddGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        ActiveLayer.AddGeometry(geometryObjects);
    }

    /// <summary>
    /// Removes geometry object from diagram.
    /// </summary>
    /// <param name="geometryObject">Geometry object to remove.</param>
    public void RemoveGeometry(GeometryObject geometryObject)
    {
        RemoveGeometry(new[] { geometryObject} );
    }

    /// <summary>
    /// Removes the geometry objects from the diagram.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects to remove.</param>
    public void RemoveGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        var geometryGroupsByParent = geometryObjects
            .Where(x => x.Parent != null)
            .GroupBy(x => x.Parent);

        foreach (var groupOfGeometry in geometryGroupsByParent)
        {
            groupOfGeometry.Key!.RemoveGeometry(groupOfGeometry);
        }
    }

    /// <summary>
    /// Clone specified geometry object.
    /// </summary>
    /// <param name="geometry">Geometry object to clone.</param>
    public void CloneGeometry(GeometryObject geometryObject)
    {
        if (geometryObject.Parent != null)
        {
            var clone = CreateClone();
            AddGeometry(clone);
        }

        GeometryObject CreateClone()
        {
            var geometryType = geometryObject.GetType();

            var cloneConstructor = geometryType.GetConstructors()
                .First(constructor =>
                {
                    var parameters = constructor.GetParameters();
                    return parameters.First().ParameterType == geometryType && parameters.Count() == 1;
                });

            var clone = (GeometryObject)cloneConstructor.Invoke(new object[] { geometryObject });
            return clone;
        }
    }

    /// <summary>
    /// Creates geometry group.
    /// </summary>
    /// <param name="geometryObjects">Geometry to group.</param>
    public GeometryGroup CreateGroup(IEnumerable<GeometryObject> geometryObjects)
    {
        if (!geometryObjects.Any())
        {
            throw new Exception("It is impossible to create group without children.");
        }

        var parent = geometryObjects.First().Parent;
        if (parent == null)
        {
            throw new InvalidOperationException("It is impossible to group geometry with different parents.");
        }

        bool isOneParent = geometryObjects.All(x => x.Parent == parent);
        if (!isOneParent)
        {
            throw new InvalidOperationException("It is impossible to group geometry with different parents.");
        }

        parent.RemoveGeometry(geometryObjects);
        var group = new GeometryGroup(geometryObjects);
        parent.AddGeometry(group);
        return group;
    }

    #endregion

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

    #region LayoutGrid

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
    }

    /// <summary>
    /// Remove a layout grid.
    /// </summary>
    /// <param name="layoutGrid">Layout grid.</param>
    public void RemoveLayoutGrid(LayoutGrid layoutGrid)
    {
        ValidateModification();
        layoutGrids.Remove(layoutGrid);
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
