using ElectronicCad.MVVM.Properties.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections;

/// <summary>
/// Base custom section implementation.
/// </summary>
/// <typeparam name="TProxy">Custom section proxy type.</typeparam>
/// <typeparam name="TModel">Custom section view model.</typeparam>

public abstract class BaseCustomSection<TProxy, TModel> : ObservableObject, ICustomSection where TProxy : IProxy where TModel : INotifyPropertyChanged
{
    /// <summary>
    /// Custom section proxy.
    /// </summary>
    protected TProxy Proxy { get; private set; }

    /// <summary>
    /// Custom section view model.
    /// </summary>
    public TModel Model
    {
        get => model;
        set => SetProperty(ref model, value);
    }

    private TModel model;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="proxy">Custom section proxy.</param>
    public BaseCustomSection(TProxy proxy, IServiceProvider serviceProvider)
    {
        Proxy = proxy;
        Proxy.Updated += HandleProxyUpdated;

        Model = (TModel)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TModel));
        UpdateFromProxy();
    }

    private void HandleProxyUpdated(object? sender, EventArgs e)
    {
        UpdateFromProxy();
    }

    private void UpdateFromProxy()
    {
        Model.PropertyChanged -= HandleShapeModelPropertyChanges;
        UpdateFromProxyInternal();
        Model.PropertyChanged += HandleShapeModelPropertyChanges;
    }

    private void HandleShapeModelPropertyChanges(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateProxy();
    }

    /// <summary>
    /// Update custom section state from proxy.
    /// </summary>
    protected virtual void UpdateFromProxyInternal()
    {
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