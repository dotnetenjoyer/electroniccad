using System.Windows.Controls;
using ElectronicCad.Diagramming.Modes;
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
            //TODO: Dispose
            viewModel.DiagramModeChanged += HandleDiagramModeChange;
            DataContext = viewModel;
        }

        private void HandleDiagramModeChange(object? sender, DiagramMode newMode)
        {
            Diagram.SetDiagramMode(newMode);
        }
    }
}
