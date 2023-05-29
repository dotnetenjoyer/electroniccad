using ElectronicCad.Domain.Exceptions;
using ElectronicCad.Domain.Geometry.Extensions;
using ElectronicCad.Domain.Geometry.LayoutGrids;
using ElectronicCad.Domain.Validations;
using System.ComponentModel;

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

        AddLayer("Default layer");
    }

    #region Layers.

    /// <summary>
    /// The event raises when a new layer is added.
    /// </summary>
    public event EventHandler<Layer> LayerAdded;

    /// <summary>
    /// The event rases when a layer is removed.
    /// </summary>
    public event EventHandler<Layer> LayerRemoved;

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
        LayerAdded?.Invoke(this, layer);
        return layer;
    }

    /// <summary>
    /// Remove layer from diagram.
    /// </summary>
    /// <param name="layer">Layer to remove.</param>
    public void RemoveLayer(Layer layer)
    {
        layers.Remove(layer);
        if (ActiveLayer == layer)
        {
            ActiveLayer = layers.Last();
        }

        LayerRemoved?.Invoke(this, layer);
    }

    #endregion

    #region Geometry objects

    /// <inheritdoc />
    public IEnumerable<GeometryObject> Children
        => layers.SelectMany(layer => layer.Children);

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
    /// <param name="geometryObjects">Geometry objects thaht has been added.</param>
    internal void RaiseGeometryAdded(IEnumerable<GeometryObject> geometryObjects)
    {
        GeometryAdded?.Invoke(this, geometryObjects);
    }

    /// <summary>
    /// Raises the geometry removed event with the specified arguments.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects thaht has been removed.</param>
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

    /// <inheritdoc />
    public void AddGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        ActiveLayer.AddGeometry(geometryObjects);
    }

    /// <summary>
    /// Removes the geometry objects from the diagram.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects to remove.</param>
    public void RemoveGeometry(IEnumerable<GeometryObject> geometryObjects)
    {
        if (geometryObjects.Any(g => g.Diagram != this))
        {
            throw new DomainException("Cannot remove geometry objects that not related with current diagram.");
        }

        var groupedGeometryObjects = geometryObjects
            .Where(x => x.Group != null);

        foreach (var geometryObjectsByGroup in groupedGeometryObjects.GroupBy(g => g.Group))
        {
            geometryObjectsByGroup.Key!.RemoveGeometry(geometryObjectsByGroup);
        }

        var layerGeometryObjects = geometryObjects
            .Except(groupedGeometryObjects);

        foreach (var geometryObjectsByLayer in layerGeometryObjects.GroupBy(g => g.Layer))
        {
            geometryObjectsByLayer.Key!.RemoveGeometry(geometryObjectsByLayer);
        }
    }

    /// <summary>
    /// Duplicates specified geometry object.
    /// </summary>
    /// <param name="geometry">Geometry object to clone.</param>
    public void DuplicateGeometry(GeometryObject geometryObject)
    {
        if (geometryObject.Group != null)
        {
            var clone = CreateClone();
            geometryObject.Group.AddGeometry(clone);
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
    /// Validate ability to create a group with the specified geometry objects.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects.</param>
    /// <returns>Validation result.</returns>
    public ValidationResult CanCreateGroup(IEnumerable<GeometryObject> geometryObjects)
    {
        var result = new ValidationResult();

        if (!geometryObjects.Any())
        {
            result.AddError(nameof(geometryObjects), "It is impossible to create group without children.");
        }

        var group = geometryObjects.First().Group;
        bool isOneGroup = geometryObjects.All(g => g.Group == group);

        var layer = geometryObjects.First().Layer;
        var isOneLayer = geometryObjects.All(g => g.Layer == layer);

        if (!(isOneLayer && isOneGroup))
        {
            result.AddError(nameof(geometryObjects), "It is impossible to group geometry with different parents");
        }

        return result;
    }

    /// <summary>
    /// Creates geometry group.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects to group.</param>
    public GeometryGroup CreateGroup(params GeometryObject[] geometryObjects)
    {
        var validationResult = CanCreateGroup(geometryObjects);
        if (!validationResult.IsSuccessed)
        {
            throw new ValidationException(validationResult);
        }

        var geometryObject = geometryObjects.First();
        IGeometryContainer? container = geometryObject.Group != null 
            ? geometryObject.Group 
            : geometryObject.Layer;

        container!.RemoveGeometry(geometryObjects);
        
        var group = new GeometryGroup(geometryObjects);
        container.AddGeometry(group);
        
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
