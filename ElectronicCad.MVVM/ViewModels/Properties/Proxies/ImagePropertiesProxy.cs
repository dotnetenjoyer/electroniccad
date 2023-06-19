using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Image properties proxy.
/// </summary>
public class ImagePropertiesProxy : GeometryObjectPropertiesProxy<Image>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="image">Image.</param>
    public ImagePropertiesProxy(Image image) : base(image)
    {
    }
}