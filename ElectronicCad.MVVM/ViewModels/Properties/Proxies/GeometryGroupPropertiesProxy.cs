using ElectronicCad.Domain.Geometry;
using ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Align;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Properties proxy to geometry group.
/// </summary>
public class GeometryGroupPropertiesProxy : VersionablePropertiesProxy<GeometryGroup>, IAlignPropertiesProxy
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
        // Clone? 
        GeometryObjects = Source.Children;
    }
}
