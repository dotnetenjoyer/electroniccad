using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.Proxies;

/// <summary>
/// Base proxy class.
/// </summary>
/// <typeparam name="TSource">Type of source object.</typeparam>
public abstract class PropertyProxy<TSource> : IProxy
{
    /// <summary>
    /// Proxy source object.
    /// </summary>
    public TSource Source { get; init; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="source">Proxy source object.</param>
    public PropertyProxy(TSource source)
    {
        Source = source;
        UpdateFromEntity();
    }

    /// <inheritdoc />
    public virtual void UpdateFromEntity()
    {
    }
    
    /// <inheritdoc />
    public virtual void UpdateToEntity()
    {
    }
}