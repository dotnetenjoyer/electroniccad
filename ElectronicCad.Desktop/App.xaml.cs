using System.Windows;
using ElectronicCad.Desktop.Views;
using ElectronicCad.MVVM.Utils;
using ElectronicCad.MVVM.ViewModels;
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

            var viewModelFactory = compositionRoot.ServiceProvider.GetRequiredService<ViewModelFactory>();
            var mainViewModel = viewModelFactory.Create<MainViewModel>();
            mainWindow.DataContext = mainViewModel;
            mainViewModel.LoadAsync();

            //var dialogService = compositionRoot.ServiceProvider.GetRequiredService<IDialogService>();
            //dialogService.OpenAsync<WelcomeViewModel>();
            
            base.OnStartup(e);
        }
    }
}