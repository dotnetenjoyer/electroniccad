using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ElectronicCad.Desktop.UI.Components
{
    /// <summary>
    /// Interaction logic for u.xaml
    /// </summary>
    public partial class InputControl : UserControl
    {
        /// <summary>
        /// Title.
        /// </summary>
        public string Title 
        { 
            get => (string)GetValue(TitleProperty); 
            set => SetValue(TitleProperty, value);
        }

        private static DependencyProperty TitleProperty = 
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(InputControl),
                new PropertyMetadata());

        /// <summary>
        /// Placeholder.
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        private static DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(
                nameof(Placeholder),
                typeof(string),
                typeof(InputControl),
                new PropertyMetadata());

        /// <summary>
        /// Value.
        /// </summary>
        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(string),
                typeof(InputControl),
                new FrameworkPropertyMetadata
                {
                    BindsTwoWayByDefault = true,
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });

        /// <summary>
        /// String format.
        /// </summary>
        public string StringFormat
        {
            get => (string)GetValue(StringFormatProperty);
            set => SetValue(StringFormatProperty, value);
        }

        private static DependencyProperty StringFormatProperty =
            DependencyProperty.Register(
                nameof(StringFormat),
                typeof(string),
                typeof(InputControl),
                new FrameworkPropertyMetadata(string.Empty));


        /// <summary>
        /// Constructor.
        /// </summary>
        public InputControl()
        {
            InitializeComponent();
        }
    }
}
