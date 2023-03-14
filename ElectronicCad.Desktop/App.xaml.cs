using System.Windows;
using ElectronicCad.Desktop.Views;
using ElectronicCad.MVVM.ServiceAbstractions.Navigation;
using ElectronicCad.MVVM.ViewModels;
using ElectronicCad.MVVM.ViewModels.Project;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicCad.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <inheritdoc/>
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            CompositionRoot.MainWindow = mainWindow;
            var compositionRoot = CompositionRoot.GetInstance();
            // var dialogService = compositionRoot.ServiceProvider.GetRequiredService<IDialogService>();
            // dialogService.OpenAsync<WelcomeViewModel>();
            base.OnStartup(e);
        }
    }
}