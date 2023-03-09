using System;
using ElectronicCad.Desktop.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.Desktop;

internal class CompositionRoot
{
    private static CompositionRoot _instanse;
    
    private IServiceProvider _serviceProvider;

    /// <summary>
    /// Service provider;
    /// </summary>
    public IServiceProvider ServiceProvider => _serviceProvider;

    /// <summary>
    /// Get an instance of composition root.
    /// </summary>
    public static CompositionRoot GetInstance()
    {
        if (_instanse == null)
        {
            _instanse = new CompositionRoot();
            _instanse.Configure();
        }

        return _instanse;
    }

    private void Configure()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection serviceCollection)
    {
        DesktopModule.Register(serviceCollection);
    }
}