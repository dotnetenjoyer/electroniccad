using ElectronicCad.Desktop.Infrastructure.Navigation;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.Desktop.Infrastructure.DependencyInjection;

internal class DesktopModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IDialogService, DialogService>();
    }
}