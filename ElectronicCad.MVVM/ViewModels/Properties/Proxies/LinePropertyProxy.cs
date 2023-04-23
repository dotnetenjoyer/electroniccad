using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Line proxy.
/// </summary>
public class LinePropertyProxy : GeometryObjectPropertyProxy<Line>
{
    public bool IsEnable { get; set; }

    public Color Color { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="line">Line.</param>
    public LinePropertyProxy(Line line) : base(line)
    {

    }
}

public enum Color
{
    Red,
    Gree,
    Blue
}