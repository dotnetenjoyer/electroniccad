namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Represent size of the element.
/// </summary>
public struct Size
{
    /// <summary>
    /// Width.
    /// </summary>
    public double Width { get; init; }

    /// <summary>
    /// Height.
    /// </summary>
    public double Height { get; init; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    public Size(double width, double height)
    {
        Width = width;
        Height = height;
    }
}

