using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using NullSoft.Diagramming.Behaviour;
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
        private readonly List<Layer> _layers = new();

        private IDiagramBehaviour _currentDiagramBehaviour;
        
        public IEnumerable<DiagramNode> Nodes => _layers.SelectMany(layer => layer.Nodes);
        
        public Diagram()
        {
            InitializeComponent();

            Layer layer = CreateLayer();
            
            layer.AddNode(new LineDiagramNode
            {
                Bounds = new SKRect(300, 300, 500, 500),
            });
            
            layer.AddNode(new EllipseDiagramNode
            {
                Bounds = new SKRect(100, 100, 250, 200)
            });
            
            layer.AddNode(new PolygonDiagramNode()
            {
                Bounds = new SKRect(0, 0, 100, 100)
            });
            
            SkiaCanvas.PaintSurface += SkElementOnPaintSurface;
        }

        /// <summary>
        /// Method that allow ddd new layer.
        /// </summary>
        /// <returns>New layer.</returns>
        public Layer CreateLayer()
        {
            int maxLayerIndex = _layers.Count == 0 ? 0 : _layers.Max(layer => layer.Index);
            Layer layer = new(maxLayerIndex + 1);
            layer.NodesChange += HandleNodesChange;
            _layers.Add(layer);
            return layer;
        }

        /// <summary>
        /// Method that allow remove layer.
        /// </summary>
        /// <param name="layer"></param>
        public void RemoveLayer(Layer layer)
        {
            _layers.Remove(layer);
            layer.NodesChange -= HandleNodesChange;
        }
        
        private void HandleNodesChange(object? sender, EventArgs e)
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
            foreach (var layer in _layers.OrderBy(layer => layer.Index))
            {
                foreach (var node in layer.Nodes.OrderBy(x => x.ZIndex))
                {
                    node.Draw(canvas);
                }
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _layers.ForEach(layer =>
            {
                layer.NodesChange -= HandleNodesChange;
                layer.Dispose();
            });
        }
    }
}