using System.Numerics;
using Microsoft.Toolkit.Mvvm.Input;
using ElectronicCad.Domain.Geometry;
using ElectronicCad.Domain.Geometry.Utils;
using ElectronicCad.MVVM.Properties.Abstractions;

namespace ElectronicCad.MVVM.ViewModels.Properties.CustomSections.Align;

/// <summary>
/// Align custom section.
/// </summary>
public class AlignCustomSection : ICustomSection
{
    private readonly IAlignPropertiesProxy proxy;

    /// <summary>
    /// Command to align geometry objects to top.
    /// </summary>
    public RelayCommand AlignTopCommand { get; }

    /// <summary>
    /// Command to align geometry objects to middle.
    /// </summary>    
    public RelayCommand AlignMiddleCommand { get; }

    /// <summary>
    /// Command to align geometry objects to bottom.
    /// </summary>
    public RelayCommand AlignBottomCommand { get; }

    /// <summary>
    /// Command to align geometry objects to start.
    /// </summary>
    public RelayCommand AlignStartCommand { get; }

    /// <summary>
    /// Command to align geometry objects to center.
    /// </summary>
    public RelayCommand AlignCenterCommand { get; }

    /// <summary>
    /// Command to align geometry objects to end.
    /// </summary>    
    public RelayCommand AlignEndCommand { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="proxy">Align proxy.</param>
    /// <param name="serviceProvider">Service provider.</param>
    public AlignCustomSection(IAlignPropertiesProxy proxy)
    {
        this.proxy = proxy;

        AlignTopCommand = new RelayCommand(AlignTop);
        AlignMiddleCommand = new RelayCommand(AlignMiddle);
        AlignBottomCommand = new RelayCommand(AlignBottom);
        AlignStartCommand = new RelayCommand(AlignStart);
        AlignCenterCommand = new RelayCommand(AlignCenter);
        AlignEndCommand = new RelayCommand(AlignEnd);
    }

    private void AlignMiddle()
    {
        var points = proxy.GeometryObjects.SelectMany(x => x.ControlPoints);
        var boundingBox = PointsUtils.CalculateBoundingBox(points);
        var targetMiddle = boundingBox.Center.Y;

        Align((boundingBox) => new Vector2(0, (float)(targetMiddle - boundingBox.Center.Y)));
    }

    private void AlignTop()
    {
        var tops = proxy.GeometryObjects.Select(x => x.BoundingBox.Start.Y);
        var targetTop = tops.Min();
        
        Align((boundingBox) => new Vector2(0, (float)(targetTop - boundingBox.Start.Y)));
    }

    private void AlignBottom()
    {
        var bottoms = proxy.GeometryObjects.Select(x => x.BoundingBox.End.Y);
        var targetBottom = bottoms.Max();
        
        Align((boundingBox) => new Vector2(0, (float)(targetBottom - boundingBox.End.Y)));
    }

    private void AlignCenter()
    {
        var points = proxy.GeometryObjects.SelectMany(x => x.ControlPoints);
        var boundingBox = PointsUtils.CalculateBoundingBox(points);
        var targetCenter = boundingBox.Center.X;

        Align((boundingBox) => new Vector2((float)(targetCenter - boundingBox.Center.X), 0));
    }

    private void AlignStart()
    {
        var starts = proxy.GeometryObjects.Select(x => x.BoundingBox.Start.X);
        var targetStart = starts.Min();

        Align((boundingBox) => new Vector2((float)(targetStart - boundingBox.Start.X), 0));
    }

    private void AlignEnd()
    {
        var ends = proxy.GeometryObjects.Select(obj => obj.BoundingBox.End.X);
        var targetEnd = ends.Max();

        Align((boundingBox) => new Vector2((float)(targetEnd - boundingBox.End.X), 0));
    }

    private void Align(Func<Rectangle, Vector2> calculateTranslate)
    {
        if (proxy.GeometryObjects == null || !proxy.GeometryObjects.Any())
        {
            return;
        }

        using var scope = proxy.GeometryObjects!.First().StartDiagramModifcation();
        foreach (var geometry in proxy.GeometryObjects)
        {
            geometry.StartModification();

            var translateMatrix = Matrix3x2.CreateTranslation(calculateTranslate(geometry.BoundingBox));
            geometry.Transform(translateMatrix);

            geometry.CompleteModification();
        }
    }
}
