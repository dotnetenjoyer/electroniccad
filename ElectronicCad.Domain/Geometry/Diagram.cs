using ElectronicCad.Domain.Exceptions;
using ElectronicCad.Domain.Geometry.Extensions;
using ElectronicCad.Domain.Geometry.Layouts;
using ElectronicCad.Domain.Validations;

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

        AddLayer("Слой");
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
        ActivateLayer(layer);
        LayerAdded?.Invoke(this, layer);
        return layer;
    }

    /// <summary>
    /// Activates related layer.
    /// </summary>
    /// <param name="layer">Layer to activate.</param>
    public void ActivateLayer(Layer layer)
    {
        if (!layers.Contains(layer))
        {
            throw new DomainException("It isn't possible to activate a layer that is not related with the diagram");
        }

        ActiveLayer = layer;
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
            .Where(x => x.Group != null)
            .ToList();

        foreach (var geometryObjectsByGroup in groupedGeometryObjects.GroupBy(g => g.Group))
        {
            geometryObjectsByGroup.Key!.RemoveGeometry(geometryObjectsByGroup);
        }

        var layerGeometryObjects = geometryObjects
            .Except(groupedGeometryObjects)
            .ToList();

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
        var clone = geometryObject.Clone();
        
        if (geometryObject.Group != null)
        {
            geometryObject.Group.AddGeometry(clone);
        }
        else if (geometryObject.Layer != null)
        {
            geometryObject.Layer.AddGeometry(clone);
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
    public GeometryGroup CreateGroup(IEnumerable<GeometryObject> geometryObjects)
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

    /// <summary>
    /// Shows the geometry objects.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects to show.</param>
    public void ShowGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
    {
        ModifyGeometryObjects(geometryObjects, geometryObject => geometryObject.IsVisible = true);
    }

    /// <summary>
    /// Hides the geometry objects.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects to hide.</param>
    public void HideGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
    {
        ModifyGeometryObjects(geometryObjects, geometryObject => geometryObject.IsVisible = false);

    }
    
    /// <summary>
    /// Locks the geometry objects.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects to lock.</param>
    public void LockGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
    {
        ModifyGeometryObjects(geometryObjects, geometryObject => geometryObject.IsLock = true);
    }

    /// <summary>
    /// Unlocks the geometry objects.
    /// </summary>
    /// <param name="geometryObjects">Geometry objects to unlock.</param>
    public void UnlockGeometryObjects(IEnumerable<GeometryObject> geometryObjects)
    {
        ModifyGeometryObjects(geometryObjects, geometryObject => geometryObject.IsLock = false);
    }

    private void ModifyGeometryObjects(IEnumerable<GeometryObject> geometryObjects, Action<GeometryObject> modifyAction)
    {
        var ownGeometryObjects = geometryObjects
            .Where(x => x.Diagram == this)
            .ToList();

        foreach (var geometryObject in ownGeometryObjects)
        {
            geometryObject.StartModification();
            modifyAction.Invoke(geometryObject);
            geometryObject.CompleteModification();
        }
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

    #region Layout

    /// <summary>
    /// Diagram layouts.
    /// </summary>
    public IEnumerable<Layout> Layouts => layouts;

    private List<Layout> layouts = new();

    /// <summary>
    /// Add a new layout.
    /// </summary>
    /// <param name="layout">New layout.</param>
    public void AddLayout(Layout layout)
    {
        ValidateModification();
        layouts.Add(layout);
    }

    /// <summary>
    /// Updates a layout.
    /// </summary>
    /// <param name="layout">Updated layout.</param>
    public void UpdateLayout(Layout layout)
    {
        var index = layouts.FindIndex(l => l.Id == layout.Id);

        if (index < 0)
        {
            return;
        }

        ValidateModification();
        layouts[index] = layout;
    }

    /// <summary>
    /// Remove a layout.
    /// </summary>
    /// <param name="layout">Layout.</param>
    public void RemoveLayout(Layout layout)
    {
        ValidateModification();
        layouts.Remove(layout);
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
