using System;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicCad.Desktop.UI.Components
{
    /// <summary>
    /// Interaction logic for IconCheckbox.xaml
    /// </summary>
    public partial class IconCheckBox : UserControl
    {
        /// <inheritdoc cref="DisableIconPathProperty">
        public string DisableIconPath
        {
            get => (string)GetValue(DisableIconPathProperty);
            set => SetValue(DisableIconPathProperty, value);
        }

        /// <summary>
        /// The path of the icon that is displayed when the checkbox isn't checked.
        /// </summary>
        public static DependencyProperty DisableIconPathProperty = DependencyProperty.Register(
            nameof(DisableIconPath),
            typeof(string),
            typeof(IconCheckBox),
            new PropertyMetadata());

        /// <inheritdoc cref="EnableIconPathProperty">
        public string EnableIconPath
        {
            get => (string)GetValue(EnableIconPathProperty);
            set => SetValue(EnableIconPathProperty, value);
        }

        /// <summary>
        /// The path of the icon that is displayed when the checkbox is checked.
        /// </summary>
        public static DependencyProperty EnableIconPathProperty = DependencyProperty.Register(
            nameof(EnableIconPath),
            typeof(string),
            typeof(IconCheckBox),
            new PropertyMetadata());

        /// <inheritdoc cref="IsCheckedProperty"/>
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        /// <inheritdoc cref="CheckBox.IsCheckedProperty"/>
        public static DependencyProperty IsCheckedProperty = CheckBox.IsCheckedProperty
            .AddOwner(typeof(IconCheckBox), new PropertyMetadata(Test));

        private static void Test(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public IconCheckBox()
        {
            InitializeComponent();
        }
    }
}
