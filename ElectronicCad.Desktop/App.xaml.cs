using System.Windows;
using ElectronicCad.Desktop.Views;

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
            
            var compositionRoot = CompositionRoot.GetInstance();
            base.OnStartup(e);
        }
    }
}