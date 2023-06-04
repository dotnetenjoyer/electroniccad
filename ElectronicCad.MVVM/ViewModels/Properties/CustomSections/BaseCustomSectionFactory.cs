using ElectronicCad.MVVM.Properties.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections;

/// <summary>
/// Implementation of base custom section factory.
/// </summary>
/// <typeparam name="TCustomSection">Type of custom section.</typeparam>
/// <typeparam name="TProxy">Type of related proxy.</typeparam>
public abstract class BaseCustomSectionFactory<TCustomSection, TProxy> : ICustomSectionFactory
    where TCustomSection : ICustomSection where TProxy : IPropertiesProxy
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    public BaseCustomSectionFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public bool CanCreate(IPropertiesProxy proxy)
    {
        if (proxy is TProxy)
        {
            return true;
        }

        return false;
    }
    
    /// <inheritdoc />
    public ICustomSection Create(IPropertiesProxy proxy)
    {
        if (!CanCreate(proxy))
        {
            throw new InvalidOperationException($"Cannot create {nameof(TCustomSection)}");
        }

        var customSection = (TCustomSection)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TCustomSection), proxy);
        return customSection;
    }
}