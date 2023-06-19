namespace ElectronicCad.Domain.Common;

/// <summary>
/// Abstraction for object that have name.
/// </summary>
public interface IHaveName
{
    /// <summary>
    /// Name of object.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Renames object.
    /// </summary>
    void Rename(string name);
}