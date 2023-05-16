using System.ComponentModel;
using ElectronicCad.Domain.Common;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Base proxy implementation.
/// </summary>
/// <typeparam name="TSource">Type of source object.</typeparam>
public abstract class BaseProxy<TSource> : IProxy, IPropertyModel where TSource : INotifyPropertyChanged
{
    /// <summary>
    /// Proxy source object.
    /// </summary>
    protected TSource Source { get; init; }

    /// <inheritdoc />
    public event EventHandler<EventArgs> Updated;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="source">Proxy source object.</param>
    public BaseProxy(TSource source)
    {
        Source = source;
        UpdateFromEntity();

        Source.PropertyChanged += HandleSourceChange;

        if (source is IVersionable objWithVersion)
        {
            objWithVersion.VersionChanged += HandleSourceVersionChange;
        }
    }

    private void HandleSourceChange(object? sender, PropertyChangedEventArgs e)
    {
        UpdateFromEntity();
        RaiseUpdatedEvent();
    }

    private void HandleSourceVersionChange(object? sender, EventArgs eventArgs)
    {
        UpdateFromEntity();
        RaiseUpdatedEvent();
    }

    /// <summary>
    /// Raises proxy updated event.
    /// </summary>
    protected void RaiseUpdatedEvent()
    {
        Updated?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    public abstract void UpdateFromEntity();

    /// <inheritdoc />
    public abstract void UpdateEntity();
}