using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ElectronicCad.Diagramming.Modes;
using ElectronicCad.Diagramming.Nodes;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SelectionMode = ElectronicCad.Diagramming.Modes.SelectionMode;

namespace ElectronicCad.Diagramming
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Diagram : UserControl, IDisposable
    {
        public Diagram()
        {
            InitializeComponent();

            CreateLayer();
            SetDiagramMode(new NewLineMode());

            SkiaCanvas.PaintSurface += SkElementOnPaintSurface;
        }

        #region Diagram mode

        private IDiagramMode _diagramMode;

        private void SetDiagramMode(IDiagramMode diagramMode)
        {
            if (_diagramMode != null)
            {
                _diagramMode.Finalize();
            }
            
            _diagramMode = diagramMode;
            diagramMode.Initialize(this);
        }
        
        #endregion
        
        #region Layers
        
        /// <summary>
        /// Diagram layers.
        /// </summary>
        public IEnumerable<Layer> Layers => _layers;
        
        /// <summary>
        /// All diagram nodes.
        /// </summary>
        public IEnumerable<DiagramNode> AllNodes => _layers.SelectMany(layer => layer.Nodes);

        /// <summary>
        /// Active diagram layer.
        /// </summary>
        public Layer ActiveLayer => _activeLayer;
        
        private Layer _activeLayer;
        
        private readonly List<Layer> _layers = new();
        
        /// <summary>
        /// Method to create new diagram layer.
        /// </summary>
        /// <returns>Created layer.</returns>
        public Layer CreateLayer()
        {
            int maxLayerIndex = _layers.Count == 0 
                ? 0 
                : _layers.Max(layer => layer.Index);
            
            var layer = new Layer(maxLayerIndex + 1);
            layer.NodesChange += HandleNodesChange;
            _layers.Add(layer);
            _activeLayer = layer;
            
            return layer;
        }

        /// <summary>
        /// Method to remove layer.
        /// </summary>
        /// <param name="layer">Layer to remove.</param>
        public void RemoveLayer(Layer layer)
        {
            _layers.Remove(layer);
            layer.NodesChange -= HandleNodesChange;
        }
        
        #endregion

        public void RedrawDiagram()
        {
            SkiaCanvas.InvalidateVisual();
        }

        private void HandleNodesChange(object? sender, EventArgs e)
        {
            RedrawDiagram();
        }

        private void SkElementOnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            Draw(canvas);
        }

        private void Draw(SKCanvas canvas)
        {
            canvas.Clear();
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

        private void SetSelectionMode(object sender, RoutedEventArgs e)
        {
            SetDiagramMode(new Modes.SelectionMode());
        }

        private void SetNewLineMode(object sender, RoutedEventArgs e)
        {
            SetDiagramMode(new NewLineMode());
        }
    }
}