using System.Windows.Controls;
using ElectronicCad.Diagramming.ViewModels;

namespace ElectronicCad.Diagramming
{
    /// <summary>
    /// Interaction logic for DiagramControl.xaml
    /// </summary>
    public partial class DiagramControl : UserControl
    {
        public DiagramControl()
        {
            InitializeComponent();
            var viewModel = new DiagramControlViewModel();
            DataContext = viewModel;
            
            viewModel.DiagramModeChanged += (sender, args) =>
            {
                Diagram.SetDiagramMode(viewModel.UpperToolbar.DiagramMode);
            };

        }
    }
}
