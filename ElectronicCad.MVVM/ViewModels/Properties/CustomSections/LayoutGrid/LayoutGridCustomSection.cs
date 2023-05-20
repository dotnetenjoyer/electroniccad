using ElectronicCad.Domain.Geometry.LayoutGrids;
using ElectronicCad.MVVM.Properties.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.DiagramLayoutGrid;

/// <summary>
/// Diagram layout grid custom section.
/// </summary>
public class LayoutGridCustomSection : BaseCustomSection<ILayoutGridProxy, LayoutGridModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="proxy"></param>
    /// <param name="serviceProvider"></param>
    public LayoutGridCustomSection(ILayoutGridProxy proxy, IServiceProvider serviceProvider) 
        : base(proxy, serviceProvider)
    {
    }

    /// <inheritdoc />
    protected override void UpdateFromProxyInternal()
    {
        base.UpdateFromProxyInternal();
     
        Model.LayoutGrids = new ObservableCollection<LayoutGrid>(Proxy.LayoutGrids);
    }
}

public class LayoutGridModel : ObservableObject
{
    public ObservableCollection<LayoutGrid> LayoutGrids { get; set; }
}

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