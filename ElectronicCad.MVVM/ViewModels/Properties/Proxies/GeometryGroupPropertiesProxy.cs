using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Align;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Properties proxy to geometry group.
/// </summary>
public class GeometryGroupPropertiesProxy : GeometryObjectPropertiesProxy<GeometryGroup>, IAlignPropertiesProxy
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="group">Geometry group.</param>
    public GeometryGroupPropertiesProxy(GeometryGroup group) : base(group)
    {
    }

    /// <inheritdoc />
    public IEnumerable<GeometryObject> GeometryObjects { get; set; }

    public override void UpdateFromSource()
    {
        base.UpdateFromSource();
        GeometryObjects = Source.Children;
    }
}

public class VirtualGeometryGroupPropertiesProxy : IAlignPropertiesProxy
{
    public IEnumerable<GeometryObject> GeometryObjects => throw new NotImplementedException();

    public event EventHandler<EventArgs> Updated;

    public void UpdateFromSource()
    {
        throw new NotImplementedException();
    }

    public void UpdateSource()
    {
        throw new NotImplementedException();
    }
}