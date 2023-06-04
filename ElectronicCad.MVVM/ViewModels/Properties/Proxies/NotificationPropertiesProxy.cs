using ElectronicCad.MVVM.Properties.Abstractions;
using System.ComponentModel;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Base implementation of proxy with source that implements INotifyPropertyChanged interface.
/// </summary>
/// <typeparam name="TSource">Type of source object.</typeparam>
public abstract class NotificationPropertiesProxy<TSource> : IPropertiesProxy where TSource : INotifyPropertyChanged
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
    public NotificationPropertiesProxy(TSource source)
    {
        Source = source;
        UpdateFromSource();

        Source.PropertyChanged += HandleSourcePropertyChange;
    }

    protected void HandleSourcePropertyChange(object? sender, EventArgs eventArgs)
    {
        UpdateFromSource();
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
    public abstract void UpdateFromSource();

    /// <inheritdoc />
    public virtual void UpdateSource()
    {
        Source.PropertyChanged -= HandleSourcePropertyChange;
        UpdateSourceInternal();
        Source.PropertyChanged += HandleSourcePropertyChange;
    }

    /// <summary>
    /// Update proxy source properties.
    /// </summary>
    protected virtual void UpdateSourceInternal()
    {
    }
}