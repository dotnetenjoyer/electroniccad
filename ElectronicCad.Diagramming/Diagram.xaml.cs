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
            DeinitializeDomainDiagram();

            DomainDiagram = domainDiagram;
            DomainDiagram.GeometryAdded += HandleDiagramGeometryAdded;
            DomainDiagram.GeometryModified += HandleGeometryModified;
            DomainDiagram.GeometryRemoved += HandleDiagramGeometryRemoved;

            CalculateDeltas();
        }
       
        private void DeinitializeDomainDiagram()
        {
            if (DomainDiagram == null)
            {
                return;
            }

            DomainDiagram.GeometryAdded -= HandleDiagramGeometryAdded;
            DomainDiagram.GeometryModified -= HandleGeometryModified;
            DomainDiagram.GeometryRemoved -= HandleDiagramGeometryRemoved;
        }

        private void CalculateDeltas()
        {
            DeltaX = ((float)SkiaCanvas.ActualWidth - DomainDiagram.Width) / 2;
            DeltaY = ((float)SkiaCanvas.ActualHeight - DomainDiagram.Height) / 2;
        }

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

                case DiagramMode.LineCreation:
                    SetDiagramMode(new LineCreationMode());
                    break;

                case DiagramMode.EllipseCreation:
                    SetDiagramMode(new EllipseCreationMode());
                    break;

                case DiagramMode.PolygonCreation:
                    SetDiagramMode(new PolygonCreationMode());
                    break;

                case DiagramMode.TextCreation:
                    SetDiagramMode(new Modes.TextCreationMode());
                    break;
            }

            currentMode = diagramMode;
        }

        private void SetDiagramMode(IDiagramMode diagramMode)
        {
            if(this.diagramMode != null)
            {
                this.diagramMode.Finish();
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

        /// <summary>
        /// Focused diagram item.
        /// </summary>
        internal DiagramItem? FocusItem { get; private set; }

        /// <summary>
        /// Diagram item that is now being interacted.
        /// </summary>
        internal DiagramItem? InteractingItem { get; private set; }

        /// <summary>
        /// Diagram delta x.
        /// </summary>
        internal float DeltaX { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal float DeltaY { get; private  set; }

        /// <summary>
        /// Called when redrawing the diagram.
        /// </summary>
        internal event EventHandler<SkiaDrawingContext> Redraws;

        /// <summary>
        /// Initiates redrawing of diagram.
        /// </summary>
        public void Redraw()
        {
            SkiaCanvas.InvalidateVisual();
        }

        private void HandleDiagramMouseDown(object sender, MouseButtonEventArgs eventArgs)
        {
            var mouse = new MouseParameters
            {
                LeftButton = (MouseButtonState)eventArgs.LeftButton,
                RightButton = (MouseButtonState)eventArgs.RightButton,
                Position = Position
            };

            if (FocusItem != null)
            {
                InteractingItem = FocusItem;
                FocusItem.CheckMouseDown(mouse);
            }
        }

        private void HandleDiagramMouseUp(object sender, MouseButtonEventArgs eventArgs)
        {
            var mouse = new MouseParameters
            {
                LeftButton = (MouseButtonState)eventArgs.LeftButton,
                RightButton = (MouseButtonState)eventArgs.RightButton,
                Position = Position
            };

            if (FocusItem != null)
            {
                FocusItem.CheckMouseUp(mouse);
            }

            InteractingItem = null;
        }

        private void HandleDiagramMouseMove(object sender, MouseEventArgs eventArgs)
        {
            var previousPosition = Position;
            Position = GetPosition(eventArgs);

            var mouse = new MovingMouseParameters
            {
                LeftButton = (MouseButtonState)eventArgs.LeftButton,
                RightButton = (MouseButtonState)eventArgs.RightButton,
                Position = Position,
                Delta = Position - previousPosition
            };

            if(InteractingItem != null)
            {
                InteractingItem.RaiseMouseMove(mouse);
            }
            else if(FocusItem == null && currentMode == DiagramMode.Selection && mouse.LeftButton == MouseButtonState.Pressed)
            {
                DeltaX += mouse.Delta.X;
                DeltaY += mouse.Delta.Y;
                Position = GetPosition(eventArgs);

                Redraw();
            }
            else
            {
                UpdateFocuItem(mouse);
            }
        }

        private void UpdateFocuItem(MovingMouseParameters mouse)
        {
            var interactableItems = DiagramItems
                .Where(item => item.IsVisible)
                .OrderByDescending(item => item.ZIndex);
            
            foreach (var item in interactableItems)
            {
                if (item.CheckMouseMove(mouse))
                {
                    FocusItem = item;
                    return;
                }
            }

            FocusItem = null;
            return;
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
            
            var drawingContext = new SkiaDrawingContext(canvas);
            drawingContext.Translate(DeltaX, DeltaY);

            DrawWorkspaceArea(drawingContext);

            var sortedDiagramItems = diagramItems
                .Where(item => item.IsVisible)
                .ToList();

            foreach (var item in sortedDiagramItems)
            {
                item.Draw(drawingContext);
            }

            Redraws.Invoke(this, drawingContext);
        }

        private void DrawWorkspaceArea(SkiaDrawingContext drawingContext)
        {
            var workspaceArea = new SKRect(0, 0, DomainDiagram.Width, DomainDiagram.Height);
            var paint = new SKPaint { Color = Colors.SecondaryBackground };
            drawingContext.DrawRect(workspaceArea, paint);
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
