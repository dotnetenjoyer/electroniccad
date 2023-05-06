using ElectronicCad.Domain.Common;
using ElectronicCad.MVVM.Properties.Abstractions;
using System.ComponentModel;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Base proxy class.
/// </summary>
/// <typeparam name="TSource">Type of source object.</typeparam>
public abstract class BaseProxy<TSource> : IProxy where TSource : INotifyPropertyChanged
{
    /// <summary>
    /// Proxy source object.
    /// </summary>
    protected TSource Source { get; init; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="source">Proxy source object.</param>
    public BaseProxy(TSource source)
    {
        Source = source;
        UpdateFromEntity();

        if (source is IVersionable objWithVersion)
        {
            objWithVersion.VersionChanged += (sender, args) =>
            {
                UpdateFromEntity();
            };
        }

        Source.PropertyChanged += HandleSourceChanged;
    }
    
    private void HandleSourceChanged(object? sender, PropertyChangedEventArgs e)
    {
        UpdateFromEntity();
    }

    /// <inheritdoc />
    public event EventHandler<EventArgs> Updated;

    /// <summary>
    /// Invokes update from entity event.
    /// </summary>
    protected void OnUpdateFromEntity()
    {
        Updated?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    public virtual void UpdateFromEntity()
    {
    }
    
    /// <inheritdoc />
    public virtual void UpdateEntity()
    {
    }
}