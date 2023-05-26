using ElectronicCad.Infrastructure.Abstractions.Models.Projects;

namespace ElectronicCad.Infrastructure.Abstractions.Services.Projects;

/// <summary>
/// Diagram size prototypes storage abstraction.
/// </summary>
public interface ISizePrototypesStorage
{
    /// <summary>
    /// Returns a collection of the size prototypes.
    /// </summary>
    /// <returns>Collection of the size prototypes.</returns>
    IEnumerable<SizePrototype> GetPrototypes();

    /// <summary>
    /// Adds a new size prototype.
    /// </summary>
    /// <param name="prototype">Size prototype to add.</param>
    void AddPrototype(SizePrototype prototype);

    /// <summary>
    /// Removes a size prototype.
    /// </summary>
    /// <param name="prototype">Size prototype to remove.</param>
    void RemovePrototype(SizePrototype prototype);
}