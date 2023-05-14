using SkiaSharp;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// The base diagram item that represent item with a content.
/// </summary>
internal abstract class ContentGeometryObjectDiagramItem : GeometryObjectDiagramItem
{
    /// <summary>
    /// Diagram item fill paint.
    /// </summary>
    public SKPaint FillPaint { get; set; }

    private ContentGeometry ContentGeometry => (ContentGeometry)GeometryObject;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="contentGeometry">Content geometry object.</param>
    public ContentGeometryObjectDiagramItem(ContentGeometry contentGeometry) : base(contentGeometry)
    {
    }

    /// <inheritdoc />
    public override void UpdateViewState()
    {
        base.UpdateViewState();

        FillPaint = new SKPaint
        {
            Color = ContentGeometry.FillColor.ToSKColor(),
            Style = SKPaintStyle.Fill,
        };
    }
} 