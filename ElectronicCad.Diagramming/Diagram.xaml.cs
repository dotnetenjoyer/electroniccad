using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Diagramming.Modes;
using ElectronicCad.Diagramming.Items;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Diagramming.Extensions;
using Colors = ElectronicCad.Diagramming.Utils.Colors;
using DomainDiagram = ElectronicCad.Domain.Geometry.Diagram;

namespace ElectronicCad.Diagramming
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Diagram : UserControl, IDisposable
    {
        /// <summary>
        /// Related domain diagram.
        /// </summary>
        public DomainDiagram DomainDiagram
        {
            get => (DomainDiagram)GetValue(DomainDiagramProperty);
            set => SetValue(DomainDiagramProperty, value);
        }

        private static readonly DependencyProperty DomainDiagramProperty = DependencyProperty
            .Register(nameof(DomainDiagram),
                typeof(DomainDiagram),
                typeof(Diagram),
                new PropertyMetadata(DomainDiagramChanged));

        private static void DomainDiagramChanged(DependencyObject obj, DependencyPropertyChangedEventArgs eventArgs)
        {
            var diagram = (Diagram)obj;
            diagram.DeinitializeDomainDiagram();
            diagram.InitializeDomainDiagram((DomainDiagram)eventArgs.NewValue);
        }

        private void InitializeDomainDiagram(DomainDiagram domainDiagram)
        {
            DomainDiagram = domainDiagram;
            DomainDiagram.GeometryAdded += HandleDiagramGeometryAdded;
            DomainDiagram.GeometryModified += HandleGeometryModified;
            DomainDiagram.GeometryRemoved += HandleDiagramGeometryRemoved;

            CalculateDeltas();
        }

        private void DeinitializeDomainDiagram()
        {
            if(DomainDiagram == null)
            {
                return;
            }

            DomainDiagram.GeometryAdded += HandleDiagramGeometryAdded;
            DomainDiagram.GeometryModified += HandleGeometryModified;
            DomainDiagram.GeometryRemoved += HandleDiagramGeometryRemoved;
        }

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

            MouseMove += HandleDiagramMouseMove;
            MouseUp += HandleDiagramMouseUp;
            MouseDown += HandleDiagramMouseDown;
            
            SetDiagramMode(DiagramMode.Selection);
        }

        #region DomainDiagram
       
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

        private DiagramMode currentMode;
        
        private IDiagramMode diagramMode;

        public void SetDiagramMode(DiagramMode diagramMode)
        {
            switch (diagramMode)
            {
                case DiagramMode.Selection:
                    SetDiagramMode(new Modes.SelectionMode());
                    break;

                case DiagramMode.NewLine:
                    SetDiagramMode(new NewLineMode());
                    break;

                case DiagramMode.NewEllipse:
                    SetDiagramMode(new NewEllipseMode());
                    break;

                case DiagramMode.NewPolygon:
                    SetDiagramMode(new NewPolygonMode());
                    break;

                case DiagramMode.NewText:
                    SetDiagramMode(new NewTextMode());
                    break;
            }

            currentMode = diagramMode;
        }

        private void SetDiagramMode(IDiagramMode diagramMode)
        {
            if(this.diagramMode != null)
            {
                this.diagramMode.Finalize();
            }
         
            this.diagramMode = diagramMode;
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

        /// <summary>
        /// Current canvas mouse position.
        /// </summary>
        internal SKPoint Position
        {
            get => (SKPoint)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        private static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(nameof(Position), typeof(SKPoint),
                typeof(Diagram), new PropertyMetadata());

        /// <summary>
        /// Focused diagram item.
        /// </summary>
        internal DiagramItem? FocusItem { get; private set; }

        /// <summary>
        /// Diagram delta x.
        /// </summary>
        internal float DeltaX { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal float DeltaY { get; private  set; }

        /// <summary>
        /// Calculates canvas position.
        /// </summary>
        /// <param name="eventArgs">Mouse event args.</param>
        /// <returns>Canvas mouse position.</returns>
        internal SKPoint GetPosition(MouseEventArgs eventArgs)
        {
            var position = eventArgs.GetPosition(this).ToSKPoint();
            position.Offset(-DeltaX, -DeltaY);
            return position;
        }

        private void HandleDiagramMouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (FocusItem != null)
            {
                var mouse = new MouseParameters
                {
                    LeftButton = (MouseButtonState)eventArgs.LeftButton,
                    RightButton = (MouseButtonState)eventArgs.RightButton,
                    RelativePosition = Position - FocusItem.BoundingBox.GetTopLeft(),
                    Position = Position
                };

                FocusItem.HandleMouseDown(mouse);
            }
        }

        private void HandleDiagramMouseUp(object sender, MouseButtonEventArgs eventArgs)
        {
            if (FocusItem != null)
            {
                var mouse = new MouseParameters
                {
                    LeftButton = (MouseButtonState)eventArgs.LeftButton,
                    RightButton = (MouseButtonState)eventArgs.RightButton,
                    RelativePosition = Position - FocusItem.BoundingBox.GetTopLeft(),
                    Position = Position
                };

                FocusItem.HandleMouseUp(mouse);
            }
        }

        private void HandleDiagramMouseMove(object sender, MouseEventArgs eventArgs)
        {
            var position = GetPosition(eventArgs);
            var delta = position - Position;
            Position = position;

            if (TryHitItem(ref position, out var hitItem))
            {
                FocusItem = hitItem;

                var mouse = new MovingMouseParameters
                {
                    LeftButton = (MouseButtonState)eventArgs.LeftButton,
                    RightButton = (MouseButtonState)eventArgs.RightButton,
                    RelativePosition = position - hitItem!.BoundingBox.GetTopLeft(),
                    Position = position,
                    Delta = delta
                };

                FocusItem!.HandleMouseMove(mouse);
            }
            else
            {
                FocusItem = null;

                if (currentMode == DiagramMode.Selection && eventArgs.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    DeltaX += delta.X;
                    DeltaY += delta.Y;
                    Position = GetPosition(eventArgs);
                    
                    Redraw();
                }
            }
        }

        private bool TryHitItem(ref SKPoint point, out DiagramItem? hitItem)
        {
            foreach (var item in DiagramItems.Where(item => item.IsVisible))
            {
                if (item.CheckHit(ref point))
                {
                    hitItem = item;
                    return true;
                }
            }

            hitItem = null;
            return false;
        }

        private void CalculateDeltas()
        {
            DeltaX = ((float)SkiaCanvas.ActualWidth - DomainDiagram.Width) / 2;
            DeltaY = ((float)SkiaCanvas.ActualHeight - DomainDiagram.Height) / 2;
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
            if (DomainDiagram == null)
            {
                return;
            }

            canvas.Clear();
            canvas.Translate((float)DeltaX, (float)DeltaY);

            DrawWorkspaceArea(canvas);

            var sortedDiagramItems = diagramItems
                .Where(item => item.IsVisible)
                .OrderBy(item => item.ZIndex)
                .ToList();

            foreach (var item in sortedDiagramItems)
            {
                item.Draw(canvas);
            }
        }

        private void DrawWorkspaceArea(SKCanvas canvas)
        {
            var workspaceArea = new SKRect(0, 0, DomainDiagram.Width, DomainDiagram.Height);
            var paint = new SKPaint { Color = Colors.SecondaryBackground };
            canvas.DrawRect(workspaceArea, paint);
        }

        #endregion

        /// <inheritdoc/>
        public void Dispose()
        {
            SkiaCanvas.PaintSurface -= SkElementOnPaintSurface;

            MouseMove -= HandleDiagramMouseMove;
            MouseUp -= HandleDiagramMouseUp;
            MouseDown -= HandleDiagramMouseDown;

            DeinitializeDomainDiagram();
        }
    }
}