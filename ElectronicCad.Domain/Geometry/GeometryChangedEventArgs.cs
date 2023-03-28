namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Geometry chagnes type.
/// </summary>
public enum GeometryChangeType
{
    Add,
    Update,
    Remove
}

/// <summary>
/// Event args for geometry changed events.
/// </summary>
public class GeometryChangedEventArgs : EventArgs
{
    /// <summary>
    /// Type of geometry changes.
    /// </summary>
    public GeometryChangeType Type { get; init; }

    /// <summary>
    /// Target geometry object.
    /// </summary>
    public GeometryObject GeometryObject { get; init; }

    /// <summary>
    /// Constcturo.
    /// </summary>
    /// <param name="type">Changes type.</param>
    /// <param name="geometry">Target geometry object.</param>
    public GeometryChangedEventArgs(GeometryChangeType type, GeometryObject geometry)
    {
        Type = type;
        GeometryObject = geometry;
    }
}