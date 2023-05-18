using SkiaSharp;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Drawing.Items;

/// <summary>
/// The base diagram item that represent item with a content.
/// </summary>
internal abstract class ContentGeometryObjectDiagramItem<TContentGeometry> : GeometryObjectDiagramItem<TContentGeometry> where TContentGeometry : ContentGeometry
{
    /// <summary>
    /// Diagram item fill paint.
    /// </summary>
    public SKPaint FillPaint { get; set; }

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

        FillPaint = new SKPaint
        {
            Color = CertainGeometryObject.FillColor.ToSKColor(),
            Style = SKPaintStyle.Fill,
        };
    }
}