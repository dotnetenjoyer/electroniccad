using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Diagramming.Modes;
using ElectronicCad.Diagramming.Items;
using Colors = ElectronicCad.Diagramming.Utils.Colors;
using DomainDiagram = ElectronicCad.Domain.Geometry.Diagram;
using ElectronicCad.Domain.Geometry;
using System.Linq;
using System.Windows;

using WindowsPoint = System.Windows.Point;
using System.Windows.Input;
using SkiaSharp.Views.WPF;

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
            DomainDiagram.GeometryAdded += HandleDiagramGeometryAdded;
            DomainDiagram.GeometryModified += HandleGeometryModified;
            DomainDiagram.GeometryRemoved += HandleDiagramGeometryRemoved;

            SetDiagramMode(DiagramMode.Selection);

            MouseMove += HandleDiagramMouseMove;
            MouseUp += HandleDiagramMouseUp;
            MouseDown += HandleDiagramMouseDown;
        }

        #region DomainDiagram

        public DomainDiagram DomainDiagram { get; private set; }
       
        private void HandleGeometryModified(object? sender, IEnumerable<GeometryObject> modifiedGeometryObjects)
        {
            var modifiedItems = diagramItems
                .OfType<GeometryObjectDiagramItem>()
                .Where(item => modifiedGeometryObjects.Contains(item.GeometryObject))
                .ToList();

            foreach (var item in modifiedItems)
            {
                item.UpdateViewState();
            }
            
            Redraw();
        }

        private void HandleDiagramGeometryAdded(object? sender, GeometryObject geometryObject)
        {
            var diagramItem = DiagramItemsFactory.Create(geometryObject);
            diagramItems.Add(diagramItem);
            Redraw();
        }

        private void HandleDiagramGeometryRemoved(object? sender, GeometryObject geometryObject)
        {
            var diagramItem = diagramItems
                .OfType<GeometryObjectDiagramItem>()
                .FirstOrDefault(item => item.GeometryObject == geometryObject);

            if(diagramItem != null)
            {
                diagramItems.Remove(diagramItem);
                Redraw();
            }
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

        internal WindowsPoint Position
        {
            get => (WindowsPoint)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        private static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(nameof(Position), typeof(WindowsPoint),
                typeof(Diagram), new PropertyMetadata());

        internal DiagramItem? FocusedItem { get; private set; }

        private void HandleDiagramMouseMove(object sender, MouseEventArgs eventArgs)
        {
            Position = eventArgs.GetPosition(this);
            var skiaPoint = Position.ToSKPoint();
            
            if (TryHitItem(ref skiaPoint, out var hitItem))
            {
                FocusedItem = hitItem;
                FocusedItem!.HandleMouseMove(skiaPoint);
            }
            else
            {   
                FocusedItem = null;
            }
        }

        private bool TryHitItem(ref SKPoint point, out DiagramItem? hitItem)
        {
            hitItem = null;
            var result = false;

            foreach(var item in DiagramItems)
            {
                if(item.CheckHit(ref point))
                {
                    hitItem = item;
                    result = true;
                    break;
                }
            }

            return result;
        }

        private void HandleDiagramMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(FocusedItem != null)
            {
                FocusedItem.HandleMouseDown(Position.ToSKPoint());
            }
        }

        private void HandleDiagramMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (FocusedItem != null)
            {
                FocusedItem.HandleMouseUp(Position.ToSKPoint());
            }
        }

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

        /// <summary>
        /// Collection of all diagram item on the diagram.
        /// </summary>
        internal IEnumerable<DiagramItem> DiagramItems => diagramItems;

        private List<DiagramItem> diagramItems = new();

        internal void AddDiagramItem(DiagramItem diagramItem)
        {
            diagramItems.Add(diagramItem);
            diagramItems = diagramItems
                .OrderByDescending(item => item.ZIndex)
                .ToList();
        }

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

            foreach(var item in diagramItems)
            {
                item.Draw(canvas);
            }
            
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
            DomainDiagram.GeometryAdded -= HandleDiagramGeometryAdded;
            DomainDiagram.GeometryModified -= HandleGeometryModified;
            DomainDiagram.GeometryRemoved -= HandleDiagramGeometryRemoved;

            MouseMove -= HandleDiagramMouseMove;
            MouseUp -= HandleDiagramMouseUp;
            MouseDown -= HandleDiagramMouseDown;
        }
    }
}