using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections;

/// <summary>
/// Base custom section implementation.
/// </summary>
/// <typeparam name="TProxy">Custom section proxy type.</typeparam>
/// <typeparam name="TModel">Custom section view model.</typeparam>

public abstract class BaseCustomSection<TProxy, TModel> : ObservableObject, ICustomSection where TProxy : IProxy where TModel : INotifyPropertyChanged
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Custom section proxy.
    /// </summary>
    protected TProxy Proxy { get; private set; }

    /// <summary>
    /// Custom section view model.
    /// </summary>
    public TModel Model { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="proxy">Custom section proxy.</param>
    public BaseCustomSection(TProxy proxy, IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;

        Proxy = proxy;
        Proxy.Updated += HandleProxyUpdated;
        
        Model = CreateModel();
    }

    private void HandleProxyUpdated(object? sender, EventArgs e)
    {
        UpdateFromProxy();
    }

    /// <summary>
    /// Creates custom section view model.
    /// </summary>
    /// <returns></returns>
    protected virtual TModel CreateModel()
    {
        return (TModel)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TModel));
    }

    /// <summary>
    /// Iniciate the custom section state update from a proxy.
    /// </summary>
    protected void UpdateFromProxy()
    {
        Model.PropertyChanged -= HandleModelPropertyChanges;
        
        UpdateFromProxyInternal();
        
        Model.PropertyChanged += HandleModelPropertyChanges;
    }

    /// <summary>
    /// Update custom section state from proxy.
    /// </summary>
    protected virtual void UpdateFromProxyInternal()
    {
    }

    private void HandleModelPropertyChanges(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateProxy();
    }

    private void UpdateProxy()
    {
        UpdateProxyInternal();
        Proxy.UpdateEntity();
    }

    /// <summary>
    /// Updates proxy state.
    /// </summary>
    protected virtual void UpdateProxyInternal()
    {
    }
}
