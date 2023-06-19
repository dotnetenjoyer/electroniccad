namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Polygon geometry object.
/// </summary>
public class Polygon : ContentGeometry
{
    /// <summary>
    /// Corner radius.
    /// </summary>
    public double CornerRadius
    {
        get => cornerRadius;
        set
        {
            ValidateModification();
            SetProperty(ref cornerRadius, value);
        }
    }

    private double cornerRadius = 0;

    /// <summary>
    /// Constructor.
    /// </summary>
    public Polygon(Point center, double width, double height, bool isTemperary = false) : base(center, width, height, isTemperary)
    {
        Name = "Прямоугольник";
    }

    /// <summary>
    /// Cloning constructor.
    /// </summary>
    /// <param name="cloneFrom">Clone from.</param>
    public Polygon(Polygon cloneFrom) : base(cloneFrom)
    {
        cornerRadius = cloneFrom.CornerRadius;
    }
}