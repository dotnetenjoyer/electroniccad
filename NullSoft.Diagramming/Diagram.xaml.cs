using System;
using System.Collections.Specialized;
using System.Windows.Controls;
using NullSoft.Diagramming.Nodes;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace NullSoft.Diagramming
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Diagram : UserControl, IDisposable
    {
        public DiagramNodes Nodes { get; } = new();
        
        public Diagram()
        {
            InitializeComponent();
            Nodes.Add(new LineNode());
            Nodes.Add(new Circle());
            Nodes.CollectionChanged += NodesChanged;
            SkiaCanvas.PaintSurface += SkElementOnPaintSurface;
        }

        private void NodesChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            SkiaCanvas.InvalidateVisual();
        }

        private void SkElementOnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            Draw(canvas);
        }

        private void Draw(SKCanvas canvas)
        {
            foreach (var node in Nodes)
            {
                node.Draw(canvas);
            }
        }

        public void Dispose()
        {
            Nodes.CollectionChanged -= NodesChanged;
            SkiaCanvas.PaintSurface -= SkElementOnPaintSurface;
        }
    }
}