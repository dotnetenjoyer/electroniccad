using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using ElectronicCad.Desktop.Styles.Utils;
using ElectronicCad.Desktop.Styles.Converters;
using System.Xml.Linq;
using System.Diagnostics.Eventing.Reader;

namespace ElectronicCad.Desktop.UI.Components
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        private static readonly Brush HueSpectrumBrush;

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ColorPicker()
        {
            var hueSpectrumBrush = new LinearGradientBrush();

            hueSpectrumBrush.GradientStops.Add(new GradientStop(ColorUtils.FromHSL(0, 1, .5f), 0.00));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(ColorUtils.FromHSL(298.8f, 1, .5f), 0.17));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(ColorUtils.FromHSL(241.2f, 1, .5f), 0.33));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(ColorUtils.FromHSL(180f, 1, .5f), 0.5));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(ColorUtils.FromHSL(118.8f, 1, .5f), 0.67));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(ColorUtils.FromHSL(61.2f, 1, .5f), 0.83));
            hueSpectrumBrush.GradientStops.Add(new GradientStop(ColorUtils.FromHSL(360, 1, .5f), 1));

            HueSpectrumBrush = hueSpectrumBrush;
        }

        private Color HueColor;
        
        private Ellipse hueCursor;


        private Window window;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ColorPicker()
        {
            HueColor = Colors.Red;
            Loaded += HandleColorPickerLoad;

            InitializeComponent();
        }

        private void HandleColorPickerLoad(object sender, RoutedEventArgs e)
        {
            InitializeSpectrumCanvas();
            InitializeHueCanvas();
        }

        private void InitializeSpectrumCanvas()
        {
            UpdateSpectrumCanvas();
        }

        private void UpdateSpectrumCanvas()
        {
            SpectrumCanvas.Children.Clear();

            var whiteGradient = new LinearGradientBrush();
            whiteGradient.StartPoint = new Point(0, 0);
            whiteGradient.EndPoint = new Point(1, 0);
            whiteGradient.GradientStops.Add(new GradientStop(Colors.White, 0));
            whiteGradient.GradientStops.Add(new GradientStop(Colors.Transparent, 1));

            var blackGradient = new LinearGradientBrush();
            blackGradient.StartPoint = new Point(0, 0);
            blackGradient.EndPoint = new Point(0, 1); 
            blackGradient.GradientStops.Add(new GradientStop(Colors.Transparent, 0));
            blackGradient.GradientStops.Add(new GradientStop(Colors.Black, 1));

            var hueBrush = new SolidColorBrush(HueColor);

            PaintCanvas(hueBrush);
            PaintCanvas(whiteGradient);
            PaintCanvas(blackGradient);

            void PaintCanvas(Brush brush)
            {
                var rectangle = new Rectangle();
                rectangle.Fill = brush;
                rectangle.Width = 150;
                rectangle.Height = 150;

                SpectrumCanvas.Children.Add(rectangle);
            }
        }

        private void InitializeHueCanvas()
        {
            HueCanvas.MouseDown += StartHueColorSelection;
            HueCanvas.MouseLeave += EndHueColorSelection;

            UpdateHueCanvas();
        }

        private void UpdateHueCanvas()
        {
            HueCanvas.Children.Clear();

            var spectrumRectangle = new Rectangle();
            spectrumRectangle.RadiusX = 5;
            spectrumRectangle.RadiusY = 5;
            spectrumRectangle.Width = 10;
            spectrumRectangle.Height = HueCanvas.ActualHeight;
            spectrumRectangle.Fill = HueSpectrumBrush;
            HueCanvas.AlignHorizontalCenter(spectrumRectangle);
            HueCanvas.Children.Add(spectrumRectangle);

            hueCursor = new Ellipse();
            hueCursor.Width = 18;
            hueCursor.Height = 18;
            hueCursor.Stroke = new SolidColorBrush(Colors.White);
            hueCursor.StrokeThickness = 2;
            hueCursor.Fill = new SolidColorBrush(HueColor);
            HueCanvas.AlignHorizontalCenter(hueCursor);
            HueCanvas.Children.Add(hueCursor);
        }

        private void StartHueColorSelection(object sender, MouseButtonEventArgs args)
        {
            HueCanvas.MouseMove += HandleHueCanvasMouseMove;

            var position = HueCanvas.GetPosition(args);
            UpdateHueColor(position);
        }

        private void EndHueColorSelection(object sender, MouseEventArgs args)
        {
            HueCanvas.MouseMove -= HandleHueCanvasMouseMove;
        }

        private void HandleHueCanvasMouseMove(object sender, MouseEventArgs args)
        {
            var position = HueCanvas.GetPosition(args);
            UpdateHueColor(position);
        }

        private void UpdateHueColor(Point hueCanvasPosition)
        {
            var percent = hueCanvasPosition.Y / HueCanvas.ActualHeight;
            int hue = (int)(360 - 360 * percent);

            HueColor = ColorUtils.FromHSL(hue, 1, .5f);
            UpdateSpectrumCanvas();
            UpdateHueCanvas();
         
            Canvas.SetTop(hueCursor, hueCanvasPosition.Y);
        }
    }
}
