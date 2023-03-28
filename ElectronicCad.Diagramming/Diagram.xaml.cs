using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ElectronicCad.Diagramming.Modes;
using ElectronicCad.Diagramming.Nodes;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using Colors = ElectronicCad.Diagramming.Utils.Colors;

using DomainDiagram = ElectronicCad.Domain.Geometry.Diagram;

namespace ElectronicCad.Diagramming
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Diagram : UserControl, IDisposable
    {
        #region Layers
        
        ///// <summary>
        ///// Diagram layers.
        ///// </summary>
        //public IEnumerable<Layer> Layers => _layers;

        //private readonly List<Layer> _layers = new();
        
        ///// <summary>
        ///// Active diagram layer.
        ///// </summary>
        //public Layer ActiveLayer { get; private set; }
        
        ///// <summary>
        ///// Add a new diagram layer.
        ///// </summary>
        ///// <returns>Created layer.</returns>
        //public Layer AddLayer()
        //{
        //    int layerIndex = _layers.Any() ? _layers.Max(layer => layer.Index) + 1 : 0;
        //    var layer = new Layer(layerIndex);
        //    layer.ItemsChanged += HandleItemsChanged;
        //    _layers!.Add(layer);
        //    ActiveLayer = layer;
        //    return layer;
        //}

        ///// <summary>
        ///// Remove specified layer.
        ///// </summary>
        ///// <param name="layer">Layer to remove.</param>
        //public void RemoveLayer(Layer layer)
        //{
        //    layer.ItemsChanged -= HandleItemsChanged;
        //    _layers.Remove(layer);
        //}
        
        #endregion
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Diagram()
        {
            InitializeComponent();
            Colors.Initialize(this);

            SkiaCanvas.PaintSurface += SkElementOnPaintSurface;

            DomainDiagram = new DomainDiagram();
            DomainDiagram.VersionChanged += HandleDiagramVersionChanged;

            SetDiagramMode(DiagramMode.Selection);
        }

        #region DomainDiagram

        public DomainDiagram DomainDiagram { get; private set; }
       
        private void HandleDiagramVersionChanged(object? sender, EventArgs eventArgs)
        {
            Redraw();
        }

        #endregion

        #region Diagram mode

        private IDiagramMode _diagramMode;

        public void SetDiagramMode(DiagramMode diagramMode)
        {
            switch (diagramMode)
            {
                case DiagramMode.Selection:
                    SetDiagramMode(new Modes.SelectionMode());
                    break;
                case DiagramMode.Line:
                    SetDiagramMode(new NewLineMode());
                    break;
            }
        }

        private void SetDiagramMode(IDiagramMode diagramMode)
        {
            _diagramMode?.Finalize();
            _diagramMode = diagramMode;
            diagramMode.Initialize(this);
        }
        
        #endregion

        ///// <summary>
        ///// All diagram items
        ///// </summary>
        //public IEnumerable<DiagramItem> DiagramItems => Layers.SelectMany(_ => _.DiagramItems);

        ///// <summary>
        ///// Add diagram item to active layer.
        ///// </summary>
        ///// <param name="item">Diagram item to add.</param>
        //public void AddItem(DiagramItem item)
        //{
        //    ActiveLayer.AddItem(item);
        //}

        #region Drawing

        public void Redraw()
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
            canvas.Clear();

            DrawWorkspaceArea(canvas);
            
            //foreach (var layer in _layers)
            //{
            //    foreach (var item in layer.DiagramItems)
            //    {
            //        if (!item.IsVisible)
            //        {
            //            continue;
            //        }
                    
            //        item.Draw(canvas);
            //    }
            //}
        }

        private void DrawWorkspaceArea(SKCanvas canvas)
        {
            var size = new SKSize(850, 600);
            var left = canvas.LocalClipBounds.MidX - size.Width / 2;
            var top = canvas.LocalClipBounds.MidY - size.Height / 2;
            var right = left + size.Width;
            var bottom = top + size.Height;
            var rectangle = new SKRect(left, top, right, bottom);
            
            var paint = new SKPaint { Color = Colors.SecondaryBackground };
            canvas.DrawRect(rectangle, paint);
        }

        #endregion

        /// <inheritdoc/>
        public void Dispose()
        {
            DomainDiagram.VersionChanged -= HandleDiagramVersionChanged;
        }
    }
}