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
        #region Layers
        
        /// <summary>
        /// Diagram layers.
        /// </summary>
        public IEnumerable<Layer> Layers => _layers;

        private readonly List<Layer> _layers = new();
        
        /// <summary>
        /// Active diagram layer.
        /// </summary>
        public Layer ActiveLayer { get; private set; }
        
        /// <summary>
        /// Add a new diagram layer.
        /// </summary>
        /// <returns>Created layer.</returns>
        public Layer AddLayer()
        {
            int layerIndex = _layers.Any() ? _layers.Max(layer => layer.Index) + 1 : 0;
            var layer = new Layer(layerIndex);
            layer.ItemsChanged += HandleItemsChanged;
            _layers!.Add(layer);
            ActiveLayer = layer;
            return layer;
        }

        /// <summary>
        /// Remove specified layer.
        /// </summary>
        /// <param name="layer">Layer to remove.</param>
        public void RemoveLayer(Layer layer)
        {
            layer.ItemsChanged -= HandleItemsChanged;
            _layers.Remove(layer);
        }
        
        #endregion
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Diagram()
        {
            InitializeComponent();

            AddLayer();

            var line = new LineDiagramItem();
            line.Bounds = new SKRect(0, 0, 300, 300);
            AddItem(line);

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

        /// <summary>
        /// Add diagram item to active layer.
        /// </summary>
        /// <param name="item">Diagram item to add.</param>
        public void AddItem(DiagramItem item)
        {
            ActiveLayer.AddItem(item);
        }
        
        public void RedrawDiagram()
        {
            SkiaCanvas.InvalidateVisual();
        }

        private void HandleItemsChanged(object? sender, EventArgs e)
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
            
            foreach (var layer in _layers)
            {
                foreach (var item in layer.DiagramItems)
                {
                    item.Draw(canvas);
                }
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _layers.ForEach(layer =>
            {
                layer.ItemsChanged -= HandleItemsChanged;
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