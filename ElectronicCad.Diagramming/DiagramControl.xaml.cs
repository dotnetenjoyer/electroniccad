using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectronicCad.Diagramming
{
    /// <summary>
    /// Interaction logic for DiagramControl.xaml
    /// </summary>
    public partial class DiagramControl : UserControl
    {
        public readonly static DependencyProperty DiagramBackgroundProperty = DependencyProperty
            .Register("DiagramBackground", typeof(Brush), typeof(DiagramControl));

        public Brush DiagramBackground
        {
            get => (Brush)GetValue(DiagramBackgroundProperty);
            set => SetValue(DiagramBackgroundProperty, value);
        }

        public DiagramControl()
        {
            InitializeComponent();
        }
    }
}
