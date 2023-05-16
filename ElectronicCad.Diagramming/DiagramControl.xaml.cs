using System.Windows;
using System.Windows.Controls;
using ElectronicCad.Diagramming.Drawing.Modes;
using DomainDiagram = ElectronicCad.Domain.Geometry.Diagram;

namespace ElectronicCad.Diagramming
{
    /// <summary>
    /// Interaction logic for DiagramControl.xaml
    /// </summary>
    public partial class DiagramControl : UserControl
    {
        /// <summary>
        /// Related domain diagram..
        /// </summary>
        public DomainDiagram DomainDiagram 
        { 
            get => (DomainDiagram)GetValue(DomainDiagramProperty);
            set => SetValue(DomainDiagramProperty, value);
        }

        private static readonly DependencyProperty DomainDiagramProperty = DependencyProperty
            .Register(nameof(DomainDiagram),
                typeof(DomainDiagram),
                typeof(DiagramControl),
                new PropertyMetadata());

        /// <summary>
        /// Constructor.
        /// </summary>
        public DiagramControl()
        {
            InitializeComponent();
        }

        private void HandleToolbarModeChanged(object? sender, DiagramMode newMode)
        {
            Diagram.SetDiagramMode(newMode);
        }
    }
}
