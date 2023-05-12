using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Line proxy.
/// </summary>
public class LinePropertyProxy : GeometryObjectProxy<Line>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="line">Line.</param>
    public LinePropertyProxy(Line line) : base(line)
    {
    }
}
