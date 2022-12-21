using System.Windows;
using System.Windows.Input;
using ElectronicCad.MVVM.ViewModels;

namespace ElectronicCad.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            // base.OnMouseDoubleClick(e);
        }
    }
}