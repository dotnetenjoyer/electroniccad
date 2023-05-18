using ElectronicCad.Desktop.Infrastructure.Navigation;
using ElectronicCad.Desktop.Views;
using ElectronicCad.Diagramming;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using ElectronicCad.MVVM.Utils;
using ElectronicCad.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.Desktop.Infrastructure.DependencyInjection;

internal class DesktopModule
{
    public static void Register(IServiceCollection services, MainWindow mainWindow)
    {
        services.AddAutoMapper(typeof(MainViewModel));
        
        services.AddSingleton<ViewModelFactory>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<FrameNavigation>((provider) => 
            new FrameNavigation(mainWindow.Frame));
        
        MediatorModule.Register(services);
        InfrastructureModule.Register(services);
        DiagrammingModule.Register(services);
    }
}