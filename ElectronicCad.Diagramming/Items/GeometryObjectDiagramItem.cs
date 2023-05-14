using SkiaSharp;
using SkiaSharp.Views.Desktop;
using ElectronicCad.Diagramming.Extensions;
using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Items;

/// <summary>
/// Base implementation of <see cref="IGeometryObjectDiagramItem"/>
/// </summary>
internal abstract class GeometryObjectDiagramItem : DiagramItem, IGeometryObjectDiagramItem
{
    /// <summary>
    /// Domain geometry object.
    /// </summary>
    public GeometryObject GeometryObject { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="domainObject">Domain object.</param>
    public GeometryObjectDiagramItem(GeometryObject domainObject)
    {
        GeometryObject = domainObject;
        UpdateViewState();
    }

    /// <inheritdoc />
    public virtual void UpdateViewState()
    {
        BoundingBox = GeometryObject.BoundingBox.ToSKRect();
        StrokePaint = new SKPaint
        {
            Color = GeometryObject.StrokeColor.ToSKColor(),
            Style = SKPaintStyle.Stroke,
            StrokeWidth = (float)GeometryObject.StrokeWidth,
        };
    }

    /// <inheritdoc />
    public override bool CheckHit(ref SKPoint position)
    {
        return GeometryObject.CheckHit(position.ToDomainPoint());
    }
}
