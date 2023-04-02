using ElectronicCad.Domain.Geometry;

namespace ElectronicCad.Diagramming.Extensions;

/// <summary>
/// System geometry, graphics extensions.
/// </summary>
internal static class SystemGeometryExtensions
{
    /// <summary>
    /// Convert system windows point to domain point.
    /// </summary>
    /// <param name="systemPoint">System point to convert.</param>
    /// <returns>Domain point.</returns>
    public static Point ToDomainPoint(this System.Windows.Point systemPoint)
    {
        return new Point((float)systemPoint.X, (float)systemPoint.Y);
    }
}
