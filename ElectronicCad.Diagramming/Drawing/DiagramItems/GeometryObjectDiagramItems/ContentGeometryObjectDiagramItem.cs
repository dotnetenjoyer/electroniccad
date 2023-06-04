using SkiaSharp;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.DiagramItems.GeometryObjectDiagramItems;

/// <summary>
/// The base diagram item that represent item with a content.
/// </summary>
internal abstract class ContentGeometryObjectDiagramItem<TContentGeometry> : GeometryObjectDiagramItem<TContentGeometry> where TContentGeometry : ContentGeometry
{
    /// <summary>
    /// Fill geometry paint.
    /// </summary>
    public SKPaint FillPaint { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="contentGeometry">Content geometry object.</param>
    public ContentGeometryObjectDiagramItem(TContentGeometry contentGeometry) : base(contentGeometry)
    {
    }

    /// <inheritdoc />
    public override void UpdateViewState()
    {
        base.UpdateViewState();

        if (FillPaint != null)
        {
            FillPaint.Dispose();
        }

        FillPaint = new SKPaint
        {
            Color = GeometryObject.FillColor.ToSKColor(),
            Style = SKPaintStyle.Fill,
        };
    }

    /// <inheritdoc />
    protected override void DisposeManagedResources()
    {
        base.DisposeManagedResources();

        if (FillPaint != null)
        {
            FillPaint.Dispose();
        }
    }
}