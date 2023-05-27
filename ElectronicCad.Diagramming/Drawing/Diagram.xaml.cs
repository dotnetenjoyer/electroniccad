using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.LayoutGrids;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Diagramming.Drawing;
using ElectronicCad.Diagramming.Drawing.Items;
using ElectronicCad.Diagramming.Drawing.Modes;
using ElectronicCad.Diagramming.Drawing.DiagramItems.Layout;
using GeometryDiagram = ElectronicCad.Domain.Geometry.Diagram;
using Colors = ElectronicCad.Diagramming.Utils.Colors;
using MouseButtonState = ElectronicCad.Diagramming.Drawing.MouseButtonState;
using ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

namespace ElectronicCad.Diagramming
{
    /// <summary>
    /// Interaction logic for Diagram.xaml
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

            SkiaCanvas.PaintSurface += HandleCanvasPaint;

            MouseMove += HandleMouseMove;
            MouseUp += HandleMouseUp;
            MouseDown += HandleMouseDown;
            MouseWheel += HandleMouseWheel;

            SetDiagramMode(DiagramMode.Selection);
        }

        internal readonly static Cursor DefaultCursor = Cursors.Arrow;

        #region GeometryDiagram

        /// <summary>
        /// Related geometry diagram.
        /// </summary>
        public GeometryDiagram GeometryDiagram
        {
            get => (GeometryDiagram)GetValue(DomainDiagramProperty);
            set => SetValue(DomainDiagramProperty, value);
        }

        private static readonly DependencyProperty DomainDiagramProperty = DependencyProperty
            .Register(nameof(GeometryDiagram),
                typeof(GeometryDiagram),
                typeof(Diagram),
                new PropertyMetadata(DomainDiagramChanged));

        private static void DomainDiagramChanged(DependencyObject obj, DependencyPropertyChangedEventArgs eventArgs)
        {
            var diagram = (Diagram)obj;
            diagram.InitializeGeometryDiagram((GeometryDiagram)eventArgs.NewValue);
        }

        private void InitializeGeometryDiagram(GeometryDiagram geometryDiagram)
        {
            DeinitializeGeometryDiagram();

            GeometryDiagram = geometryDiagram;
            GeometryDiagram.GeometryAdded += HandleDiagramGeometryAdded;
            GeometryDiagram.GeometryModified += HandleGeometryModified;
            GeometryDiagram.GeometryRemoved += HandleDiagramGeometryRemoved;
            GeometryDiagram.VersionChanged += HandleVersionChange;

            CalculateInitialOffsets();
            Redraw();

            var firstRectangle = new Polygon(new Domain.Geometry.Point(100, 100), 200, 200);
            var secondRectangle = new Polygon(new Domain.Geometry.Point(300, 300), 200, 200);
            var ellipse = new Ellipse(new Domain.Geometry.Point(400, 400), 100);

            GeometryDiagram.AddGeometry(new GeometryObject[] { firstRectangle, secondRectangle, ellipse });
            var group = GeometryDiagram.CreateGroup(new[] { firstRectangle, secondRectangle });
            var group2 = GeometryDiagram.CreateGroup(new GeometryObject[] { group, ellipse});
        }

        private void DeinitializeGeometryDiagram()
        {
            if (GeometryDiagram == null)
            {
                return;
            }

            GeometryDiagram.GeometryAdded -= HandleDiagramGeometryAdded;
            GeometryDiagram.GeometryModified -= HandleGeometryModified;
            GeometryDiagram.GeometryRemoved -= HandleDiagramGeometryRemoved;
            GeometryDiagram.VersionChanged -= HandleVersionChange;
        }

        private void HandleDiagramGeometryAdded(object? sender, IEnumerable<GeometryObject> geometryObjects)
        {
            foreach (var geometryObject in geometryObjects)
            {
                var diagramItem = GeometryObjectDiagramItemsFactory.Create(geometryObject);
                diagramItem.ZIndex = GetNextZIndex();
                AddDiagramItem(diagramItem);
            }

            Redraw();
        }

        private int GetNextZIndex()
        {
            var userDiagramItems = diagramItems
                .Where(x => !x.IsAuxiliary)
                .ToList();

            var maxZIndex = userDiagramItems.Any() 
                ? userDiagramItems.Max(x => x.ZIndex) 
                : 0;


            return maxZIndex + 1;
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

        private void HandleDiagramGeometryRemoved(object? sender, IEnumerable<GeometryObject> geometryObjects)
        {
            var itemsToRemove = diagramItems
                .OfType<GeometryObjectDiagramItem>()
                .Where(item => geometryObjects.Contains(item.GeometryObject))
                .ToList();

            foreach (var itemToRemove in itemsToRemove)
            {
                diagramItems.Remove(itemToRemove);
            }

            Redraw();
        }

        private void HandleVersionChange(object? sender, EventArgs eventArgs)
        {
            Redraw();
        }

        private void CalculateInitialOffsets()
        {
            OffsetX = (float)(SkiaCanvas.ActualWidth - GeometryDiagram.Size.Width) / 2;
            OffsetY = (float)(SkiaCanvas.ActualHeight - GeometryDiagram.Size.Height) / 2;
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
                    SetDiagramMode(new Drawing.Modes.SelectionMode());
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
                    SetDiagramMode(new TextCreationMode());
                    break;
            }

            currentMode = diagramMode;
        }

        private void SetDiagramMode(IDiagramMode diagramMode)
        {
            if (this.diagramMode != null)
            {
                this.diagramMode.Finish();
            }

            this.diagramMode = diagramMode;
            diagramMode.Initialize(this);
        }

        #endregion

        #region Drawing

        /// <inheritdoc cref="PositionProperty"/>
        internal SKPoint Position
        {
            get => (SKPoint)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        /// <summary>
        /// Current diagram mouse position.
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(nameof(Position), typeof(SKPoint),
                typeof(Diagram), new PropertyMetadata());

        /// <summary>
        /// Diagram delta x.
        /// </summary>
        internal float OffsetX { get; private set; }

        /// <summary>
        /// Diagram delta y.
        /// </summary>
        internal float OffsetY { get; private set; }

        /// <summary>
        /// Diagram scale.
        /// </summary>
        internal double Scale { get; private set; } = 1;

        /// <summary>
        /// Calculates diagram position.
        /// </summary>
        /// <param name="eventArgs">Mouse event args.</param>
        /// <param name="eventArgs">Mouse event args.</param>
        /// <returns>Diagram mouse position.</returns>
        internal SKPoint CalculateDiagramPosition(MouseEventArgs eventArgs)
        {
            var position = eventArgs.GetPosition(this).ToSKPoint();
            position = position.Scale((float)Scale);
            position.Offset(-OffsetX, -OffsetY);
            return position;
        }

        /// <summary>
        /// Collection of all diagram item on the diagram sorted by z index.
        /// </summary>
        internal IEnumerable<DiagramItem> DiagramItems => diagramItems;

        private List<DiagramItem> diagramItems = new();
        
        internal void AddDiagramItem(DiagramItem diagramItem)
        {
            diagramItems.Add(diagramItem);
            diagramItems = diagramItems
                .OrderBy(item => item.ZIndex)
                .ToList();
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
        /// Collection of selected geometry objects.
        /// </summary>
        public readonly static DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
                nameof(SelectedItems),
                typeof(IEnumerable<GeometryObject>),
                typeof(Diagram),
                new FrameworkPropertyMetadata(Array.Empty<GeometryObject>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <inheritdoc cref="SelectedItemsProperty" />
        public IEnumerable<GeometryObject> SelectedItems
        {
            get => (IEnumerable<GeometryObject>)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        /// <summary>
        /// The event to notify aboutn selected items changes.
        /// </summary>
        public readonly static RoutedEvent SelectedItemsChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(SelectedItemsChanged),
                RoutingStrategy.Bubble,
                typeof(EventHandler),
                typeof(Diagram));

        /// <inheritdoc cref="SelectedItemsChangedEvent"/>
        public event EventHandler SelectedItemsChanged
        {
            add => AddHandler(SelectedItemsChangedEvent, value);
            remove => RemoveHandler(SelectedItemsChangedEvent, value);
        }

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

        private void HandleMouseDown(object sender, MouseButtonEventArgs mouseEventArgs)
        {
            if (FocusItem != null)
            {
                var mouseParameters = CreateMouseParameters(mouseEventArgs);
                FocusItem.HandleDiagramMouseDown(mouseParameters);
                
                InteractingItem = FocusItem;
                if (FocusItem is GeometryObjectDiagramItem geometryObjectDiagramItem)
                {
                    var selectedGeometryObjects = new[] { geometryObjectDiagramItem.GeometryObject };
                    SelectedItems = selectedGeometryObjects;

                    var eventArgs = new RoutedEventArgs(SelectedItemsChangedEvent, this); 
                    RaiseEvent(eventArgs);
                }
            }
        }

        private void HandleMouseUp(object sender, MouseButtonEventArgs mouseEventArgs)
        {
            if (InteractingItem != null)
            {
                var mouseParameters = CreateMouseParameters(mouseEventArgs);
                InteractingItem.HandleDiagramMouseUp(mouseParameters);
                InteractingItem = null;
            }
        }

        private MouseParameters CreateMouseParameters(MouseButtonEventArgs mouseEventArgs)
        {
            var mouseParameters = new MouseParameters
            {
                LeftButton = (MouseButtonState)mouseEventArgs.LeftButton,
                RightButton = (MouseButtonState)mouseEventArgs.RightButton,
                Position = Position
            };

            return mouseParameters;
        }

        private void HandleMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var mouseParameters = CreateMovingMouseParameters(mouseEventArgs);
            if (mouseParameters.MiddleButton == MouseButtonState.Pressed)
            {
                OffsetX += mouseParameters.Delta.X;
                OffsetY += mouseParameters.Delta.Y;
                
                Position = CalculateDiagramPosition(mouseEventArgs);
                
                Redraw();
            }
            else if (InteractingItem != null)
            {
                Cursor = InteractingItem.GetCurrentCursor();
                InteractingItem.RaiseMouseMove(mouseParameters);
            }
            else
            {
                UpdateFocuItem(mouseParameters);
            }
        }

        private MovingMouseParameters CreateMovingMouseParameters(MouseEventArgs mouseEventArgs)
        {
            var previousPosition = Position;
            Position = CalculateDiagramPosition(mouseEventArgs);

            var mouse = new MovingMouseParameters
            {
                LeftButton = (MouseButtonState)mouseEventArgs.LeftButton,
                RightButton = (MouseButtonState)mouseEventArgs.RightButton,
                MiddleButton = (MouseButtonState)mouseEventArgs.MiddleButton,
                Position = Position,
                Delta = Position - previousPosition
            };

            return mouse;
        }

        private void HandleMouseWheel(object sender, MouseWheelEventArgs eventArgs)
        {
            var sensivity = 0.001;
            
            Scale += eventArgs.Delta * sensivity;
            Redraw();
        }

        private void UpdateFocuItem(MovingMouseParameters mouse)
        {
            var visibleItems = DiagramItems
                .Where(item => item.IsVisible)
                .Reverse()
                .ToList();
            
            foreach (var item in visibleItems)
            {
                if (item.HandleDiagramMouseMove(mouse))
                {
                    if (FocusItem != item)
                    {
                        FocusItem?.RaiseMouseLeave();
                    }

                    FocusItem = item;
                    Cursor = item.GetCurrentCursor();
                    return;
                }
            }

            FocusItem?.RaiseMouseLeave();
            FocusItem = null;
            Cursor = DefaultCursor;
        }

        private void HandleCanvasPaint(object? sender, SKPaintSurfaceEventArgs eventArgs)
        {
            Draw(eventArgs.Surface.Canvas);
        }

        private void Draw(SKCanvas canvas)
        {
            if (GeometryDiagram == null)
            {
                return;
            }

            canvas.Clear();
            
            var drawingContext = new SkiaDrawingContext(canvas);
            drawingContext.Scale(Scale);
            drawingContext.Translate(OffsetX, OffsetY);

            DrawWorkspaceArea(drawingContext);

            var visibleItems = diagramItems
                .Where(item => item.IsVisible)
                .ToList();

            foreach (var item in visibleItems)
            {
                item.Draw(drawingContext);
            }

            DrawLayoutGrids(drawingContext);

            Redraws.Invoke(this, drawingContext);
        }

        private void DrawWorkspaceArea(SkiaDrawingContext drawingContext)
        {
            var workspaceArea = new SKRect(0, 0, (float)GeometryDiagram.Size.Width, (float)GeometryDiagram.Size.Height);
            var paint = new SKPaint { Color = Colors.SecondaryBackground };
            drawingContext.DrawRect(workspaceArea, paint);
        }

        private void DrawLayoutGrids(SkiaDrawingContext drawingContext)
        {
            foreach(var layoutGrid in GeometryDiagram.LayoutGrids)
            {
                if (layoutGrid is ColumnLayoutGrid columnLayoutGrid)
                {
                    var layoutGridDiagramItem = new ColumnLayoutGridDiagramItem(this, columnLayoutGrid);
                    layoutGridDiagramItem.Draw(drawingContext);
                }

                if (layoutGrid is RowLayoutGrid rowLayoutGrid)
                {
                    var layoutGridDiagramItem = new RowLayoutGridDiagramItem(this, rowLayoutGrid);
                    layoutGridDiagramItem.Draw(drawingContext);
                }

                if (layoutGrid is GridLayoutGrid gridLayoutGrid)
                {
                    var gridLayoutGridDiagramItem = new GridLayoutGridDiagramItem(this, gridLayoutGrid);
                    gridLayoutGridDiagramItem.Draw(drawingContext);
                }
            }
        }

        #endregion

        /// <inheritdoc/>
        public void Dispose()
        {
            SkiaCanvas.PaintSurface -= HandleCanvasPaint;

            MouseMove -= HandleMouseMove;
            MouseUp -= HandleMouseUp;
            MouseDown -= HandleMouseDown;

            foreach (var diagramItem in DiagramItems)
            {
                diagramItem.Dispose();
            }

            DeinitializeGeometryDiagram();
        }
    }
}