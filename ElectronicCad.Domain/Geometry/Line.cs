namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Line.
/// </summary>
public class Line : GeometryObject
{
    /// <inheritdoc/>
    public override string Name { get; init; } = nameof(Line);
}