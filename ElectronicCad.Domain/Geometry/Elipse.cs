namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Elipse.
/// </summary>
public class Elipse : GeometryObject
{
    /// <inheritdoc/>
    public override string Name { get; init; } = nameof(Elipse);
}