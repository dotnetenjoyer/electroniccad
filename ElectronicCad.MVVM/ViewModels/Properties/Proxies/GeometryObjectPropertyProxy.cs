using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Geometry object property proxy.
/// </summary>
public class GeometryObjectPropertyProxy : IPropertyModel
{
    /// <summary>
    /// Width.
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    /// Height
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// Center X coordinate.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// Center Y coordinate
    /// </summary>
    public float Y { get; set; }
}