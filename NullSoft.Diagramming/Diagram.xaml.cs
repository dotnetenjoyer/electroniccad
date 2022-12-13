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
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace NullSoft.Diagramming
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Diagram : UserControl
    {
        private List<object> _items;
        
        
        public Diagram()
        {
            InitializeComponent();
            
            SkiaCanvas.PaintSurface += SkElementOnPaintSurface;
        }

        private void SkElementOnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            
            var paint = new SKPaint()
            {
                Color = SKColors.Red,
                Style = SKPaintStyle.Fill
            };
            
            canvas.
        }
    }
}