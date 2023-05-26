using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Infrastructure.Abstractions.Models.Projects;

/// <summary>
/// Describe size prototype.
/// </summary>
public class SizePrototype
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Name of the size prototype.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Size.
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">Name of the diagram scael.</param>
    /// <param name="width">Width.</param>
    /// <param name="height">Height.</param>
    public SizePrototype(string name, double width, double height)
    {
        Id = Guid.NewGuid();
        Name = name;
        Size = new Size(width, height);
    }
}