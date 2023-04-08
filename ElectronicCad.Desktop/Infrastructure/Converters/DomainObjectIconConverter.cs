
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Workspace;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ElectronicCad.Desktop.Infrastructure.Converters;

/// <summary>
/// Converts domain object to icon path.
/// </summary>
public class DomainObjectIconConverter : IValueConverter
{
    /// <summary>
    /// Project diagram icon path.
    /// </summary>
    public string ProjectDiagramIconPath { get; init; }

    /// <summary>
    /// Pen icon path.
    /// </summary>
    public string PenIconPath { get; init; }

    /// <summary>
    /// Line icon path.
    /// </summary>
    public string LineIconPath { get; init; }

    /// <summary>
    /// Rectangle icon path.
    /// </summary>
    public string RectangleIconPath { get; init; }

    /// <summary>
    /// Ellipse icon path.
    /// </summary>
    public string EllipseIconPath { get; init; }

    /// <summary>
    /// Text icon path.
    /// </summary>
    public string TextIconPath { get; init; }

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return string.Empty;
        }

        return value switch
        {
            ProjectDiagram => ProjectDiagramIconPath,
            Line => LineIconPath,
            Ellipse => EllipseIconPath,
            Polygon => RectangleIconPath,
            Text => TextIconPath,
            _ => string.Empty

        };
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}