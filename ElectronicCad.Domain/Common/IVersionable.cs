namespace ElectronicCad.Domain.Common;

/// <summary>
/// Contains members to support object versioning.
/// </summary>
public interface IVersionable
{
    /// <summary>
    /// The version of the diagram object.
    /// Increases each time when importants properties of the object changes.
    /// </summary>
    int Version { get; }

    /// <summary>
    /// Version change event.
    /// </summary>
    event EventHandler? VersionChanged;
}