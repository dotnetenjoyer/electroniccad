using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using ElectronicCad.Desktop.UI.Utils;
using Microsoft.Toolkit.Mvvm.Input;

namespace ElectronicCad.Desktop.UI.Components
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        private static readonly Brush HueSpectrumBrush;
        private static readonly Brush SpectrumWhiteBrush;
        private static readonly Brush SpectrumBlackBrush;

        private static readonly Color[] SystemColors;

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ColorPicker()
        {
            var hueSpectrumBrush = new LinearGradientBrush();
            hueSpectrumBrush.GradientStops.Add(new GradientStop(new HSLColor(0, 1, .5f).ToColor(), 0.00));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(new HSLColor(298.8f, 1, .5f).ToColor(), 0.17));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(new HSLColor(241.2f, 1, .5f).ToColor(), 0.33));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(new HSLColor(180f, 1, .5f).ToColor(), 0.5));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(new HSLColor(118.8f, 1, .5f).ToColor(), 0.67));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(new HSLColor(61.2f, 1, .5f).ToColor(), 0.83));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(new HSLColor(360, 1, .5f).ToColor(), 1));
            HueSpectrumBrush = hueSpectrumBrush;

            var whiteGradient = new LinearGradientBrush();
            whiteGradient.StartPoint = new Point(0, 0);
            whiteGradient.EndPoint = new Point(1, 0);
            whiteGradient.GradientStops.Add(new GradientStop(Colors.White, 0));
            whiteGradient.GradientStops.Add(new GradientStop(Colors.Transparent, 1));
            SpectrumWhiteBrush = whiteGradient;

            var blackGradient = new LinearGradientBrush();
            blackGradient.StartPoint = new Point(0, 0);
            blackGradient.EndPoint = new Point(0, 1);
            blackGradient.GradientStops.Add(new GradientStop(Colors.Transparent, 0));
            blackGradient.GradientStops.Add(new GradientStop(Colors.Black, 1));
            SpectrumBlackBrush = blackGradient;

            SystemColors = new[]
            {
                Color.FromRgb(255, 255, 255),
                Color.FromRgb(255, 251, 13),
                Color.FromRgb(5, 50, 255),
                Color.FromRgb(255, 147, 0),
                Color.FromRgb(0, 249, 26),
                Color.FromRgb(255, 39, 0),
                Color.FromRgb(57, 0, 153),
                Color.FromRgb(10, 77, 104),
                Color.FromRgb(0, 0, 0),
                Color.FromRgb(104, 104, 104),
                Color.FromRgb(238, 84, 100),
                Color.FromRgb(210, 122, 238),
                Color.FromRgb(91, 168, 196),
                Color.FromRgb(230, 74, 169),
                Color.FromRgb(60, 72, 75),
                Color.FromRgb(200, 198, 167),
            };
        }
    
        /// <inheritdoc cref="ValueProperty"/>
        public Color Value 
        {
            get => (Color) GetValue(ValueProperty); 
            set => SetValue(ValueProperty, value); 
        }

        /// <summary>
        /// Selected color
        /// </summary>
        public readonly static DependencyProperty ValueProperty = 
            DependencyProperty.Register(
                nameof(Value),
                typeof(Color),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(Color.FromRgb(78, 27, 255), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HandleColorUpdate));

        private static void HandleColorUpdate(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var colorPicker = (ColorPicker)obj;
            if (!colorPicker.IsColorSelectionStarted)
            {
                colorPicker.UpdateCursorsStates();
            }
        }

        /// <inheritdoc cref="SystemColorPaletteProperty"/>
        public Color[] SystemColorPalette
        {
            get => (Color[])GetValue(SystemColorPaletteProperty);
            private set => SetValue(SystemColorPaletteProperty, value);
        }

        /// <summary>
        /// Palette of system color.
        /// </summary>
        public readonly static DependencyProperty SystemColorPaletteProperty =
            DependencyProperty.Register(
                nameof(SystemColorPalette),
                typeof(Color[]),
                typeof(ColorPicker),
                new PropertyMetadata());

        /// <inheritdoc cref="IsOpenProperty"/>
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        /// <summary>
        /// Indicates whether the color picker is open.
        /// </summary>
        public readonly static DependencyProperty IsOpenProperty =
            DependencyProperty.Register(
                nameof(IsOpen),
                typeof(bool),
                typeof(ColorPicker),
                new PropertyMetadata());

        /// <inhertidoc cref="SelectColorCommandProperty/>
        public RelayCommand<Color> SelectColorCommand 
        { 
            get => (RelayCommand<Color>)GetValue(SelectColorCommandProperty); 
            set => SetValue(SelectColorCommandProperty, value); 
        }

        /// <summary>
        /// Command to select color from palette.
        /// </summary>
        public readonly static DependencyProperty SelectColorCommandProperty =
            DependencyProperty.Register(
                nameof(SelectColorCommand),
                typeof(RelayCommand<Color>),
                typeof(ColorPicker),
                new PropertyMetadata());

        /// <summary>
        /// Indicates whether the user selects a color with the canvases.
        /// </summary>
        public bool IsColorSelectionStarted { get; private set; }

        private float hue;
        private Color hueColor;
        private Point? huePosition;
        private Ellipse hueCursor;

        private Point? spectrumPosition;
        private Ellipse spectrumCursor;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ColorPicker()
        {
            InitializeComponent();
            
            SystemColorPalette = SystemColors;
            SelectColorCommand = new RelayCommand<Color>(SelectColor);

            InitializeSpectrums();
        }

        private void SelectColor(Color color)
        {
            Value = color;
        }

        private void InitializeSpectrums()
        {
            InitializeHueCanvas();
            InitializeSpectrumCanvas();
        }

        private void UpdateCursorsStates()
        {
            var hsl = HSLColor.FromRgbColor(Value);
            hue = hsl.Hue;
            hueColor = new HSLColor(hsl.Hue, 1, .5f).ToColor();
            
            var hueYPosition = HueCanvas.Height - (hsl.Hue / 360) * HueCanvas.Height;
            huePosition = new Point(0, hueYPosition);

            var hsv = HSVColor.FromColor(Value);
            var spectrumXPosition = SpectrumCanvas.Width * hsl.Saturation;
            var spectrumYPosition = SpectrumCanvas.Height * (1 - hsv.Value);
            spectrumPosition = new Point(spectrumXPosition, spectrumYPosition);

            RedrawHueCanvas();
            RedrawSpectrumCanvas();
        }

        private void InitializeSpectrumCanvas()
        {
            SpectrumCanvas.MouseDown += StartColorSelection;

            SpectrumCanvas.MouseLeave += EndColorSelection;
            SpectrumCanvas.MouseUp += EndColorSelection;

            spectrumCursor = new Ellipse();
            spectrumCursor.Width = 25;
            spectrumCursor.Height = 25;
            spectrumCursor.Stroke = new SolidColorBrush(Colors.White);
            spectrumCursor.StrokeThickness = 2;
            spectrumCursor.Fill = new SolidColorBrush(Value);
            spectrumCursor.MouseUp += EndColorSelection;

            RedrawSpectrumCanvas();
        }

        private void StartColorSelection(object sender, MouseButtonEventArgs mouseEventArgs)
        {
            SpectrumCanvas.MouseMove += HandleSpectrumCanvasMouseMove;
            IsColorSelectionStarted = true;
        }

        private void EndColorSelection(object sender, MouseEventArgs e)
        {
            SpectrumCanvas.MouseMove -= HandleSpectrumCanvasMouseMove;
            IsColorSelectionStarted = false;
        }

        private void HandleSpectrumCanvasMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var position = SpectrumCanvas.GetPosition(mouseEventArgs);
            spectrumPosition = position;

            SelectSpectrumColor();
            RedrawSpectrumCanvas();
        }

        private void SelectSpectrumColor()
        {
            if (spectrumPosition == null)
            {
                return;
            }

            var xRatio = spectrumPosition.Value.X / SpectrumCanvas.ActualWidth;
            var yRatio = spectrumPosition.Value.Y / SpectrumCanvas.ActualHeight;

            var hsvValue = 1 - yRatio;
            var hsvSaturation = xRatio;

            var lightness = (hsvValue / 2) * (2 - hsvSaturation);
            var saturation = (hsvValue * hsvSaturation) / (1 - Math.Abs(2 * lightness - 1));

            Value = new HSLColor(hue, (float)saturation, (float)lightness).ToColor();
        }

        private void RedrawSpectrumCanvas()
        {
            SpectrumCanvas.Children.Clear();

            var hueBrush = new SolidColorBrush(hueColor);

            PaintCanvas(hueBrush);
            PaintCanvas(SpectrumWhiteBrush);
            PaintCanvas(SpectrumBlackBrush);

            spectrumCursor.Fill = new SolidColorBrush(Value);

            if (spectrumPosition != null)
            {
                var leftOffset = spectrumPosition.Value.X - spectrumCursor.Width / 2;
                var topOffset = spectrumPosition.Value.Y - spectrumCursor.Height / 2;

                Canvas.SetLeft(spectrumCursor, leftOffset);
                Canvas.SetTop(spectrumCursor, topOffset);
            }

            SpectrumCanvas.Children.Add(spectrumCursor);

            void PaintCanvas(Brush brush)
            {
                var rectangle = new Rectangle();
                rectangle.Fill = brush;
                rectangle.Width = SpectrumCanvas.Width;
                rectangle.Height = SpectrumCanvas.Height;

                SpectrumCanvas.Children.Add(rectangle);
            }
        }

        private void InitializeHueCanvas()
        {
            HueCanvas.MouseDown += StartHueColorSelection;
            
            HueCanvas.MouseUp += EndHueColorSelection;
            HueCanvas.MouseLeave += EndHueColorSelection;

            hueCursor = new Ellipse();
            hueCursor.Width = 18;
            hueCursor.Height = 18;
            hueCursor.Stroke = new SolidColorBrush(Colors.White);
            hueCursor.StrokeThickness = 2;
            hueCursor.MouseUp += EndHueColorSelection;
            HueCanvas.AlignHorizontalCenter(hueCursor);

            RedrawHueCanvas();
        }

        private void StartHueColorSelection(object sender, MouseButtonEventArgs mouseEventArgs)
        {
            HueCanvas.MouseMove += HandleHueCanvasMouseMove;
            IsColorSelectionStarted = true;

            var position = HueCanvas.GetPosition(mouseEventArgs);
            huePosition = position; 
            SelectHueColor();
        }

        private void EndHueColorSelection(object sender, MouseEventArgs args)
        {
            IsColorSelectionStarted = false;
            HueCanvas.MouseMove -= HandleHueCanvasMouseMove;
        }

        private void HandleHueCanvasMouseMove(object sender, MouseEventArgs args)
        {
            var position = HueCanvas.GetPosition(args);
            huePosition = position;
            SelectHueColor();
        }

        private void SelectHueColor()
        {
            if (huePosition == null)
            {
                return;
            }

            var yRatio = (float)(huePosition.Value.Y / HueCanvas.ActualHeight);
            hue = 360 - 360 * yRatio;
            hueColor = new HSLColor(hue, 1, .5f).ToColor();

            SelectSpectrumColor();
            RedrawSpectrumCanvas();
            RedrawHueCanvas();
        }
        
        private void RedrawHueCanvas()
        {
            HueCanvas.Children.Clear();

            var spectrumRectangle = new Rectangle();
            spectrumRectangle.RadiusX = 5;
            spectrumRectangle.RadiusY = 5;
            spectrumRectangle.Width = 10;
            spectrumRectangle.Height = HueCanvas.Height;
            spectrumRectangle.Fill = HueSpectrumBrush;
            HueCanvas.AlignHorizontalCenter(spectrumRectangle);
            HueCanvas.Children.Add(spectrumRectangle);

            hueCursor.Fill = new SolidColorBrush(hueColor);

            if (huePosition != null)
            {
                var topOffset = huePosition.Value.Y - hueCursor.Height / 2;
                Canvas.SetTop(hueCursor, topOffset);
            }

            HueCanvas.Children.Add(hueCursor);
        }
    }
}
