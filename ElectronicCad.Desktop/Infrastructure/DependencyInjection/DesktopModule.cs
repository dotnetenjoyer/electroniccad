using ElectronicCad.Desktop.Infrastructure.Navigation;
using ElectronicCad.Desktop.Views;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.Desktop.Infrastructure.DependencyInjection;

internal class DesktopModule
{
    public static void Register(IServiceCollection services, MainWindow mainWindow)
    {
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<FrameNavigation>((provider) => 
            new FrameNavigation(mainWindow.Frame));
    }
}