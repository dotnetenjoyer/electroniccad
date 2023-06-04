using ElectronicCad.Domain.Common;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Base implementation of proxy with source that implements IVersionable interface.
/// </summary>
/// <typeparam name="TSource">Type of source object.</typeparam>
public abstract class VersionablePropertiesProxy<TSource> : IPropertiesProxy where TSource : IVersionable
{
    /// <summary>
    /// Source object.
    /// </summary>
    protected TSource Source { get; init; }

    /// <inheritdoc />
    public event EventHandler<EventArgs> Updated;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="source">Proxy source object.</param>
    public VersionablePropertiesProxy(TSource source)
    {
        Source = source;
        UpdateFromSource();

        Source.VersionChanged += HandleSourceVersionChange;
    }

    private void HandleSourceVersionChange(object? sender, EventArgs eventArgs)
    {
        UpdateFromSource();
        Updated?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    public abstract void UpdateFromSource();

    /// <inheritdoc />
    public virtual void UpdateSource()
    {
        Source.VersionChanged -= HandleSourceVersionChange;
        UpdateSourceInternal();
        Source.VersionChanged += HandleSourceVersionChange;
    }

    /// <summary>
    /// Update proxy source properties.
    /// </summary>
    protected virtual void UpdateSourceInternal()
    {
    }
}