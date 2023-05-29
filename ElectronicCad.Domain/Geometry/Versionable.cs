using ElectronicCad.Domain.Common;
using ElectronicCad.Domain.Exceptions;

namespace ElectronicCad.Domain.Geometry;

/// <summary>
/// Base versionable implementation.
/// </summary>
public class VersionableBase : IVersionable
{
    /// <inheritdoc />
    public int Version { get; private set; }

    /// <inheritdoc />
    public event EventHandler? VersionChanged;

    /// <summary>
    /// Indicates if modification was started.
    /// </summary>
    public bool IsModificationStarted { get; private set; }

    /// <summary>
    /// Increases the version of the geometry object and invokes the <see cref="VersionChanged">.
    /// </summary>
    protected void IncrementVersion()
    {
        Version++;
        VersionChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Validates availability of modification.
    /// </summary>
    protected virtual void ValidateModification()
    {
        if (!IsModificationStarted)
        {
            throw new DomainException("Modification is not started.");
        }
    }

    /// <summary>
    /// Starts object modification.
    /// </summary>
    public void StartModification()
    {
        if (IsModificationStarted)
        {
            throw new DomainException("Attempting to start a modification when it is already started.");
        }

        IsModificationStarted = true;
    }

    /// <summary>
    /// Completes object modification.
    /// </summary>
    public void CompleteModification()
    {
        if (!IsModificationStarted)
        {
            throw new DomainException("Attempting to complete a modification when it is not started.");
        }

        IsModificationStarted = false;
        IncrementVersion();
    }
}
