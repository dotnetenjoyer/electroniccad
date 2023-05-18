using System;
using System.IO;
using ElectronicCad.Desktop.Infrastructure.DependencyInjection;
using ElectronicCad.Desktop.Views;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
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
    /// Main window.
    /// </summary>
    public static MainWindow MainWindow { get; set; }

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

    /// <summary>
    /// Return application data folder path.
    /// </summary>
    /// <returns>Path to application data folder.</returns>
    public static string GetApplicationDataFolder()
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(folderPath, "Nuldes");
    }

    /// <summary>
    /// Creates an application data folder if it doesn't exist.
    /// </summary>
    public static void EnsureApplicationDataFolderExsisting()
    {
        var applicationDataFolderPath = GetApplicationDataFolder();
        if (!Directory.Exists(applicationDataFolderPath))
        {
            Directory.CreateDirectory(applicationDataFolderPath);
        }
    }

    private void Configure()
    {
        EnsureApplicationDataFolderExsisting();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection serviceCollection)
    {
        DesktopModule.Register(serviceCollection, MainWindow);
    }
}