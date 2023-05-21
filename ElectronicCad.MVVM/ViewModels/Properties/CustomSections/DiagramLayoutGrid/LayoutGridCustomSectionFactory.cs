using ElectronicCad.MVVM.Properties.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;

/// <summary>
/// Layout grid custom section factory.
/// </summary>
public class LayoutGridCustomSectionFactory : ICustomSectionFactory
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Constructor.
    /// </summary>
    public LayoutGridCustomSectionFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inhertidoc />
    public bool CanCreate(IProxy proxy)
    {
        return proxy is ILayoutGridProxy;
    }

    /// <inhertidoc />
    public ICustomSection Create(IProxy proxy)
    {
        if (!CanCreate(proxy))
        {
            throw new InvalidOperationException($"Cannot create the {nameof(LayoutGridCustomSection)} the with specified proxy.");
        }

        return (LayoutGridCustomSection)ActivatorUtilities.CreateInstance(serviceProvider, 
            typeof(LayoutGridCustomSection), (ILayoutGridProxy)proxy);
    }
}